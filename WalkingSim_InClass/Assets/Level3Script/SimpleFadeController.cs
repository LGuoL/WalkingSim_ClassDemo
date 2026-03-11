using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    public IEnumerator FadeToWhite()
    {
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
        fadeImage.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            c.a = Mathf.Lerp(0f, 1f, t);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;
    }

}