using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// drags an item out over area and drops it in a valid slot. if from a spawner, it leaves a replacement when dragged.
// logic for spawner invalid parent and invalid slot snap back.

// NOTE: needs correct image and item ID

// refrence: https://www.youtube.com/watch?v=kWRyZ3hb1Vc 
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemID itemID;
    
    public Image image;
    public Transform parentAfterDrag;
    [SerializeField] private Transform dragLayer;

    private bool droppedOnValidTarget;

    public bool needsEditor;
    public string editedValue;
    
    public Sprite droppedSprite;
    private Sprite originalSprite;

    // add in slot temp image and variable/value label
    
    
    private void Start()
    {
        image = GetComponent<Image>();
        dragLayer = transform.parent.parent.parent;
        originalSprite = image.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        SetOriginalVisual();
        transform.localScale = Vector3.one;
        parentAfterDrag = transform.parent;
        droppedOnValidTarget = false;
        
        // ref claude
        SpawnerSlot spawner = parentAfterDrag.GetComponent<SpawnerSlot>();
        if (spawner != null)
        {
            Debug.Log("replaced");
            spawner.SpawnItem();
        }
        
        transform.SetParent(dragLayer);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        if (eventData.pointerCurrentRaycast.isValid)
        {
            transform.position = eventData.pointerCurrentRaycast.worldPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!droppedOnValidTarget)
        {
            if (parentAfterDrag.GetComponent<SpawnerSlot>() != null){
                Destroy(gameObject);
            } else
            {
                transform.SetParent(parentAfterDrag);
            }
        }
        
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Slot parentSlot = GetComponentInParent<Slot>();
        if (parentSlot != null)
        {
            parentSlot.OnDrop(eventData);
        }
    }

    public void MarkDropped()
    {
        droppedOnValidTarget = true;
    }

    public void ApplyValue(String value)
    {
        editedValue = value;
        
        // could also apply an image or other value label
    }
    
    public void SetDroppedVisual()
    {
        if (droppedSprite != null)
        {
            image.sprite = droppedSprite;
        }
    }

    public void SetOriginalVisual()
    {
        image.sprite = originalSprite;
    }
}
