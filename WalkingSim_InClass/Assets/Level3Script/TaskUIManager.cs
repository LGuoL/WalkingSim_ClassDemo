using TMPro;
using UnityEngine;

public class TaskUIManager : MonoBehaviour
{
    public TextMeshProUGUI mainTaskText;
    public TextMeshProUGUI subTaskText;

    public void SetTask(string mainTask, string subTask)
    {
        if (mainTaskText != null) mainTaskText.text = mainTask;
        if (subTaskText != null) subTaskText.text = subTask;
    }

    public void ClearTask()
    {
        if (mainTaskText != null) mainTaskText.text = "";
        if (subTaskText != null) subTaskText.text = "";
    }
}