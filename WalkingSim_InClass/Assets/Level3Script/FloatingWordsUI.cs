using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingWordsUI : MonoBehaviour
{
    public TextMeshProUGUI floatingText;

    private void Start()
    {
        if (floatingText != null)
            floatingText.gameObject.SetActive(false);
    }

    public IEnumerator ShowLine(string text, float holdTime)
    {
        if (floatingText == null)
        {
            Debug.LogError("FloatingWordsUI: floatingText is NULL");
            yield break;
        }

        floatingText.gameObject.SetActive(true);
        floatingText.text = text;

        yield return new WaitForSeconds(holdTime);

        floatingText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }

}