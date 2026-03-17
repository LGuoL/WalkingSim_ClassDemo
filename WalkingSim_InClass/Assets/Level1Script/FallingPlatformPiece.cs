using System.Collections;
using UnityEngine;

public class FallingPlatformPiece : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasFallen = false;
    private Vector3 originalPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;

        if (rb != null)
            rb.isKinematic = true;
    }

    public void Fall()
    {
        if (hasFallen) return;
        hasFallen = true;
        StartCoroutine(FallRoutine());
    }

    IEnumerator FallRoutine()
    {
        float shakeDuration = 0.35f;
        float shakeMagnitude = 0.05f;
        float timer = 0f;

        while (timer < shakeDuration)
        {
            transform.position = originalPos + Random.insideUnitSphere * shakeMagnitude;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
        }

        Destroy(gameObject, 5f);
    }
}