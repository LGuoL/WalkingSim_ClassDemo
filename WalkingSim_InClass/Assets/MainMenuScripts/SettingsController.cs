using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public Toggle fullscreenToggle;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        int savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1);

        if (volumeSlider != null) volumeSlider.value = savedVolume;
        if (sensitivitySlider != null) sensitivitySlider.value = savedSensitivity;
        if (fullscreenToggle != null) fullscreenToggle.isOn = savedFullscreen == 1;

        AudioListener.volume = savedVolume;
        Screen.fullScreen = savedFullscreen == 1;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}