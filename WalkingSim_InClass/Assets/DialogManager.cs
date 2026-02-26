using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI placeHolderOpeningLine;


    private void OnEnable()
    {
        Player.OnDialogueRequested += StartDialogue;
    }

    private void OnDisabel()
    {
        Player.OnDialogueRequested -= StartDialogue;
    }

    void StartDialogue(NPCData nPCData)
    {
        if(nPCData == null)
        {
            Debug.Log("NPC Data is Null");
            return;
        }
        if(dialoguePanel != null) dialoguePanel.SetActive(true);
        if (displayName != null) displayName.text = nPCData.displayName;
        if(placeHolderOpeningLine != null) placeHolderOpeningLine.text = nPCData.placeHolderOpeningLine;
        Debug.Log($"Dialogue start with {nPCData.displayName}: {nPCData.placeHolderOpeningLine}");
    }
}
