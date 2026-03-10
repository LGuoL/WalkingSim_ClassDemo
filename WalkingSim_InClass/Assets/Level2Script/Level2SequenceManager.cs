using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2SequenceManager : MonoBehaviour
{
    public static Level2SequenceManager instance;

    [Header("Wake Choice")]
    public WakeChoiceUI wakeChoiceUI;
    public NoSequenceUI noSequenceUI;

    [Header("Room States")]
    public GameObject smallRoomRoot;
    public GameObject bigRoomRoot;

    [Header("Player")]
    public Player player;
    public Camera playerCamera;
    public Transform puzzleCameraPoint;

    [Header("Puzzle")]
    public PuzzlePlaceholderUI puzzlePlaceholderUI;
    public PuzzleMonitorInteractable puzzleMonitorInteractable;

    [Header("Fade")]
    public FadeToWhite fadeToWhite;
    public string nextSceneName = "Level3";

    private bool wakeChoiceFinished = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (wakeChoiceUI != null) wakeChoiceUI.HidePanelImmediate();
        if (noSequenceUI != null) noSequenceUI.HideImmediate();
        if (puzzlePlaceholderUI != null) puzzlePlaceholderUI.HideImmediate();

        if (bigRoomRoot != null) bigRoomRoot.SetActive(false);

        if (puzzleMonitorInteractable != null)
            puzzleMonitorInteractable.SetCanInteract(false);

        if (wakeChoiceUI == null)
            Debug.LogError("Level2SequenceManager: wakeChoiceUI is missing");

        if (noSequenceUI == null)
            Debug.LogError("Level2SequenceManager: noSequenceUI is missing");

        if (puzzlePlaceholderUI == null)
            Debug.LogError("Level2SequenceManager: puzzlePlaceholderUI is missing");

        if (puzzleMonitorInteractable == null)
            Debug.LogError("Level2SequenceManager: puzzleMonitorInteractable is missing");
    }

    public void OpenWakeChoice()
    {
        if (wakeChoiceFinished) return;

        Debug.Log("OpenWakeChoice called");

        if (player != null)
            player.SetControlEnabled(false);

        if (wakeChoiceUI != null)
        {
            wakeChoiceUI.Show("Do you want to wake up?", OnWakeChoiceSelected);
        }
    }

    void OnWakeChoiceSelected(bool choseYes)
    {
        Debug.Log("Wake choice selected: " + (choseYes ? "YES" : "NO"));

        if (wakeChoiceUI != null)
            wakeChoiceUI.HidePanelImmediate();

        if (choseYes)
        {
            StartCoroutine(ProceedToExpandedRoom());
        }
        else
        {
            StartCoroutine(HandleNoSequenceThenProceed());
        }
    }

    IEnumerator HandleNoSequenceThenProceed()
    {
        if (noSequenceUI != null)
            yield return StartCoroutine(noSequenceUI.PlayNoSequence());

        yield return StartCoroutine(ProceedToExpandedRoom());
    }

    IEnumerator ProceedToExpandedRoom()
    {
        wakeChoiceFinished = true;

        yield return new WaitForSeconds(0.5f);

        if (smallRoomRoot != null) smallRoomRoot.SetActive(false);
        if (bigRoomRoot != null) bigRoomRoot.SetActive(true);

        if (puzzleMonitorInteractable != null)
            puzzleMonitorInteractable.SetCanInteract(true);

        if (player != null)
            player.SetControlEnabled(true);

        Debug.Log("Big room activated");
    }

    public void OpenPuzzleView()
    {
        Debug.Log("OpenPuzzleView called");

        if (player != null)
            player.SetControlEnabled(false);

        if (playerCamera != null && puzzleCameraPoint != null)
        {
            playerCamera.transform.position = puzzleCameraPoint.position;
            playerCamera.transform.rotation = puzzleCameraPoint.rotation;
        }

        if (puzzlePlaceholderUI != null)
            puzzlePlaceholderUI.Show(OnPuzzleCompleted);
    }

    void OnPuzzleCompleted()
    {
        Debug.Log("Puzzle completed");

        if (puzzlePlaceholderUI != null)
            puzzlePlaceholderUI.HideImmediate();

        StartCoroutine(FinishLevel2());
    }

    IEnumerator FinishLevel2()
    {
        if (fadeToWhite != null)
            yield return StartCoroutine(fadeToWhite.PlayFade());

        SceneManager.LoadScene(nextSceneName);
    }
}