using UnityEngine;

public class CleaningAgent : InteractableAction
{
    private MopCleaner cleaner = null;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            cleaner = player.GetComponentInChildren<MopCleaner>();

            if (cleaner != null)
            {
                _isInside = true;
                canvas.SetActive(true);
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = false;
            player = null;
            cleaner = null;
            ResetButton();
            canvas.SetActive(false);
        }
    }


    protected override void OnComplete()
    {
        MopCleaner mop = player.GetComponentInChildren<MopCleaner>();
        if (mop != null)
        {
            mop.SetWet();
        }
    }
}
