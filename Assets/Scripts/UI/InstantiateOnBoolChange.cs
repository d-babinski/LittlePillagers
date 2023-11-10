using UnityEngine;

public class InstantiateOnBoolChange : MonoBehaviour
{
    [SerializeField] private BoolVariable isIsleHovered = null;
    [SerializeField] private VisibilityTweenPool spawner = null;

    private bool wasTruePreviousFrame = false;

    private void Update()
    {
        if (wasTruePreviousFrame == isIsleHovered.Value)
        {
            return;
        }

        wasTruePreviousFrame = isIsleHovered.Value;
        
        if (isIsleHovered.Value)
        {
            spawner.Show();
        }
        else
        {
            spawner.Hide();
        }
    }
}
