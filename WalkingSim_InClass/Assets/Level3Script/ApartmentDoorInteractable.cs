using UnityEngine;

public class ApartmentDoorInteractable : Interactable
{
    public bool isMainDoor1403 = true;
    private bool used = false;

    [Header("Door Settings")]
    public GameObject doorBlockObject;   // 你这个 cube 门本体
    public bool disableDoorAfterInteract = true;

    public override void Interact(Player player)
    {
        if (!isMainDoor1403 || used) return;

        used = true;

        // 开始第三关任务
        Level3SequenceManager.instance.StartLevel3Tasks();

        // 让门消失，相当于开门
        if (disableDoorAfterInteract && doorBlockObject != null)
        {
            doorBlockObject.SetActive(false);
        }

        Debug.Log("Door 1403 opened.");
    }
}