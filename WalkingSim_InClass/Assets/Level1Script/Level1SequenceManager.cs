using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1SequenceManager : MonoBehaviour
{
    public static Level1SequenceManager instance;

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
        Debug.Log("ShowNextChoice called. currentChoiceStep = " + currentChoiceStep);

        if (choicePanelUI == null)
        {
            Debug.LogError("choicePanelUI is NULL");
            return;
        }

        if (currentChoiceStep == 0)
        {
            Debug.Log("Showing first choice UI");
            choicePanelUI.ShowChoices(
                "Do you want to continue?",
                "Yes",
                "No",
                OnChoiceSelected
            );
        }
        else if (currentChoiceStep == 1)
        {
            Debug.Log("Showing second choice UI");
            choicePanelUI.ShowChoices(
                "Are you sure?",
                "Keep going",
                "Turn back",
                OnChoiceSelected
            );
        }
        else if (currentChoiceStep == 2)
        {
            Debug.Log("Showing third choice UI");
            choicePanelUI.ShowChoices(
                "Final choice.",
                "Accept",
                "Refuse",
                OnChoiceSelected
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

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(nextSceneName);
    }
}