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

    public void UpdateDirection(float direction)
    {
            _renderer.GetPropertyBlock(_propertyBlock);

            _propertyBlock.SetFloat(_randomnessProperty, direction);
            _renderer.SetPropertyBlock(_propertyBlock);
    }

}
