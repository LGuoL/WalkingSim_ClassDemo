using UnityEngine;

public class FallingPlatformPiece : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasFallen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = true;
    }

    public void Fall()
    {
        if (hasFallen) return;
        hasFallen = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Destroy(gameObject, 5f);
    }
}