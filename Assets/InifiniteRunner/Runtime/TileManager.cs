using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] RoadTiles;
    [SerializeField]
    Transform PlayerPosition;
    [SerializeField]
    Transform SurfaceParent;
    private Queue<GameObject> Roads;
    private int _maxTileNums = 20;
    private int _curTileNums;

    private int _roadLength = 5;

    private Vector3 _initPosition;

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
            int idx = Random.Range(0, RoadTiles.Length);
            InitTile(idx); 
        }
    }

    void InitTile(int idx)
    {
        GameObject tile = Instantiate(RoadTiles[idx]);
        tile.transform.parent = SurfaceParent;
        tile.transform.position = new Vector3(_initPosition.x, SurfaceParent.position.y, _initPosition.z);
        tile.SetActive(true);
        tile.name = RoadTiles[idx].name + (++_curTileNums).ToString();
        Roads.Enqueue(tile);

        _initPosition += tile.transform.forward * _roadLength;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Abs(Roads.Peek().transform.position.z - PlayerPosition.position.z);
        if(dist > 10)
        {
            GameObject tile = Roads.Dequeue();
            Destroy(tile);

            int idx = Random.Range(0, RoadTiles.Length);
            InitTile(idx);
        }

    }
}
