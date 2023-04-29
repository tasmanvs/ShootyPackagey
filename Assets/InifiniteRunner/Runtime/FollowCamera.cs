using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    public Transform Player;

    [SerializeField][Range(0.0f, 5.0f)]
    private float _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Player.position - Player.forward * _offset;
        newPosition.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10.0f);
    }
}
