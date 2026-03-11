using UnityEngine;

public class CookingStation : Interactable
{
    public GameObject mealPrefab;
    public Transform mealSpawnPoint;
    private bool cooked = false;

    public override void Interact(Player player)
    {
        if (cooked) return;
        if (!Level3SequenceManager.instance.HasAllIngredients()) return;

        cooked = true;

        if (mealPrefab != null && mealSpawnPoint != null)
        {
            Instantiate(mealPrefab, mealSpawnPoint.position, mealSpawnPoint.rotation);
        }

        Level3SequenceManager.instance.RegisterMealCooked();
    }
}