using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PoliceLights : MonoBehaviour
{
    [SerializeField] private bool invert;
    [SerializeField] private Color redColor;
    [SerializeField] private Color blueColor;
    private Image image;
    

    private void Awake()
    {
        image = GetComponent<Image>();
        if (invert)
        {
            image.color = redColor;
        }
        else
        {
            image.color = blueColor;
        }
    }

    private void Start()
    {
        if (invert)
        {
            image.DOColor(blueColor, 0.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetAutoKill(true);
        }
        else
        {
            image.DOColor(redColor, 0.2f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true).SetAutoKill(true);
        }
    }

    private void OnDestroy()
    {
        
    }

}
