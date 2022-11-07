
public class InteractTest : Interactable
{
    public override void Detach()
    {
        print("Deatched");
    }

    protected override void Interact()
    {
        print("Interacted");
    }
}
