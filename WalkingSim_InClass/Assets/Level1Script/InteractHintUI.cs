using TMPro;
using UnityEngine;

public class InteractHintUI : MonoBehaviour
{
    public GameObject hintRoot;
    public TextMeshProUGUI hintText;

    public void ShowHint(string text)
    {
        hintRoot.SetActive(true);
        hintText.text = text;
    }

    public void HideHint()
    {
        hintRoot.SetActive(false);
    }
}