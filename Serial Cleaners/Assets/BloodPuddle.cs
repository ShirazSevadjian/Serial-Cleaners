using UnityEngine;

public class BloodPuddle : MonoBehaviour
{
    [SerializeField] private Texture2D maskBaseTexture;
    [SerializeField] private Texture2D brushTexture;

    [SerializeField] private float threshold = 0.1f;

    private Material material;
    private Texture2D templateMask;

    private float bloodAmountTotal;
    private float bloodRemaining;
    private float bloodPercentange;

    private BloodManager bloodManager;

    private void Awake()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        bloodManager = BloodManager.Instance;

        bloodAmountTotal = 0.0f;
        for (int x = 0; x < maskBaseTexture.width; x++)
        {
            for (int y = 0; y < maskBaseTexture.height; y++)
            {
                bloodAmountTotal += maskBaseTexture.GetPixel(x, y).g;
            }
        }

        bloodRemaining = bloodAmountTotal;

        CreateTexture();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Mop"))
        {
            Ray ray = new Ray(other.transform.position, -Vector3.up);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.0f))
            {
                Vector2 textureCoord = hitInfo.textureCoord;
                int pixelX = (int)(textureCoord.x * templateMask.width);
                int pixelY = (int)(textureCoord.y * templateMask.height);

                int pixelXOffset = pixelX - (brushTexture.width / 2);
                int pixelYOffset = pixelY - (brushTexture.height / 2);

                for (int x = 0; x < brushTexture.width; x++)
                {
                    for (int y = 0; y < brushTexture.height; y++)
                    {
                        Color pixel = brushTexture.GetPixel(x, y);
                        Color pixelMask = templateMask.GetPixel(pixelXOffset + x, pixelYOffset + y);

                        if (pixelMask.g == 1 && pixel.g <= 0.1f)
                        {
                            bloodRemaining--;
                            bloodPercentange = bloodRemaining / bloodAmountTotal;

                            if (bloodPercentange < threshold)
                            {
                                bloodManager.RemovePuddle(this);
                                Destroy(gameObject);
                            }
                        }

                        templateMask.SetPixel(pixelXOffset + x, pixelYOffset + y, new Color(0.0f, pixel.g * pixelMask.g, 0.0f));
                    }
                }

                templateMask.Apply();
            }
        }
    }

    private void FixedUpdate()
    {
    }

    private void CreateTexture()
    {
        templateMask = new Texture2D(maskBaseTexture.width, maskBaseTexture.height);
        templateMask.SetPixels(maskBaseTexture.GetPixels());
        templateMask.Apply();

        material.SetTexture("_MaskTexture", templateMask);
    }
}
