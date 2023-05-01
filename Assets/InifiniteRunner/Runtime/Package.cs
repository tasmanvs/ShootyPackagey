using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    public Material[] random_materials;

    // Start is called before the first frame update
    void Start()
    {
        int material_index = Random.Range(0, random_materials.Length);
        GetComponentInChildren<MeshRenderer>().material = random_materials[material_index];
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }
}
