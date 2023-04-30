using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField][Range(0.0f, 5.0f)] 
    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        _speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
