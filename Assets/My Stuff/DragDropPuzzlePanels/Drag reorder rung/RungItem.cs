using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement), typeof(CanvasGroup))]
public class RungItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string puzzleID; // for later - which puzzle this rung opens
    public string rungID;
    
    [SerializeField] private Transform dragLayer;
    [SerializeField] private Image currImage;
    [SerializeField] private Sprite completedImage;

    private RectTransform rectTransform;
    private LayoutElement layoutElement;
    private CanvasGroup canvasGroup;
    private RungContainer container;
    private RectTransform placeholder;
    
    private Vector2 originalSize;
    private bool isDragging;

    public bool isCompleted = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        layoutElement = GetComponent<LayoutElement>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        container = GetComponentInParent<RungContainer>();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false; 
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragging) return; // guard against re-entrancy

        if (dragLayer == null || container == null)
        {
            Debug.LogWarning($"{name}: missing dragLayer or RungContainer parent, cannot drag.");
            return;
        }

        isDragging = true;
        originalSize = rectTransform.rect.size;

        placeholder = CreatePlaceholder();

        transform.SetParent(dragLayer, worldPositionStays: false);
        rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = originalSize;
        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"pointerDrag is: {eventData.pointerDrag?.name}");
        if (!isDragging) return;

        if (eventData.pointerCurrentRaycast.isValid)
        {
            rectTransform.position = eventData.pointerCurrentRaycast.worldPosition;
        }
        

        int targetIndex = container.GetIndexForPosition(rectTransform.position, placeholder);
        placeholder.SetSiblingIndex(targetIndex);
        container.Rebuild();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        int finalIndex = placeholder.GetSiblingIndex();
        Transform originalParent = placeholder.parent;
        Destroy(placeholder.gameObject);
        placeholder = null;

        transform.SetParent(originalParent, worldPositionStays: false);
        transform.SetSiblingIndex(finalIndex);

        Vector3 pos = rectTransform.localPosition;
        pos.z = 0f;
        rectTransform.localPosition = pos;

        canvasGroup.blocksRaycasts = true;
        container.Rebuild();

        isDragging = false;
    }
    
    private RectTransform CreatePlaceholder()
    {
        var obj = new GameObject("RungPlaceholder", typeof(RectTransform), typeof(LayoutElement));
        var rt = obj.GetComponent<RectTransform>();
        rt.SetParent(transform.parent, false);
        rt.SetSiblingIndex(transform.GetSiblingIndex());

        var placeholderLayout = obj.GetComponent<LayoutElement>();
        placeholderLayout.preferredWidth = originalSize.x;
        placeholderLayout.preferredHeight = originalSize.y;

        return rt;
    }

    public void puzzleComplete()
    {
        currImage.GetComponent<Image>().sprite = completedImage;
        isCompleted = true;
    }
    
    private void OnDestroy()
    {
        // safety net: don't leave an orphaned placeholder if this object is destroyed mid-drag
        if (placeholder != null)
        {
            Destroy(placeholder.gameObject);
        }
    }
    
}
