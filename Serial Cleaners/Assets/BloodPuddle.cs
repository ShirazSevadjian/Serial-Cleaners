using UnityEngine;

public class BloodPuddle : MonoBehaviour
{
    public Texture2D maskBaseTexture;
    public Texture2D brushTexture;

    private Material material;
    private Texture2D templateMask;

    private float bloodAmountTotal;
    private float bloodRemaining;
    private float bloodPercentange;

    private void Start()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;

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
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.0f))
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

                        if (pixelMask.g == 1)
                        {
                            bloodRemaining -= 0.8f;
                            bloodPercentange = bloodRemaining / bloodAmountTotal;
                            
                            if (bloodPercentange < 0.1f)
                            {
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
        print(bloodPercentange);
    }

    private void CreateTexture()
    {
        templateMask = new Texture2D(maskBaseTexture.width, maskBaseTexture.height);
        templateMask.SetPixels(maskBaseTexture.GetPixels());
        templateMask.Apply();

        material.SetTexture("_MaskTexture", templateMask);
    }
}
