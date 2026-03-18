using UnityEngine;

public class MonitorPulse : MonoBehaviour
{
    public Renderer targetRenderer;
    public string emissionProperty = "_EmissionColor";
    public Color minColor = Color.gray;
    public Color maxColor = Color.white;
    public float speed = 3f;

    void Update()
    {
        if (targetRenderer == null) return;

        float t = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f;
        Color current = Color.Lerp(minColor, maxColor, t);

        if (targetRenderer.material.HasProperty(emissionProperty))
        {
            targetRenderer.material.SetColor(emissionProperty, current);
        }
    }
}