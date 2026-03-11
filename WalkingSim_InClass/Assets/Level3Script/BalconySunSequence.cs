using System.Collections;
using UnityEngine;

public class BalconySunSequence : MonoBehaviour
{
    public GameObject sunRoot;
    public Transform sunTransform;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float riseDuration = 5f;
    public bool canTrigger = false;

    public void EnableSequence()
    {
        canTrigger = true;
        if (sunRoot != null) sunRoot.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTrigger) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            StartCoroutine(RunSunSequence());
        }
    }

    IEnumerator RunSunSequence()
    {
        canTrigger = false;

        if (sunRoot != null) sunRoot.SetActive(true);

        float timer = 0f;
        while (timer < riseDuration)
        {
            timer += Time.deltaTime;
            float t = timer / riseDuration;

            if (sunTransform != null)
                sunTransform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        Level3SequenceManager.instance.TriggerEnding();
    }

}