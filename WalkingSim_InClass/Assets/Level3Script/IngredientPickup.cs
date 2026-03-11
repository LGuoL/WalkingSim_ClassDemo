using UnityEngine;

public class IngredientPickup : Interactable
{
    private bool picked = false;

    public override void Interact(Player player)
    {
        if (picked) return;
        picked = true;
        gameObject.SetActive(false);
        Level3SequenceManager.instance.RegisterIngredientCollected();
    }
}