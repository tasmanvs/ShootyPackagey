using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField][Range(0.001f, 1)] 
    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _speed = 0.005f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed;
        // Debug.Log($"{transform.position}, {_speed}");
    }
}
