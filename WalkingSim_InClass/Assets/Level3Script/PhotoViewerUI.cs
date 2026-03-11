using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotoViewerUI : MonoBehaviour
{
    public GameObject panelRoot;
    public TextMeshProUGUI monologueText;
    public Button closeButton;

    private Action onClosed;

    public void Show(string monologue, Action closeCallback)
    {
        panelRoot.SetActive(true);
        monologueText.text = monologue;
        onClosed = closeCallback;

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideImmediate()
    {
        panelRoot.SetActive(false);
    }

    void Close()
    {
        panelRoot.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        onClosed?.Invoke();
    }
}