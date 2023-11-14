using UnityEngine;

public class RadioButtonGroup : MonoBehaviour
{
    public int CurrentChosenOption => currentChosenOption;

    private RadioButton[] radioButtons = null;
    private int currentChosenOption = 0;

    private void Awake()
    {
        radioButtons = GetComponentsInChildren<RadioButton>();
    }

    private void Start()
    {
        foreach (RadioButton _uiToggle in radioButtons)
        {
            _uiToggle.OnRadioButtonClicked.AddListener(changeSelection);
        }

        radioButtons[currentChosenOption].SelectRadioButton();

    }

    private void OnDestroy()
    {
        foreach (RadioButton _uiToggle in radioButtons)
        {
            _uiToggle.OnRadioButtonClicked.RemoveListener(changeSelection);
        }
    }

    private void changeSelection()
    {
        radioButtons[currentChosenOption].UnselectRadioButton();

        for (int i = 0; i < radioButtons.Length; i++)
        {
            if (radioButtons[i] == false)
            {
                currentChosenOption = i;
            }
        }
    }
}
