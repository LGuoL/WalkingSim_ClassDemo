using UnityEngine;

public class PuzzleMonitorInteractable : Interactable
{
    private bool canInteract = false;
    private bool used = false;

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }

    public override void Interact(Player player)
    {
        if (!canInteract || used) return;

        used = true;
        Level2SequenceManager.instance.OpenPuzzleView();
    }
}