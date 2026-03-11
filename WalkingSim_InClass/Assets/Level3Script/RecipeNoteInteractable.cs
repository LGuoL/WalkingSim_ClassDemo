using UnityEngine;

public class RecipeNoteInteractable : Interactable
{
    public override void Interact(Player player)
    {
        Debug.Log("Recipe: Curry Pork Cutlet Rice");
    }
}