using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField]
    GameObject Coin;

    [SerializeField][Range(0.0f, 10.0f)]
    float Force;

    [SerializeField]
    GameObject SmallPackage;
    int IsActive;
    // Start is called before the first frame update
    void Start()
    {
        IsActive = Random.Range(0, 2);
        Force = 5.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectCollision(Collision collider)
    {
        if(collider.gameObject.name == "SmallPackage")
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
            Instantiate(Coin, spawnPosition, Quaternion.identity).GetComponent<Rigidbody>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 1) * Force);
        }

    }
}
