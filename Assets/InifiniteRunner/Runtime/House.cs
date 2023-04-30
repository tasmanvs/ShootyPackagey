using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField]
    GameObject Coin;

    [SerializeField][Range(0.0f, 10.0f)]
    float Force;

    [SerializeField]
    GameObject SmallPackage;

    [SerializeField]
    List<Transform> housePieces;

    [SerializeField]
    float distanceThreshold = 1f;

    private Dictionary<Transform, Vector3> initialPositions;
    private bool coinsSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        Force = 5.0f;
        initialPositions = new Dictionary<Transform, Vector3>();

        foreach (Transform piece in housePieces)
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
        foreach (Transform piece in housePieces)
        {
            if (Vector3.Distance(initialPositions[piece], piece.position) < distanceThreshold)
            {
                return false;
            }
        }
        return true;
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
            Instantiate(Coin, spawnPosition, Quaternion.identity).GetComponent<Rigidbody>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 1) * Force);
        }
    }
}
