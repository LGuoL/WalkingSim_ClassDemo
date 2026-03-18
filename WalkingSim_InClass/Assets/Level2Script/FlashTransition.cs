using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashTransition : MonoBehaviour
{
    public Image flashImage;

    public IEnumerator PlayFlash(float fadeInTime = 0.08f, float holdTime = 0.08f, float fadeOutTime = 0.2f)
    {
        if (flashImage == null) yield break;

        Color c = flashImage.color;

        // Fade in
        float timer = 0f;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float t = timer / fadeInTime;
            c.a = Mathf.Lerp(0f, 1f, t);
            flashImage.color = c;
            yield return null;
        }

        c.a = 1f;
        flashImage.color = c;

        yield return new WaitForSeconds(holdTime);

        // Fade out
        timer = 0f;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            float t = timer / fadeOutTime;
            c.a = Mathf.Lerp(1f, 0f, t);
            flashImage.color = c;
            yield return null;
        }

        c.a = 0f;
        flashImage.color = c;
    }
}