using UnityEngine;

public class MealPlacementZone : MonoBehaviour
{
    private bool placed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (placed) return;

        if (other.CompareTag("Meal"))
        {
            placed = true;
            other.transform.position = transform.position;
            Level3SequenceManager.instance.RegisterMealPlaced();
        }
    }
}