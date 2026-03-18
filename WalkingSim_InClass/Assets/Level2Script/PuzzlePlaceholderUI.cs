using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePlaceholderUI : MonoBehaviour
{
    public GameObject panelRoot;
    public TextMeshProUGUI titleText;
    public Button completePuzzleButton;
    public UITextTyper textTyper;

    private Action onPuzzleCompleted;

    public void Show(Action callback)
    {
        panelRoot.SetActive(true);
        panelRoot.transform.SetAsLastSibling();
        completePuzzleButton.transform.SetAsLastSibling();

        onPuzzleCompleted = callback;

        if (textTyper != null)
            textTyper.PlayText("ASSEMBLE THE EXIT SIGN");
        else
            titleText.text = "ASSEMBLE THE EXIT SIGN";

        completePuzzleButton.onClick.RemoveAllListeners();
        completePuzzleButton.onClick.AddListener(CompletePuzzle);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("PuzzlePlaceholderUI shown");
    }

    public void HideImmediate()
    {
        panelRoot.SetActive(false);
    }

    void CompletePuzzle()
    {
        Debug.Log("CompletePuzzle button clicked");
        onPuzzleCompleted?.Invoke();
    }
}