using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutlineThickness : MonoBehaviour
{
    //TODO: Make a propertyblock
    
    private const string THICKNESS_PARAM_NAME = "_Thickness";

    public void SetOutline(SpriteRenderer _renderer)
    {
        _renderer.material.SetFloat(THICKNESS_PARAM_NAME, 1f);
    }

    public void SetNoOutline(SpriteRenderer _renderer)
    {
        _renderer.material.SetFloat(THICKNESS_PARAM_NAME, 0f);
    }
}
