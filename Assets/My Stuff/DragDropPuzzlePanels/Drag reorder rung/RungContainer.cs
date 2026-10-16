using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RungContainer : MonoBehaviour
{
    public List<string> rungIDs;
    public ObjectiveTask rungOrderTask;
    [SerializeField] private CanvasGroup canvasGroup;
    
    public int GetIndexForPosition(Vector3 worldPosition, Transform draggedItem)
    {
        Vector3 localPos = transform.InverseTransformPoint(worldPosition);
        int index = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child == draggedItem) continue;

            RectTransform childRect = child as RectTransform;

            // assumes top-to-bottom vertical list (Y decreases downward)
            if (localPos.y < childRect.localPosition.y)
            {
                index = i + 1;
            }
        }

        return index;
    }

    public void CheckRungOrder()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log($"Checking order, childCount: {transform.childCount}, expected count: {rungIDs.Count}");

            RungItem rung = transform.GetChild(i).GetComponent<RungItem>();
            if (rung == null)
            {
                Debug.Log($"Child {i} has no RungItem, skipping");
                continue;
            }
            
            if (i >= rungIDs.Count || rung.rungID != rungIDs[i] || !rung.isCompleted)
            {
                Debug.Log($"FAILED at index {i}");
                return;
            }
        }
        Debug.Log("All checks passed, order correct");
        ObjectivesManager.Instance.CompleteTask(rungOrderTask);
        canvasGroup.blocksRaycasts = true;
    }

    public void Rebuild()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
