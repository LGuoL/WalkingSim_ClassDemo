using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class NpcInteractable : Interactable
{
    public NPCData nPCData;

    public override void Interact(Player player)
    {
        if(nPCData == null)
        {
            Debug.Log("npc has no data: " + gameObject.name);
        }

        player.RequestDialogue(nPCData);
    }
}
