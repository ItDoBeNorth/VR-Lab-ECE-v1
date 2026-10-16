using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateOnSelect : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up;   // Local axis
    public float rotationSpeed = 90f;            // Degrees per second

    private bool isSelected = false;
    private bool forward = true;
    public float minAngle = -45f;
    public float maxAngle = 45f;

    


    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        isSelected = true;
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        isSelected = false;
    }

    void Update()
    {
        if (!isSelected) return;

        float delta = rotationSpeed * Time.deltaTime;
        Vector3 current = transform.localEulerAngles;

        float angle = current.y;
        if (angle >= 180f)
        {
            forward = false;
        }
        if (angle <= 0f)
        {
            forward = true;
        }
        if (forward)
        {
            angle = Mathf.Clamp(angle + delta, minAngle, maxAngle);
        }
        else
        {
            angle = Mathf.Clamp(angle - delta, minAngle, maxAngle);
        }
        transform.localEulerAngles = new Vector3(current.x, angle, current.z);
    }
}
