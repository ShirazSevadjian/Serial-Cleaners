
public class InteractTest : Interactable
{
    protected override void Interact()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerInteractable>().AttachHandsOnly(this.gameObject, leftHandPosition, rightHandPosition))
            {
                _collider.enabled = false;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
                canvas.SetActive(false);
            }
        }
    }
}
