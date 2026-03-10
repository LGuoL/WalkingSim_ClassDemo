using UnityEngine;

public class ApartmentDoorInteractable : Interactable
{
    public bool isMainDoor1403 = true;
    private bool used = false;

    public override void Interact(Player player)
    {
        if (!isMainDoor1403 || used) return;

        used = true;
        Level3SequenceManager.instance.StartLevel3Tasks();
    }
}