using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject packagePrefab;
    [SerializeField] private GameObject ultraPackagePrefab;
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private Camera playerCamera;
    [SerializeField] public float shootForce;

    private Countdown GameManager;

    [SerializeField] private float ultraShootForce = 500f;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private AudioClip fling_sound;
    AudioSource audio_source;

    [SerializeField] private GameObject reticle;

    void Start()
    {
        playerCamera = Camera.main;
        GameManager = GameObject.Find("ScoreTracker").GetComponent<Countdown>();
        shootForce = GameManager.CannonSpeed;
        audio_source = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0.0f)
        {
            Vector3 targetPoint = FindTarget();

            Vector3 targetDirection = (targetPoint - cannonTransform.position).normalized;

            RotateCannon(targetDirection);

            if (Input.GetMouseButtonDown(0))
            {
                Shoot(targetDirection);
            }

            if(Input.GetMouseButtonDown(1))
            {
                UltraShoot(targetDirection);
            }
        }

    }

    private Vector3 FindTarget()
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

        reticle.transform.LookAt(playerCamera.transform);
        reticle.transform.position = targetPoint;

        return targetPoint;
    }


    private void RotateCannon(Vector3 targetDirection)
    {
        float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;

        // Set the cannon's Y rotation to the calculated angle
        cannonTransform.rotation =  Quaternion.Euler(-90, targetAngle - 90, 90);
    }

    private void Shoot(Vector3 targetDirection)
    {
        // Add Y offset to the spawn position
        float yOffset = 0.2f;
        Vector3 spawnPosition = targetDirection*2 + cannonTransform.position + new Vector3(0, yOffset, 0);

        GameObject package = Instantiate(packagePrefab, spawnPosition, Quaternion.identity);
        package.name = "SmallPackage";
        package.GetComponent<Rigidbody>().AddForce(targetDirection * shootForce, ForceMode.Impulse);

        float volume = Random.Range(0.5f, 1.0f);
        audio_source.pitch = Random.Range(0.6f, 1.4f);

        audio_source.PlayOneShot(fling_sound, volume);
    }

    private void UltraShoot(Vector3 targetDirection)
    {
        // Add Y offset to the spawn position
        float yOffset = 0.5f;
        Vector3 spawnPosition = targetDirection + cannonTransform.position + new Vector3(0, yOffset, 0);

        GameObject package = Instantiate(ultraPackagePrefab, spawnPosition, Quaternion.identity);
        package.name = "UltraPackage";
        package.GetComponent<Rigidbody>().AddForce(targetDirection * ultraShootForce, ForceMode.Impulse);
    
    
        float volume = Random.Range(0.5f, 1.0f);
        audio_source.pitch = Random.Range(0.6f, 1.4f);

        audio_source.PlayOneShot(fling_sound, volume);

        ScoreTracker.Instance.IncreaseScore(-5);
    }
}
