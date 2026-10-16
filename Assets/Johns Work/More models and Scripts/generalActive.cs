using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject disableObject_1;
    public GameObject disableObject_2;

    public GameObject enableObject_1;
    public GameObject enableObject_2;

    /// <summary>
    /// Disables the target GameObject.
    /// Can be called from other scripts, UnityEvents, buttons, or XR events.
    /// </summary>
    public void DisableTarget()
    {
        if (disableObject_1 != null)
        {
            disableObject_1.SetActive(false);
        }
        if (disableObject_2 != null)
        {
            disableObject_2.SetActive(false);
        }
    }
    public void ActivateTarget()
    {
        if (enableObject_1 != null)
        {
            enableObject_1.SetActive(true);
        }
        if (enableObject_2 != null)
        {
            enableObject_2.SetActive(true);
        }
    }
}