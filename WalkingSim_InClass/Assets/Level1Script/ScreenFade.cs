using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;

    public IEnumerator FadeOut(float duration)
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < duration)
        {
            float t = timer / duration;
            fadeImage.color = new Color(color.r, color.g, color.b, t);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, 1f);
    }
}