using System.Collections;
using UnityEngine;

public class EyeSpawner : MonoBehaviour
{
    public GameObject eyePrefab;
    public Transform[] spawnPoints;
    public Transform playerTarget;

    private int spawnedCount = 0;

    public void SpawnEyesForStep(int step)
    {
        int amountToSpawn = 0;

        if (step == 0) amountToSpawn = 2;
        else if (step == 1) amountToSpawn = 4;
        else if (step == 2) amountToSpawn = 6;

        for (int i = 0; i < amountToSpawn; i++)
        {
            if (spawnedCount >= spawnPoints.Length) break;

            GameObject eye = Instantiate(
                eyePrefab,
                spawnPoints[spawnedCount].position,
                spawnPoints[spawnedCount].rotation
            );

            if (playerTarget != null)
            {
                eye.transform.LookAt(playerTarget);
            }

            StartCoroutine(ScaleIn(eye.transform));

            spawnedCount++;
        }

        Debug.Log("Spawned eyes for step: " + step);
    }

    IEnumerator ScaleIn(Transform target)
    {
        Vector3 finalScale = target.localScale;
        target.localScale = Vector3.zero;

        float timer = 0f;
        float duration = 0.3f;

        while (timer < duration)
        {
            target.localScale = Vector3.Lerp(Vector3.zero, finalScale, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        target.localScale = finalScale;
    }
}