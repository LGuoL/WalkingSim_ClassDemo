using UnityEngine;

public class DestoryInteractable : Interactable
{
    public override void Interact(Player player)
    {
        Destroy(gameObject);
        Debug.Log("Destoryed: " + gameObject.name);
    }
}
