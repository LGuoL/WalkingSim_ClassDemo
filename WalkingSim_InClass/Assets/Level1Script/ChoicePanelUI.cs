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

    public RectTransform panelRect;
    public Image panelImage;

    private Action<int> onChoiceSelected;
    private Player player;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    public void ShowChoices(string question, string leftText, string rightText, Action<int> callback, int step)
    {
        panelRoot.SetActive(true);

        questionText.text = question;
        leftButtonText.text = leftText;
        rightButtonText.text = rightText;

        ApplyStyleByStep(step);

        onChoiceSelected = callback;

        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        leftButton.onClick.AddListener(() => SelectChoice(0));
        rightButton.onClick.AddListener(() => SelectChoice(1));

        if (player != null)
        {
            player.SetUIMode(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void ApplyStyleByStep(int step)
    {
        if (panelRect == null || panelImage == null) return;

        if (step == 0)
        {
            panelRect.anchoredPosition = new Vector2(0, 80);
            panelRect.sizeDelta = new Vector2(1200, 250);
            questionText.fontSize = 36;
            panelImage.color = new Color(0, 0, 0, 0.6f);
        }
        else if (step == 1)
        {
            panelRect.anchoredPosition = new Vector2(0, 130);
            panelRect.sizeDelta = new Vector2(1300, 280);
            questionText.fontSize = 42;
            panelImage.color = new Color(0, 0, 0, 0.75f);
        }
        else if (step == 2)
        {
            panelRect.anchoredPosition = new Vector2(0, 200);
            panelRect.sizeDelta = new Vector2(1400, 320);
            questionText.fontSize = 50;
            panelImage.color = new Color(0, 0, 0, 0.9f);
        }
    }

    public void HidePanel()
    {
        panelRoot.SetActive(false);

        if (player != null)
        {
            player.SetUIMode(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void SelectChoice(int index)
    {
        onChoiceSelected?.Invoke(index);
    }
}