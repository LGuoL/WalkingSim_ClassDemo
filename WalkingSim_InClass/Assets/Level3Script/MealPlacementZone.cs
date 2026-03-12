using UnityEngine;

public class MealPlacementZone : MonoBehaviour
{
    private bool placed = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered meal zone: " + other.gameObject.name);

        if (placed) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player entered meal zone: " + player.gameObject.name);

            if (player.carriedMeal != null)
            {
                Debug.Log("Player is carrying meal: " + player.carriedMeal.gameObject.name);

                placed = true;
                player.carriedMeal.PlaceInZone(transform);
                Level3SequenceManager.instance.RegisterMealPlaced();

                Debug.Log("Meal placed successfully.");
            }
            else
            {
                Debug.Log("Player entered meal zone but is not carrying meal.");
            }
        }
    }
}