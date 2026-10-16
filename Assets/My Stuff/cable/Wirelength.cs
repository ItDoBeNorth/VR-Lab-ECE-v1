using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Wirelength : MonoBehaviour
{
    public Transform wireAnchor;
    public float maxWireLength = 2.5f;
    
    public XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (wireAnchor == null) return;

        float dist = Vector3.Distance(transform.position, wireAnchor.position);

        if (dist > maxWireLength)
        {
            // Project plug back onto the sphere surface at maxWireLength
            Vector3 dir = (transform.position - wireAnchor.position).normalized;
            Vector3 clampedPos = wireAnchor.position + dir * maxWireLength;

            // Move rigidbody (use MovePosition to respect physics)
            rb.MovePosition(clampedPos);

            // Kill outward velocity to prevent tunneling
            Vector3 radialVelocity = Vector3.Project(rb.linearVelocity, dir);
            if (Vector3.Dot(radialVelocity, dir) > 0)
                rb.linearVelocity -= radialVelocity;
            
            // if (grabInteractable != null && grabInteractable.iitsSelected)
            // {
            //     grabInteractable.interactionManager.SelectExit(
            //         grabInteractable.firstInteractorSelecting,
            //         grabInteractable
            //     );
            //     
            // }
        }
    }

    
    
}
