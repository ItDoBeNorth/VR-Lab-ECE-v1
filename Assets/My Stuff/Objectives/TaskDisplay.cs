using UnityEngine;
using TMPro;

public class TaskDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text taskText;
    [SerializeField] private ObjectivesManager objectiveManager;
    
    private void Start()
    {
        ObjectivesManager.Instance.OnTaskCompleted += HandleTaskCompleted;
        UpdateDisplay();

    }

    private void OnDisable()
    {
        ObjectivesManager.Instance.OnTaskCompleted -= HandleTaskCompleted;
    }
    
    private void HandleTaskCompleted(ObjectiveTask completedTask)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        foreach (ObjectiveTask task in objectiveManager.allTasks)
        {
            if (!objectiveManager.IsComplete(task))
            {
                taskText.text = task.taskDescription;
                return;
            }
        }

        taskText.text = "All tasks complete!";
    }
    
}
