using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelUI : MonoBehaviour
{
    public GameObject panelRoot;
    public TextMeshProUGUI questionText;

    public Button leftButton;
    public TextMeshProUGUI leftButtonText;

    public Button rightButton;
    public TextMeshProUGUI rightButtonText;

    private Action<int> onChoiceSelected;

    public void ShowChoices(string question, string leftText, string rightText, Action<int> callback)
    {
        panelRoot.SetActive(true);

        questionText.text = question;
        leftButtonText.text = leftText;
        rightButtonText.text = rightText;

        onChoiceSelected = callback;

        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        leftButton.onClick.AddListener(() => SelectChoice(0));
        rightButton.onClick.AddListener(() => SelectChoice(1));

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HidePanel()
    {
        panelRoot.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SelectChoice(int index)
    {
        onChoiceSelected?.Invoke(index);
    }
}