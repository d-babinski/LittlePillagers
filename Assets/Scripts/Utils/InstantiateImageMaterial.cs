using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InstantiateImageMaterial : MonoBehaviour
{
    private void Awake()
    {
        Image _image = GetComponent<Image>();
        _image.material = Instantiate(_image.material);
    }
}
