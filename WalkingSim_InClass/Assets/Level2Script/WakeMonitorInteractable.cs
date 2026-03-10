using UnityEngine;

public class WakeMonitorInteractable : Interactable
{
    private bool used = false;

    public override void Interact(Player player)
    {
        if (used) return;

        used = true;
        Level2SequenceManager.instance.OpenWakeChoice();
    }
}