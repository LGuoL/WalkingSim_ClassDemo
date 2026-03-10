using UnityEngine;

public class PhoneInteractable : Interactable
{
    private bool canInteract = false;
    private bool hasAnswered = false;

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }

    public override void Interact(Player player)
    {
        if (!canInteract || hasAnswered) return;

        hasAnswered = true;
        Level1SequenceManager.instance.AnswerPhone();
    }
}