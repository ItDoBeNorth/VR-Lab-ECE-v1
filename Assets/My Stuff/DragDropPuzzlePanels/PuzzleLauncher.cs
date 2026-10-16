using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

[Serializable]
public class PuzzlePanelEntry
{
    public string puzzleID;
    public GameObject panel;
    public bool wasStarted = false;
    public ObjectiveTask startingTask;
}

public class PuzzleLauncher : MonoBehaviour, IDropHandler
{
    [SerializeField] private List<PuzzlePanelEntry> puzzlePanels;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        GameObject dropped = eventData.pointerDrag;
        RungItem rung = dropped.GetComponent<RungItem>();

        if (rung == null) return; // not a rung, ignore (e.g. a puzzle piece dragged here accidentally)

        PuzzlePanelEntry entry = FindPanel(rung.puzzleID);
        if (entry == null)
        {
            Debug.LogWarning($"No puzzle panel registered for ID '{rung.puzzleID}'.");
            return;
        }
        GameObject targetPanel = entry.panel;

        PanelManager.Instance.Open(targetPanel);

        if (entry.wasStarted == false)
        {
            ObjectivesManager.Instance.CompleteTask(entry.startingTask);
            entry.wasStarted = true;
        }
        
    }

    private PuzzlePanelEntry FindPanel(string puzzleID)
    {
        foreach (var entry in puzzlePanels)
        {
            if (entry.puzzleID == puzzleID)
            {
                return entry;
            }
        }
        return null;
    }

}
