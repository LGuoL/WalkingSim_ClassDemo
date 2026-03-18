using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2SequenceManager : MonoBehaviour
{
    public static Level2SequenceManager instance;
    [Header("Canvas Roots")]
    public GameObject wakeChoiceCanvasRoot;
    public GameObject noSequenceCanvasRoot;
    public GameObject puzzleCanvasRoot;

    [Header("Room Transition")]
    public Transform bigRoomSpawnPoint;
    public FlashTransition flashTransition;
    public AudioSource transitionAudioSource;
    public AudioClip transitionImpactClip;

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

        if (player != null)
            player.SetControlEnabled(false);

        // 播放冲击音
        if (transitionAudioSource != null && transitionImpactClip != null)
            transitionAudioSource.PlayOneShot(transitionImpactClip);

        // 白闪
        if (flashTransition != null)
            yield return StartCoroutine(flashTransition.PlayFlash());

        // 切换空间
        if (smallRoomRoot != null) smallRoomRoot.SetActive(false);
        if (bigRoomRoot != null) bigRoomRoot.SetActive(true);

        // 传送玩家到大房间入口
        if (player != null && bigRoomSpawnPoint != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();

            if (cc != null)
                cc.enabled = false;

            player.transform.position = bigRoomSpawnPoint.position;
            player.transform.rotation = bigRoomSpawnPoint.rotation;

            if (cc != null)
                cc.enabled = true;
        }

        // 激活高亮显示器交互
        if (puzzleMonitorInteractable != null)
            puzzleMonitorInteractable.SetCanInteract(true);

        // 稍微停顿一下，让玩家“看见新空间”
        yield return new WaitForSeconds(0.2f);

        // 恢复控制
        if (player != null)
            player.SetControlEnabled(true);

        Debug.Log("Big room activated with transition");
    }

     public void OpenPuzzleView()
    {
        Debug.Log("OpenPuzzleView called");

        // 彻底关闭旧UI
        if (wakeChoiceUI != null)
            wakeChoiceUI.HidePanelImmediate();

        if (noSequenceUI != null)
            noSequenceUI.HideImmediate();

        // 如果直接引用 Canvas一起关
        if (wakeChoiceCanvasRoot != null)
            wakeChoiceCanvasRoot.SetActive(false);

        if (noSequenceCanvasRoot != null)
            noSequenceCanvasRoot.SetActive(false);

        // 锁玩家控制
        if (player != null)
            player.SetControlEnabled(false);

        // 相机切到拼图视角
        if (playerCamera != null && puzzleCameraPoint != null)
        {
            playerCamera.transform.position = puzzleCameraPoint.position;
            playerCamera.transform.rotation = puzzleCameraPoint.rotation;
        }

        // 打开拼图UI
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