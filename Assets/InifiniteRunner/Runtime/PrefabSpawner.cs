using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs; // List of all prefabs

    [SerializeField]
    private List<float> difficultyRatings; // List of difficulty ratings corresponding to the prefabs list

    [SerializeField]
    private Transform grassTile; // Reference to the grass tile Transform

    private float elapsedTime = 0f; // Time elapsed since the start of the game

    [SerializeField]
    private float spawnInterval = 5f; // Interval between spawning prefabs

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Spawn prefabs periodically
        if (elapsedTime >= spawnInterval)
        {
            elapsedTime = 0f;
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        // Calculate current difficulty based on time
        float currentDifficulty = Mathf.Lerp(0, difficultyRatings[difficultyRatings.Count - 1], elapsedTime / 300f);

        // Find the prefab that matches the current difficulty
        GameObject prefabToSpawn = prefabs[0];
        float minDifficultyDifference = Mathf.Abs(currentDifficulty - difficultyRatings[0]);

        for (int i = 1; i < prefabs.Count; i++)
        {
            float difficultyDifference = Mathf.Abs(currentDifficulty - difficultyRatings[i]);
            if (difficultyDifference < minDifficultyDifference)
            {
                prefabToSpawn = prefabs[i];
                minDifficultyDifference = difficultyDifference;
            }
        }

        // Spawn the prefab on the grass tile
        Vector3 spawnPosition = new Vector3(grassTile.position.x, grassTile.position.y + 0.1f, grassTile.position.z);
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
