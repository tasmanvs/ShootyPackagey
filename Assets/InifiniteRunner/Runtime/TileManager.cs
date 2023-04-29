using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    GameObject RoadTile;
    [SerializeField]
    Transform PlayerPosition;
    private Queue<GameObject> Roads;
    private int _maxTileNums = 4;
    private int _curTileNums;

    private int _roadLength = 10;

    private Vector3 _initPosition;

    private GameObject _lastRoadTile;

    private float _curCurveDirection;

    // Start is called before the first frame update
    void Awake()
    {
        _initPosition = PlayerPosition.position;
        Roads = new Queue<GameObject>();

    }
    void Start()
    {
        while(Roads.Count < _maxTileNums)
        {
            GameObject tile = InitTile();
            Roads.Enqueue(tile);
        }

        StartCoroutine("UpdateDirection");;
    }

    GameObject InitTile()
    {
            GameObject tile = Instantiate(RoadTile);
            tile.transform.position = new Vector3(_initPosition.x, -0.5f, _initPosition.z);
            _initPosition += tile.transform.up * _roadLength;
            tile.SetActive(true);

            _lastRoadTile = tile;

            tile.name = (++_curTileNums).ToString();

            tile.GetComponent<Road>().UpdateDirection(_curCurveDirection);
            return tile;
    }

    IEnumerator UpdateDirection() {
        while(true) 
        {
            _curCurveDirection = Random.Range(-10, 10) / 10.0f;
            foreach(GameObject tile in Roads)
            {
                tile.GetComponent<Road>().UpdateDirection(_curCurveDirection);
            }

            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dist = _lastRoadTile.transform.position.z - PlayerPosition.position.z;
        if(dist < 25)
        {
            GameObject tile = Roads.Dequeue();
            Destroy(tile);

            GameObject newTile = InitTile();
            Roads.Enqueue(newTile);
        }
    }
}
