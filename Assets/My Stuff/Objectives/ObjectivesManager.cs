using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    public static ObjectivesManager Instance { get; private set; }

    public List<ObjectiveTask> allTasks;
    private HashSet<ObjectiveTask> completedTasks = new();

    public event Action<ObjectiveTask> OnTaskCompleted;

    private void Awake() => Instance = this;
    
    public void CompleteTask(ObjectiveTask task)
    {
        if (completedTasks.Add(task))
        {
            Debug.Log("Task completed");
            OnTaskCompleted?.Invoke(task);
        }
    }

    public bool IsComplete(ObjectiveTask task) => completedTasks.Contains(task);
    
}
