using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1SequenceManager : MonoBehaviour
{
    public static Level1SequenceManager instance;
    public CameraShake cameraShake;
    public ScreenFade screenFade;
    
    [Header("Lighting")]
    public Light directionalLight;
    public float lightAfterAnswer = 0.6f;

    [Header("SFX")]
    public AudioSource sfxAudioSource;
    public AudioClip platformFallClip;

    [Header("Phone")]
    public AudioSource phoneAudioSource;
    public AudioClip ringingClip;
    public AudioClip pickedUpClip;
    public PhoneInteractable phoneInteractable;

    [Header("Choice UI")]
    public ChoicePanelUI choicePanelUI;

    [Header("Platform Pieces")]
    public FallingPlatformPiece[] platformPieces;

    [Header("Eyes")]
    public EyeSpawner eyeSpawner;

    [Header("Level Transition")]
    public string nextSceneName = "Level2";
    public float finalDelay = 5f;

    private int currentChoiceStep = 0;
    private bool phoneRinging = false;
    private bool phoneAnswered = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("AudioListener.volume = " + AudioListener.volume);
        if (phoneInteractable != null)
            phoneInteractable.SetCanInteract(false);

        if (choicePanelUI != null)
            choicePanelUI.HidePanel();
    }

    public void TriggerPhoneRinging()
    {
        if (phoneRinging) return;

        phoneRinging = true;

        if (phoneInteractable != null)
            phoneInteractable.SetCanInteract(true);

        if (phoneAudioSource != null && ringingClip != null)
        {
            phoneAudioSource.clip = ringingClip;
            phoneAudioSource.loop = true;
            phoneAudioSource.Play();
        }

        Debug.Log("Phone is ringing.");
    }

    public void AnswerPhone()
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = lightAfterAnswer;
        }
        if (cameraShake != null)
        {
            cameraShake.Shake(0.15f, 0.05f);
        }
        if (!phoneRinging || phoneAnswered) return;

        phoneAnswered = true;

        if (phoneAudioSource != null)
        {
            phoneAudioSource.Stop();

            if (pickedUpClip != null)
                phoneAudioSource.PlayOneShot(pickedUpClip);
        }

        Debug.Log("Phone answered. About to show first choice.");

        ShowNextChoice();
    }


    void ShowNextChoice()
    {
        if (choicePanelUI == null) return;

        if (currentChoiceStep == 0)
        {
            choicePanelUI.ShowChoices(
                "Do you want to continue?",
                "Yes",
                "No",
                OnChoiceSelected,
                currentChoiceStep
            );
        }
        else if (currentChoiceStep == 1)
        {
            choicePanelUI.ShowChoices(
                "Are you SURE?",
                "YES",
                "NOOOOOOOOOOOO?",
                OnChoiceSelected,
                currentChoiceStep
            );
        }
        else if (currentChoiceStep == 2)
        {
            choicePanelUI.ShowChoices(
                "There is no other choice.",
                "Accept",
                "Accept",
                OnChoiceSelected,
                currentChoiceStep
            );
        }
        else
        {
            StartCoroutine(FinalSequence());
        }
    }

    void OnChoiceSelected(int optionIndex)
    {
        Debug.Log("Player selected option: " + optionIndex);

        if (choicePanelUI != null)
            choicePanelUI.HidePanel();

        if (currentChoiceStep < platformPieces.Length && platformPieces[currentChoiceStep] != null)
        {
            platformPieces[currentChoiceStep].Fall();
        }

        if (sfxAudioSource != null && platformFallClip != null)
        {
            sfxAudioSource.PlayOneShot(platformFallClip);
        }

        if (cameraShake != null)
        {
            cameraShake.Shake(0.2f, 0.06f);
        }

        if (eyeSpawner != null)
        {
            eyeSpawner.SpawnEyesForStep(currentChoiceStep);
        }

        currentChoiceStep++;
        Invoke(nameof(ShowNextChoice), 1f);
    }

    IEnumerator FinalSequence()
    {
        Debug.Log("Final choice complete. Waiting before last platform falls...");

        yield return new WaitForSeconds(finalDelay);

        if (currentChoiceStep < platformPieces.Length && platformPieces[currentChoiceStep] != null)
        {
            platformPieces[currentChoiceStep].Fall();
        }

        if (cameraShake != null)
        {
            cameraShake.Shake(0.3f, 0.08f);
        }

        yield return new WaitForSeconds(1f);

        if (screenFade != null)
        {
            yield return StartCoroutine(screenFade.FadeOut(1.2f));
        }

        SceneManager.LoadScene(nextSceneName);
    }
}