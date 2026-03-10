using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3SequenceManager : MonoBehaviour
{
    public static Level3SequenceManager instance;

    [Header("UI")]
    public TaskUIManager taskUI;
    public MotherDialogueUI motherDialogueUI;
    public FloatingWordsUI floatingWordsUI;
    public PhotoViewerUI photoViewerUI;
    public SimpleFadeController fadeController;

    [Header("Task 1 - Boxes")]
    public GameObject boxPrefab;
    public Transform[] boxSpawnPoints;
    public BoxPlacementZone[] boxPlacementZones;
    public Transform spawnedBoxesRoot;

    [Header("Task 2 - Cooking")]
    public GameObject recipeNote;
    public IngredientPickup[] ingredients;
    public CookingStation cookingStation;
    public MealPlacementZone mealPlacementZone;

    [Header("Task 3 - Photo")]
    public GameObject photoPrefab;
    public Transform photoSpawnPoint;
    public GameObject spawnedPhotoInstance;
    public WorldDesaturator worldDesaturator;

    [Header("Ending")]
    public BalconySunSequence balconySunSequence;
    public string mainMenuSceneName = "MainMenu";

    private int placedBoxesCount = 0;
    private int collectedIngredientsCount = 0;
    private bool cookedMeal = false;
    private bool placedMeal = false;
    private bool photoViewed = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    IEnumerator Start()
    {
        yield return StartCoroutine(PlayOpeningWords());
        PrepareInitialState();
    }

    void PrepareInitialState()
    {
        if (recipeNote != null) recipeNote.SetActive(false);

        if (ingredients != null)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient != null) ingredient.gameObject.SetActive(false);
            }
        }

        if (cookingStation != null) cookingStation.gameObject.SetActive(false);
        if (photoViewerUI != null) photoViewerUI.HideImmediate();

        if (taskUI != null) taskUI.ClearTask();
    }

    IEnumerator PlayOpeningWords()
    {
        if (floatingWordsUI == null) yield break;

        yield return StartCoroutine(floatingWordsUI.ShowLine("Is this real?", 2f));
        yield return StartCoroutine(floatingWordsUI.ShowLine("This place feels so familiar.", 2f));
    }

    public void StartLevel3Tasks()
    {
        StartCoroutine(BeginTaskOne());
    }

    IEnumerator BeginTaskOne()
    {
        if (motherDialogueUI != null)
            yield return StartCoroutine(motherDialogueUI.ShowDialogue("Take those boxes inside.", 3f));

        if (taskUI != null)
            taskUI.SetTask("Main Task: Move the boxes", "0/3 boxes delivered");

        SpawnBoxes();
    }

    void SpawnBoxes()
    {
        if (boxPrefab == null || boxSpawnPoints == null) return;

        for (int i = 0; i < boxSpawnPoints.Length; i++)
        {
            GameObject box = Instantiate(boxPrefab, boxSpawnPoints[i].position, boxSpawnPoints[i].rotation, spawnedBoxesRoot);
        }
    }

    public void RegisterBoxPlaced()
    {
        placedBoxesCount++;

        if (taskUI != null)
            taskUI.SetTask("Main Task: Move the boxes", placedBoxesCount + "/3 boxes delivered");

        if (placedBoxesCount >= 3)
        {
            StartCoroutine(BeginTaskTwo());
        }
    }

    IEnumerator BeginTaskTwo()
    {
        if (motherDialogueUI != null)
            yield return StartCoroutine(motherDialogueUI.ShowDialogue("Go to the kitchen and make dinner.", 3f));

        if (recipeNote != null) recipeNote.SetActive(true);

        if (ingredients != null)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient != null) ingredient.gameObject.SetActive(true);
            }
        }

        if (cookingStation != null) cookingStation.gameObject.SetActive(true);

        if (taskUI != null)
            taskUI.SetTask("Main Task: Prepare dinner", "Collect ingredients for Curry Pork Cutlet Rice");
    }

    public void RegisterIngredientCollected()
    {
        collectedIngredientsCount++;

        if (taskUI != null)
            taskUI.SetTask("Main Task: Prepare dinner", collectedIngredientsCount + "/" + ingredients.Length + " ingredients collected");
    }

    public bool HasAllIngredients()
    {
        return collectedIngredientsCount >= ingredients.Length;
    }

    public void RegisterMealCooked()
    {
        cookedMeal = true;

        if (taskUI != null)
            taskUI.SetTask("Main Task: Prepare dinner", "Place the meal on the dining table");
    }

    public bool CanPlaceMeal()
    {
        return cookedMeal;
    }

    public void RegisterMealPlaced()
    {
        placedMeal = true;
        StartCoroutine(BeginTaskThree());
    }

    IEnumerator BeginTaskThree()
    {
        if (worldDesaturator != null)
            worldDesaturator.DesaturateWorld();

        if (motherDialogueUI != null)
            yield return StartCoroutine(motherDialogueUI.ShowDialogueInterrupted("Clean up... be tidy... be...", 2f));

        SpawnPhoto();

        if (taskUI != null)
            taskUI.SetTask("Main Task: Investigate", "Examine the photograph on the sofa");
    }

    void SpawnPhoto()
    {
        if (photoPrefab == null || photoSpawnPoint == null) return;

        spawnedPhotoInstance = Instantiate(photoPrefab, photoSpawnPoint.position, photoSpawnPoint.rotation);
    }

    public void OnPhotoViewed()
    {
        if (photoViewed) return;
        photoViewed = true;

        if (taskUI != null)
            taskUI.SetTask("Main Task: Go outside", "Walk to the balcony");

        if (balconySunSequence != null)
            balconySunSequence.EnableSequence();
    }

    public void TriggerEnding()
    {
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        if (motherDialogueUI != null)
            yield return StartCoroutine(motherDialogueUI.ShowDialogue("...", 3f));

        yield return new WaitForSeconds(2f);

        if (fadeController != null)
            yield return StartCoroutine(fadeController.FadeToWhite());

        SceneManager.LoadScene(mainMenuSceneName);
    }
}