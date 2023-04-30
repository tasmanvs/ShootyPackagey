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
            int idx = Random.Range(0, 2);
            GameObject tile = InitTile(idx);
            Roads.Enqueue(tile);
        }
    }

    GameObject InitTile(int idx)
    {
            GameObject tile = Instantiate(RoadTiles[idx]);
            tile.transform.parent = SurfaceParent;
            tile.transform.position = new Vector3(_initPosition.x, SurfaceParent.position.y, _initPosition.z);
            _initPosition += tile.transform.forward * _roadLength;
            tile.SetActive(true);

            _lastRoadTile = tile;

            tile.name = RoadTiles[idx].name + (++_curTileNums).ToString();

            return tile;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Abs(Roads.Peek().transform.position.z - PlayerPosition.position.z);
        if(dist > 10)
        {
            GameObject tile = Roads.Dequeue();
            Destroy(tile);

            int idx = Random.Range(0, 2);
            GameObject newTile = InitTile(idx);;
            Roads.Enqueue(newTile);
        }

        float curve = Mathf.Lerp(0, 0, (Mathf.Sin(Time.time / 5.0f) + 1.0f) / 2.0f);


            foreach(GameObject tile in Roads)
            {
                tile.GetComponent<Road>().UpdateDirection(curve);
            }


    }
}
