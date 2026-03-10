using System.Collections;
using TMPro;
using UnityEngine;

public class NoSequenceUI : MonoBehaviour
{
    public GameObject panelRoot;
    public TextMeshProUGUI bigText;

    public void HideImmediate()
    {
        panelRoot.SetActive(false);
    }

    public IEnumerator PlayNoSequence()
    {
        panelRoot.SetActive(true);

        bigText.fontSize = 36;
        bigText.text = "NO";
        yield return new WaitForSeconds(0.4f);

        bigText.text = "NO\nNO";
        yield return new WaitForSeconds(0.4f);

        bigText.text = "NO\nNO\nNO";
        yield return new WaitForSeconds(0.4f);

        bigText.fontSize = 90;
        bigText.text = "YES";
        yield return new WaitForSeconds(1.5f);

        panelRoot.SetActive(false);
    }
}