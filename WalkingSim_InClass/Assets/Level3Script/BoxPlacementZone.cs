using UnityEngine;

public class BoxPlacementZone : MonoBehaviour
{
    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (occupied) return;

        BoxCarryInteractable box = other.GetComponent<BoxCarryInteractable>();
        if (box != null)
        {
            occupied = true;
            box.PlaceInZone(transform);
            Level3SequenceManager.instance.RegisterBoxPlaced();
        }
    }
}