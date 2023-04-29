using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject packagePrefab;
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float shootForce = 20f;


    // Update is called once per frame
    void Update()
    {
        RotateCannon();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void RotateCannon()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            float defaultDistance = 50f;
            targetPoint = ray.GetPoint(defaultDistance);
        }

        Vector3 targetDirection = (targetPoint - cannonTransform.position).normalized;
        float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;

        // Set the cannon's Y rotation to the calculated angle
        cannonTransform.rotation =  Quaternion.Euler(-90, targetAngle - 90, 90);
    }

    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            float defaultDistance = 50f;
            targetPoint = ray.GetPoint(defaultDistance);
        }

        Vector3 targetDirection = (targetPoint - cannonTransform.position).normalized;

        // Add Y offset to the spawn position
        float yOffset = 0.2f;
        Vector3 spawnPosition = cannonTransform.position + new Vector3(0, yOffset, 0);

        GameObject package = Instantiate(packagePrefab, spawnPosition, Quaternion.identity);
        package.GetComponent<Rigidbody>().AddForce(targetDirection * shootForce, ForceMode.Impulse);
    }
}
