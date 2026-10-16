using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRHighlightFeedback : MonoBehaviour
{
    [Header("Colors")]
    public Color hoverColor = Color.yellow;
    public Color selectedColor = Color.green;

    private Renderer rend;
    private Color originalColor;


    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
    }

    // Hover
    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (rend != null)
            rend.material.color = hoverColor;
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        if (rend != null)
            rend.material.color = originalColor;
    }

    

}
