using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    public Transform Player;

    [SerializeField][Range(0.0f, 10.0f)]
    private float _offset;

    float current_cam_swing_x = 0;

    // Start is called before the first frame update
    void Start()
    {
        _offset = 6.5f;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        float mouse_normal_x = Mathf.Clamp((mousePos.x / Screen.width), 0f, 1f) - 0.5f;
        float mouse_normal_y = Mathf.Clamp((mousePos.y / Screen.height), 0f, 1f) - 0.5f;

        float target_cam_swing_x = -mouse_normal_x;

        float delta_swing = (target_cam_swing_x - current_cam_swing_x) * 1.9f * Time.deltaTime;
        current_cam_swing_x += delta_swing;

        Vector3 newPosition = Player.position - Player.forward * _offset;
        newPosition.y = transform.position.y;
        newPosition.x += current_cam_swing_x*5;

        transform.rotation = (Quaternion.Euler(0f , -current_cam_swing_x * 45f,  0f));

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10.0f);
    }
}
