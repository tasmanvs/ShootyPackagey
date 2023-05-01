using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinExplosion : MonoBehaviour
{
    [SerializeField]
    GameObject Coin;

    [SerializeField][Range(0.0f, 10.0f)]
    float Force = 10.0f;

    [SerializeField]
    List<Transform> pieces;

    [SerializeField]
    float distanceThreshold = 1f;

    [SerializeField]
    private int scoreValue = 5;

    private Dictionary<Transform, Vector3> initialPositions;
    private bool coinsSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        Force = 5.0f;
        initialPositions = new Dictionary<Transform, Vector3>();

        foreach (Transform piece in pieces)
        {
            initialPositions[piece] = piece.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!coinsSpawned && AllPiecesMoved())
        {
            SpawnCoins();
            coinsSpawned = true;
        }
    }

    private bool AllPiecesMoved()
    {
        int total_distance = 0;
        foreach (Transform piece in pieces)
        {
            total_distance += (int)Vector3.Distance(piece.position, initialPositions[piece]);
        }

        return (total_distance > distanceThreshold);
    }

    private void SpawnCoins()
    {
        ScoreTracker.Instance.IncreaseScore(scoreValue);

        // Spawn position is the center of the pieces
        Vector3 spawnPosition = Vector3.zero;
        foreach (Transform piece in pieces)
        {
            spawnPosition += piece.position;
        }

        spawnPosition /= pieces.Count;

        // Set the cone angle range
        float minAngle = -30f;
        float maxAngle = 30f;

        for (int i = 0; i < scoreValue; i++)
        {
            GameObject coin = Instantiate(Coin, spawnPosition, Quaternion.identity);
            Rigidbody coinRb = coin.GetComponent<Rigidbody>();

            Vector3 forceDirection = Vector3.zero;

            // Also apply random force in the y direction
            forceDirection.y = Random.Range(0.5f, 1f);
            forceDirection.x = Random.Range(-0.2f, 0.2f);
            forceDirection.z = Random.Range(-0.2f, 0.2f);

            coinRb.AddForce(forceDirection * Force, ForceMode.Impulse);
        }
    }
}
