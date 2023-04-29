using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    private static MaterialPropertyBlock _propertyBlock;
    private static int _randomnessProperty = Shader.PropertyToID("_Randomness");

    private float[] direction = {-1, 0, 1};

    void Awake()
    {
        if(_propertyBlock == null)
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    { 
        // StartCoroutine("DoCheck");DoCheck();
    }

    IEnumerator DoCheck() {
        while(true) 
        {
            Debug.Log("--------do checking");
            _renderer.GetPropertyBlock(_propertyBlock);

            float rand = direction[Random.Range(0, 2)];
            // float rand = -1;

            Debug.Log($"rand: {rand}");
            _propertyBlock.SetFloat(_randomnessProperty, rand);
            _renderer.SetPropertyBlock(_propertyBlock);
            // execute block of code here
            yield return new WaitForSeconds(Random.Range(20, 30));
        }
    }

    public void UpdateDirection(float direction)
    {
            _renderer.GetPropertyBlock(_propertyBlock);

            _propertyBlock.SetFloat(_randomnessProperty, direction);
            _renderer.SetPropertyBlock(_propertyBlock);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
