using UnityEngine;

public class MealCarryInteractable : Interactable
{
    private bool isCarried = false;
    private bool isPlaced = false;

    private Transform carryPoint;
    private Player carrierPlayer;

    private void Update()
    {
        if (isCarried && carryPoint != null)
        {
            transform.position = carryPoint.position;
            transform.rotation = carryPoint.rotation;
        }
    }

    public override void Interact(Player player)
    {
        if (isPlaced) return;

        Transform foundCarryPoint = player.transform.Find("CarryPoint");
        if (foundCarryPoint == null)
        {
            Debug.LogError("MealCarryInteractable: Player has no CarryPoint child.");
            return;
        }

        // 如果玩家已经拿着箱子或饭，就不允许再拿
        if (!isCarried)
        {
            if (player.carriedBox != null)
            {
                Debug.Log("Player is carrying a box, cannot pick up meal.");
                return;
            }

            if (player.carriedMeal != null)
            {
                Debug.Log("Player is already carrying a meal.");
                return;
            }

            carryPoint = foundCarryPoint;
            carrierPlayer = player;
            isCarried = true;
            player.carriedMeal = this;

            Debug.Log("Picked up meal: " + gameObject.name);
        }
        else
        {
            DropMeal();
        }
    }

    public void DropMeal()
    {
        if (!isCarried) return;

        isCarried = false;

        if (carrierPlayer != null && carrierPlayer.carriedMeal == this)
        {
            carrierPlayer.carriedMeal = null;
        }

        if (carrierPlayer != null)
        {
            transform.position = carrierPlayer.transform.position + carrierPlayer.transform.forward * 1.0f;
        }

        carrierPlayer = null;
        carryPoint = null;

        Debug.Log("Dropped meal manually: " + gameObject.name);
    }

    public void PlaceInZone(Transform zoneTransform)
    {
        if (isPlaced) return;

        isCarried = false;
        isPlaced = true;

        transform.position = zoneTransform.position;
        transform.rotation = zoneTransform.rotation;

        if (carrierPlayer != null && carrierPlayer.carriedMeal == this)
        {
            carrierPlayer.carriedMeal = null;
        }

        carrierPlayer = null;
        carryPoint = null;

        Debug.Log("Placed meal: " + gameObject.name);
    }
}