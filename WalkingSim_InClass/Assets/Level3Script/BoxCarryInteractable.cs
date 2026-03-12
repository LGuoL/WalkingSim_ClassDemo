using UnityEngine;

public class BoxCarryInteractable : Interactable
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
            Debug.LogError("BoxCarryInteractable: Player has no CarryPoint child.");
            return;
        }

        // 如果当前没抱箱子，就拿起来
        if (!isCarried)
        {
            // 如果玩家已经抱着别的箱子，就不允许再拿
            if (player.carriedBox != null)
            {
                Debug.Log("Player is already carrying another box.");
                return;
            }

            carryPoint = foundCarryPoint;
            carrierPlayer = player;
            isCarried = true;
            player.carriedBox = this;

            Debug.Log("Picked up box: " + gameObject.name);
        }
        // 如果已经抱着这箱子，再按一次E就手动放下
        else
        {
            DropBox();
        }
    }

    public void DropBox()
    {
        if (!isCarried) return;

        isCarried = false;

        if (carrierPlayer != null && carrierPlayer.carriedBox == this)
        {
            carrierPlayer.carriedBox = null;
        }

        // 放在玩家前方一点
        if (carrierPlayer != null)
        {
            transform.position = carrierPlayer.transform.position + carrierPlayer.transform.forward * 1.2f;
        }

        carrierPlayer = null;
        carryPoint = null;

        Debug.Log("Dropped box manually: " + gameObject.name);
    }

    public void PlaceInZone(Transform zoneTransform)
    {
        if (isPlaced) return;

        isCarried = false;
        isPlaced = true;

        transform.position = zoneTransform.position;
        transform.rotation = zoneTransform.rotation;

        if (carrierPlayer != null && carrierPlayer.carriedBox == this)
        {
            carrierPlayer.carriedBox = null;
        }

        carrierPlayer = null;
        carryPoint = null;

        Debug.Log("Placed box: " + gameObject.name);
    }
}