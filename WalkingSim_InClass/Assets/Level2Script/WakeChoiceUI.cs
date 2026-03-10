using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WakeChoiceUI : MonoBehaviour
{
    public GameObject panelRoot;
    public TextMeshProUGUI questionText;
    public Button yesButton;
    public Button noButton;

    private Action<bool> onChoiceSelected;

    public void Show(string question, Action<bool> callback)
    {
        panelRoot.SetActive(true);
        questionText.text = question;
        onChoiceSelected = callback;

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() => SelectChoice(true));
        noButton.onClick.AddListener(() => SelectChoice(false));

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("WakeChoiceUI shown");
    }

    public void HidePanelImmediate()
    {
        panelRoot.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SelectChoice(bool choseYes)
    {
        onChoiceSelected?.Invoke(choseYes);
    }
}