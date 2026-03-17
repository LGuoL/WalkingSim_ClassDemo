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

        bigText.fontSize = 60;
        bigText.color = Color.white;
        bigText.text = "NO";
        yield return new WaitForSeconds(0.5f);

        bigText.fontSize = 42;
        bigText.color = Color.red;
        bigText.text = "INVALID INPUT";
        yield return new WaitForSeconds(0.7f);

        bigText.fontSize = 42;
        bigText.color = Color.white;
        bigText.text = "CORRECTING RESPONSE";
        yield return new WaitForSeconds(0.7f);

        bigText.fontSize = 100;
        bigText.color = Color.white;
        bigText.text = "YES";
        yield return new WaitForSeconds(1.2f);

        panelRoot.SetActive(false);
    }
}