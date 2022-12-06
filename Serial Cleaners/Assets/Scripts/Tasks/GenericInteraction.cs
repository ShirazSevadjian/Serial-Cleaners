
public class GenericInteraction : Interactable
{
    private PlayerInteractable currentHandler;

    protected override void Interact()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerInteractable>().Attach(this.gameObject, leftHandPosition, rightHandPosition))
            {
                currentHandler = player.GetComponent<PlayerInteractable>();

                _collider.enabled = false;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
                canvas.SetActive(false);
            }
        }
    }

    public override void Detach()
    {
        currentHandler = null;

        _collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        canvas.SetActive(true);
    }

    private void OnDestroy()
    {
        currentHandler.Detach();
        Detach();

        WeaponsManager.Instance.DisposeOfWeapon(gameObject);
    }
}
