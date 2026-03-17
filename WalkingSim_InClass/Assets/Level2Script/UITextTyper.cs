using System.Collections;
using UnityEngine;
using TMPro;

public class UITextTyper : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    public float typeDelay = 0.03f;

    public void PlayText(string fullText)
    {
        StopAllCoroutines();
        StartCoroutine(TypeRoutine(fullText));
    }

    IEnumerator TypeRoutine(string fullText)
    {
        targetText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            targetText.text += fullText[i];
            yield return new WaitForSeconds(typeDelay);
        }
    }
}