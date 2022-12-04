using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public abstract class InteractableAction : MonoBehaviour
{
    [SerializeField] protected GameObject canvas;
    [SerializeField] private Image fillImage;

    protected SphereCollider _collider;

    protected bool _isInside;
    private bool isFilled;

    private float fillTime = 1.2f;
    private float fillTimer = 0.0f;

    protected GameObject player;

    protected virtual void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
    }

    protected virtual void Start()
    {
        ResetButton();
    }

    protected virtual void Update()
    {
        if (_isInside)
        {
            if (Input.GetButton("Interact") && !isFilled)
            {
                fillTimer += Time.unscaledDeltaTime;
                fillImage.fillAmount = fillTimer / fillTime;
            }
            else if (!isFilled && fillTimer > 0)
            {
                fillTimer -= Time.unscaledDeltaTime;
                fillImage.fillAmount = fillTimer / fillTime;
            }

            if (fillTimer > fillTime && !isFilled)
            {
                isFilled = true;
                OnComplete();
                ResetButton();
                canvas.SetActive(false);
            }
        }
    }

    protected void ResetButton()
    {
        isFilled = false;
        fillTimer = 0.0f;
        fillImage.fillAmount = 0.0f;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = true;
            player = other.gameObject;
            canvas.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = false;
            player = null;
            ResetButton();
            canvas.SetActive(false);
        }
    }

    protected abstract void OnComplete();
}
