using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Objectives/Task")]
public class ObjectiveTask : ScriptableObject
{
    public string taskName;
    public string taskDescription;
    public string taskID;
}
