using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Checks for validity of items being placted in boxes, swap/deletion if invalid or spawner box, swap if exsisting

// NOTE: needs itemID

// https://www.youtube.com/watch?v=kWRyZ3hb1Vc and comments for ref
public class Slot : MonoBehaviour, IDropHandler
{
    public ItemID correctItemID;
    public bool isPresetSlot = false;

    public Image boxImage;
    public Color32 defaultColor;
    
    public CanvasGroup canvasGroup;

    private void Start()
    {
        defaultColor = new Color32(105, 255, 140,225);
        boxImage =  GetComponent<Image>();
        boxImage.color = defaultColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // inital check and get
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem == null)
        {
            return;
        }
        
        // validity
        if (GetComponent<SpawnerSlot>() != null)
        {
            Debug.Log("deleted");
            Destroy(dropped);
            return;
            
        }

        // swapping
        if(transform.childCount != 0)
        {           
            GameObject currentItem = transform.GetChild(0).gameObject;
            DraggableItem currentDraggable = currentItem.GetComponent<DraggableItem>();

            if (draggableItem.parentAfterDrag.GetComponent<SpawnerSlot>() != null)
            {
                Debug.Log("swaped and deleted");
                Destroy(currentItem);
            } else {
                Debug.Log("swapped");
                currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
            }
        }
        
        //item place in box
        draggableItem.parentAfterDrag = transform;
        draggableItem.MarkDropped();
        draggableItem.SetDroppedVisual();
        Debug.Log("new parent");
        
        if (draggableItem.needsEditor)
        {
            if (canvasGroup == null)
            {
                Debug.LogWarning($"{name}: needsEditor item dropped but no CanvasGroup assigned.");
            }
            else
            {
                ValuePanel.instance.OpenPanel(draggableItem, canvasGroup);
            }
        }
        
        // ID checking and box color
        bool isCorrect = draggableItem.itemID == correctItemID;
        Debug.Log("ID is " + isCorrect);
        
        // animation???
        // if (draggableItem.biteID == correctBiteID && targetAnimator != null)
        // {
        //     targetAnimator.SetTrigger(triggerName);
        // }
    }
    
    // ID logic
    public bool IsCorrect()
    {
        if (transform.childCount == 0) return false;
        DraggableItem current = transform.GetChild(0).GetComponent<DraggableItem>();
        if (current == null) return false;

        //also check variables and values
        
        return current.itemID == correctItemID;
    }
    
    public void SetColor(Color color)
    {
        boxImage.color = color;
    }

    public void Reset()
    {
        boxImage.color = defaultColor;
        if (transform.childCount > 0 && !isPresetSlot)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        

    }

}
