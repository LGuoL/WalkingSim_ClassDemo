using System.Collections;
using UnityEngine;
using TMPro;

public class MonitorBoot : MonoBehaviour
{
    public TextMeshPro screenText;

    [Header("Typing Settings")]
    public float typeDelay = 0.05f;
    public float lineDelay = 0.5f;

    private Coroutine bootRoutine;

    void Awake()
    {
        if (screenText != null)
            screenText.text = "";
    }

    public IEnumerator PlayBootSequenceRoutine()
    {
        if (screenText == null)
        {
            Debug.LogError("MonitorBoot: screenText is not assigned.");
            yield break;
        }

        if (bootRoutine != null)
            StopCoroutine(bootRoutine);

        yield return StartCoroutine(BootSequence());
    }

    IEnumerator BootSequence()
    {
        screenText.text = "";
        yield return StartCoroutine(TypeLine("BOOTING..."));
        yield return new WaitForSeconds(lineDelay);

        screenText.text = "";
        yield return StartCoroutine(TypeLine("LOADING SYSTEM"));
        yield return new WaitForSeconds(lineDelay);

        screenText.text = "";
        yield return StartCoroutine(TypeLine("READY"));
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator TypeLine(string fullText)
    {
        screenText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            screenText.text += fullText[i];
            yield return new WaitForSeconds(typeDelay);
        }
    }

    public void ClearScreen()
    {
        if (screenText != null)
            screenText.text = "";
    }
}