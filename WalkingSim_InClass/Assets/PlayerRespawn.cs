using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float deathY = -20f;

    private CharacterController controller;
    private Player playerScript;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        if (transform.position.y < deathY)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        Debug.Log("Player fell out of bounds. Respawning...");

        if (respawnPoint == null)
        {
            Debug.LogWarning("Respawn Point is not assigned.");
            return;
        }

        if (controller != null)
        {
            controller.enabled = false;
        }

        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        if (playerScript != null)
        {
            playerScript.ResetVerticalVelocity();
            playerScript.ResetLookRotation();
        }

        if (controller != null)
        {
            controller.enabled = true;
        }
    }
}