using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnableObjectOnSelect : MonoBehaviour
{
    [Header("Target Object")]
    public GameObject objectToEnable;

    [Header("Behavior")]
    public bool disableOnDeselect = false;

    // Called when this object is selected
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (objectToEnable != null)
            objectToEnable.SetActive(true);
    }

    // Called when selection ends (optional)
    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (disableOnDeselect && objectToEnable != null)
            objectToEnable.SetActive(false);
    }
}
