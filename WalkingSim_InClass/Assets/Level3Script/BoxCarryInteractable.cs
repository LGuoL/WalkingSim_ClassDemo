using UnityEngine;

public class BoxCarryInteractable : Interactable
{
    private bool isCarried = false;
    private Transform carryPoint;

    void Update()
    {
        if (isCarried && carryPoint != null)
        {
            transform.position = carryPoint.position;
            transform.rotation = carryPoint.rotation;
        }
    }

    public override void Interact(Player player)
    {
        if (isCarried) return;

        carryPoint = player.transform;
        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, 1f, 1.2f);
        isCarried = true;
    }

    public void PlaceInZone(Transform zoneTransform)
    {
        isCarried = false;
        transform.SetParent(null);
        transform.position = zoneTransform.position;
        transform.rotation = zoneTransform.rotation;
        enabled = false;
    }
}