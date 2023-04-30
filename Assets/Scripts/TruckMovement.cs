using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float turnSpeed = 50f;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = Input.GetAxis("Vertical");
        float turnDirection = Input.GetAxis("Horizontal");

        Move(moveDirection);
        Turn(turnDirection);
    }

    private void Move(float direction)
    {
        Vector3 moveVector = transform.forward * direction * moveSpeed * Time.deltaTime;
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
    }

    private void Turn(float direction)
    {
        float turnAngle = direction * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, turnAngle, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
    }
}
