using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePlaceholderUI : MonoBehaviour
{
    public GameObject panelRoot;
    public TextMeshProUGUI titleText;
    public Button completePuzzleButton;

    private Action onPuzzleCompleted;

    public void Show(Action callback)
    {
        panelRoot.SetActive(true);
        titleText.text = "Assemble the EXIT sign";
        onPuzzleCompleted = callback;

        completePuzzleButton.onClick.RemoveAllListeners();
        completePuzzleButton.onClick.AddListener(CompletePuzzle);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideImmediate()
    {
        panelRoot.SetActive(false);
    }

    void CompletePuzzle()
    {
        onPuzzleCompleted?.Invoke();
    }
}