using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnShine : MonoBehaviour
{
    //TODO: Make a propertyblock
    
    private const string SHINE_PARAM_NAME = "_Shine";

    public void SetShine(SpriteRenderer _renderer)
    {
        _renderer.material.SetFloat(SHINE_PARAM_NAME, 1f);
    }

    public void SetNoShine(SpriteRenderer _renderer)
    {
        _renderer.material.SetFloat(SHINE_PARAM_NAME, 0f);
    }
}
