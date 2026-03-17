using System.Collections;
using UnityEngine;

public class WakeMonitorInteractable : Interactable
{
    private bool used = false;
    public MonitorBoot monitorBoot;

    public override void Interact(Player player)
    {
        if (used) return;
        used = true;

        StartCoroutine(InteractionSequence(player));
    }

    IEnumerator InteractionSequence(Player player)
    {
    
        if (player != null)
            player.SetControlEnabled(false);

        if (monitorBoot != null)
            yield return StartCoroutine(monitorBoot.PlayBootSequenceRoutine());

        Level2SequenceManager.instance.OpenWakeChoice();
    }
}