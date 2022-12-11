using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CleaningAgentController : Interactable
{
    [Header("Event")]
    [SerializeField] private Image fillImage;

    [Header("Interactable")]
    [SerializeField] private GameObject text;

    private bool _isFilled;

    private SphereCollider _sphereCollider;
    private MeshCollider _meshCollider;

    private GameObject _player = null;
    private GameObject _playerHolding = null;
    private MopCleaner cleaner = null;

    private float fillTime = 1.2f;
    private float fillTimer = 0.0f;

    protected override void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    protected override void Update()
    {
        if (_isInside)
        {
            if (cleaner != null)
            {
                if (_player.GetComponentInParent<PlayerInput>().actions["Pickup"].IsPressed() && !_isFilled)
                {
                    fillTimer += Time.unscaledDeltaTime;
                    fillImage.fillAmount = fillTimer / fillTime;
                }
                else if (!_isFilled && fillTimer > 0)
                {
                    fillTimer -= Time.unscaledDeltaTime;
                    fillImage.fillAmount = fillTimer / fillTime;
                }

                if (fillTimer > fillTime && !_isFilled)
                {
                    _isFilled = true;
                    OnComplete();
                    ResetButton();
                    canvas.SetActive(false);
                }
            }
            else
            {
                if (_playerHolding == null)
                {
                    if (_player.GetComponentInParent<PlayerInput>().actions["Pickup"].WasPressedThisFrame())
                    {
                        Interact();
                    }
                }
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = true;
            _player = other.gameObject;

            cleaner = _player.GetComponentInChildren<MopCleaner>();

            if (cleaner != null)
            {
                canvas.SetActive(true);
            }
            else
            {
                if (_playerHolding == null)
                {
                    text.SetActive(true);
                }
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = false;
            _player = null;
            cleaner = null;
            ResetButton();
            canvas.SetActive(false);
            text.SetActive(false);
        }
    }

    private void OnComplete()
    {
        if (cleaner != null)
        {
            cleaner.SetWet();
        }
    }

    private void ResetButton()
    {
        _isFilled = false;
        fillTimer = 0.0f;
        fillImage.fillAmount = 0.0f;
    }

    protected override void Interact()
    {
        if (_player != null)
        {
            if (_player.GetComponent<PlayerInteractable>().Attach(this.gameObject, leftHandPosition, rightHandPosition))
            {
                transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("InteractableObject");
                text.SetActive(false);
                canvas.SetActive(false);
                _meshCollider.enabled = false;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
                _playerHolding = _player;
                _player = null;
            }
        }
    }

    public override void Detach()
    {
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        _meshCollider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _playerHolding = null;
        //text.SetActive(true);
    }
}
