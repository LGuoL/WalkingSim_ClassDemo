using UnityEngine;

public class EyeSpawner : MonoBehaviour
{
    public GameObject eyePrefab;
    public Transform[] spawnPoints;

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

            Instantiate(eyePrefab, spawnPoints[spawnedCount].position, spawnPoints[spawnedCount].rotation);
            spawnedCount++;
        }

        Debug.Log("Spawned eyes for step: " + step);
    }
}