using System.Collections;
using TMPro;
using UnityEngine;

public class MotherDialogueUI : MonoBehaviour
{
    public GameObject panelRoot;
    public TextMeshProUGUI dialogueText;

    public IEnumerator ShowDialogue(string text, float duration)
    {
        panelRoot.SetActive(true);
        dialogueText.text = text;
        yield return new WaitForSeconds(duration);
        panelRoot.SetActive(false);
    }

    public IEnumerator ShowDialogueInterrupted(string text, float duration)
    {
        panelRoot.SetActive(true);
        dialogueText.text = text;
        yield return new WaitForSeconds(duration);
        panelRoot.SetActive(false);
    }
}