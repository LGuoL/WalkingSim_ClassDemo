using UnityEngine;

public class BoxPlacementZone : MonoBehaviour
{
    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered zone: " + other.gameObject.name);

        if (occupied) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player entered zone: " + player.gameObject.name);

            if (player.carriedBox != null)
            {
                Debug.Log("Player is carrying box: " + player.carriedBox.gameObject.name);

                occupied = true;
                player.carriedBox.PlaceInZone(transform);
                Level3SequenceManager.instance.RegisterBoxPlaced();

                Debug.Log("Box placed in zone: " + gameObject.name);
            }
            else
            {
                Debug.Log("Player entered zone but is not carrying a box.");
            }
        }
    }
}