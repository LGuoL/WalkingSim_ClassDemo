using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalLocalPos;

    void Awake()
    {
        originalLocalPos = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float timer = 0f;

        while (timer < duration)
        {
            Vector3 offset = Random.insideUnitSphere * magnitude;
            transform.localPosition = originalLocalPos + offset;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalLocalPos;
    }
}