using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomize_material : MonoBehaviour
{
    public Material[] random_materials;

    // Start is called before the first frame update
    void Start()
    {
        int material_index = Random.Range(0, random_materials.Length);

        foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = random_materials[material_index];
        }
    }
}
