using UnityEngine;

public class PhotoInteractable : Interactable
{
    public override void Interact(Player player)
    {
        if (Level3SequenceManager.instance.photoViewerUI != null)
        {
            Level3SequenceManager.instance.photoViewerUI.Show(
                "Some memories never really leave.",
                OnPhotoClosed
            );
        }
    }

    void OnPhotoClosed()
    {
        Level3SequenceManager.instance.OnPhotoViewed();
    }
}