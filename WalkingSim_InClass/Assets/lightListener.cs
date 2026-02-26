using UnityEditor.UI;
using UnityEngine;

public class LightListener : MonoBehaviour
{

    public Light sceneLight;

    public void OnEnable()
    {
        ButtonEvent.onButtonPressed += ChangeLight;
    }

    public void OnDisable()
    {
        ButtonEvent.onButtonPressed -= ChangeLight;
    }

    void ChangeLight()
    {
        sceneLight.color = Random.ColorHSV();
    }
}
