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
    private int _maxTileNums = 10;
    private int _curTileNums;

    private int _roadLength = 5;

    private Vector3 _initPosition;

    private GameObject _lastRoadTile;

    private float _timer = 0f;
    private float updateTime = 3.0f;

    [SerializeField][Range(-3.0f, 3.0f)]
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
    }

    GameObject InitTile()
    {
            GameObject tile = Instantiate(RoadTile);
            tile.transform.position = new Vector3(_initPosition.x, -0.5f, _initPosition.z);
            _initPosition += tile.transform.up * _roadLength;
            tile.SetActive(true);

            _lastRoadTile = tile;

            tile.name = (++_curTileNums).ToString();

            return tile;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Abs(Roads.Peek().transform.position.z - PlayerPosition.position.z);
        if(dist > 5)
        {
            GameObject tile = Roads.Dequeue();
            Destroy(tile);

            GameObject newTile = InitTile();
            Roads.Enqueue(newTile);
        }

        float curve = Mathf.Lerp(-3, 3, (Mathf.Sin(Time.time / 5.0f) + 1.0f) / 2.0f);


            foreach(GameObject tile in Roads)
            {
                tile.GetComponent<Road>().UpdateDirection(curve);
            }


    }
}
