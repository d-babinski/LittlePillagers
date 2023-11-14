using UnityEngine;

public class WorldToScreenPosition : MonoBehaviour
{
    [Header("This requires rect transform to be anchored bottom right with both pivots set to 0.5")]
    [SerializeField] private Vector3Variable worldPosition = null;

    private RectTransform rectTransform = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(worldPosition.Value)/rectTransform.lossyScale.x;
    }
}
