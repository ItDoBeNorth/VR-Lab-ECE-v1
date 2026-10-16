using UnityEngine;
using UnityEngine.UI;

public class OnHoverInteract : MonoBehaviour
{
    public Image targetImage;

    // Public method to change to a specific color
    public void SetSpecificColor(Color targetColor)
    {
        if (targetImage != null)
        {
            targetImage.color = targetColor;
        }
    }

}
