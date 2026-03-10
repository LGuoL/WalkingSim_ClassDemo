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
    private Player player;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        HidePanel();
    }

    public void ShowChoices(string question, string leftText, string rightText, Action<int> callback)
    {
        Debug.Log("ChoicePanelUI.ShowChoices called");

        if (player == null)
            player = FindFirstObjectByType<Player>();

        if (panelRoot == null)
        {
            Debug.LogError("panelRoot is NULL");
            return;
        }

        if (questionText == null)
        {
            Debug.LogError("questionText is NULL");
            return;
        }

        if (leftButton == null || rightButton == null)
        {
            Debug.LogError("Button reference is NULL");
            return;
        }

        if (leftButtonText == null || rightButtonText == null)
        {
            Debug.LogError("Button text reference is NULL");
            return;
        }

        panelRoot.SetActive(true);

        questionText.text = question;
        leftButtonText.text = leftText;
        rightButtonText.text = rightText;

        onChoiceSelected = callback;

        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        leftButton.onClick.AddListener(() => SelectChoice(0));
        rightButton.onClick.AddListener(() => SelectChoice(1));

        if (player != null)
            player.SetControlEnabled(false);

        Debug.Log("Choice panel activated successfully");
    }

    public void HidePanel()
    {
        panelRoot.SetActive(false);

        if (player == null)
            player = FindFirstObjectByType<Player>();

        if (player != null)
            player.SetControlEnabled(true);
    }

    void SelectChoice(int index)
    {
        onChoiceSelected?.Invoke(index);
    }
}