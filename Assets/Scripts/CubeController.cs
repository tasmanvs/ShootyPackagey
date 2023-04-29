using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject packagePrefab;
    public float shootForce = 600f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        RotateTowardsMouse();
        ShootCubeOnClick();
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        Vector3 direction = worldPos - transform.position;
        direction.y = 0; // Keep the cube level on the Y-axis

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }

    void ShootCubeOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPosition = transform.position + transform.forward * 2;
            GameObject newCube = Instantiate(packagePrefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = newCube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 forceDirection = (transform.forward + Vector3.up * 0.2f).normalized; // Add a slight upward angle to the force direction
                rb.AddForce(forceDirection * shootForce);
            }
            else
            {
                Debug.LogError("Rigidbody component not found on the instantiated cube.");
            }
        }
    }


}
