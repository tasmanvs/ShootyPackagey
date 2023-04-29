using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject packagePrefab;
    public float shootForce = 600f;

    private Camera mainCamera;

    private Quaternion originalRotation;
    private Quaternion mouseRotation;

    void Start()
    {
        mainCamera = Camera.main;
        originalRotation = transform.rotation;
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
        // direction.y = 0; // Keep the cube level on the Y-axis

        mouseRotation = Quaternion.LookRotation(direction);

        // Set the cannon rotation to point to the mouse, keeping in mind the original rotation
        transform.rotation = mouseRotation * originalRotation;
    }

    void ShootCubeOnClick()
    {
        // TODO(Tasman): Fix this logic, it works but it's janky.
        // I'm trying to keep in mind the original location of the cannon when pointing and shooting.
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        Vector3 shootDirection = worldPos - transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPosition = transform.position + shootDirection.normalized * 0.5f;
            GameObject newCube = Instantiate(packagePrefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = newCube.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 forceDirection = (shootDirection).normalized; // Add a slight upward angle to the force direction
                rb.AddForce(forceDirection * shootForce);
            }
            else
            {
                Debug.LogError("Rigidbody component not found on the instantiated cube.");
            }
        }
    }


}
