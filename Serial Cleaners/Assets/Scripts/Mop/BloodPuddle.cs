using System.Collections;
using UnityEngine;

public class BloodPuddle : MonoBehaviour
{
    [SerializeField] private Texture2D maskBaseTexture;

    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float bloodAge = 10.0f;

    private BloodManager bloodManager;
    private Material material;
    private Texture2D brushTexture;
    private Texture2D templateMask;
    private Coroutine ageCoroutine;

    private bool aged = false;
    private float bloodAmountTotal;
    private float bloodRemaining;
    private float bloodPercentange;


    private void Awake()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        bloodManager = BloodManager.Instance;
        brushTexture = bloodManager.BrushTexture;

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

        ageCoroutine = StartCoroutine(AgePuddle());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Mop"))
        {
            if (!aged || other.GetComponent<MopCleaner>().wet)
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
    }

    private void CreateTexture()
    {
        templateMask = new Texture2D(maskBaseTexture.width, maskBaseTexture.height);
        templateMask.SetPixels(maskBaseTexture.GetPixels());
        templateMask.Apply();

        material.SetTexture("_MaskTexture", templateMask);
    }

    private IEnumerator AgePuddle()
    {
        while (bloodAge > 0)
        {
            bloodAge -= 0.1f;
            material.SetVector("_BColor", bloodManager.ColorGradient.Evaluate(bloodAge / 10));
            yield return new WaitForSeconds(0.2f);
        }

        bloodAge = 0;
        aged = true;
    }

    private void OnDestroy()
    {
        if (ageCoroutine != null) StopCoroutine(ageCoroutine);
    }
}
