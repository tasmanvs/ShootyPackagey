using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    public Transform Player;

    [SerializeField][Range(0.0f, 10.0f)]
    private float _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = 6.5f;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        var mouse_normal_x = (mousePos.x / Screen.width) - 0.5f;
        var mouse_normal_y = (mousePos.y / Screen.height) - 0.5f;

        Vector3 newPosition = Player.position - Player.forward * _offset;
        newPosition.y = transform.position.y;
        newPosition.x -= mouse_normal_x * 4;

        transform.rotation = (Quaternion.Euler(0f , mouse_normal_x * 45f,  0f));

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10.0f);
    }
}
