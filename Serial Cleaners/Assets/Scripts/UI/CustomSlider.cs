using TMPro;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Slider))]
    public class CustomSlider : MonoBehaviour
    {
        [SerializeField] private TMP_Text targetText = null;
        [SerializeField] private string stringFormat = "F0";
        [SerializeField] private string targetTextUnit = "u";
        [SerializeField] private bool percentage;

        private Slider slider;


        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void Start()
        {
            slider.onValueChanged.AddListener(UpdateText);

            int value = ((int)slider.value + 80) * 100 / 80;

            targetText.text = value.ToString(stringFormat) + targetTextUnit;
        }

        private void UpdateText(float value)
        {
            value = (value + 80) * 100 / 80;

            targetText.text = value.ToString(stringFormat) + targetTextUnit;
        }
    }
}