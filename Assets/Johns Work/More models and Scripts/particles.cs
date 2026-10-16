using UnityEngine;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;

public class VRClickParticleEmitter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform controllerTransform;
    [SerializeField] private ParticleSystem particleSystem;

    [Header("Emission Settings")]
    [SerializeField] private float emitDuration = 2f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private XRNode controllerNode = XRNode.RightHand;

    private Coroutine emitRoutine;
    private InputDevice controllerDevice;

    private void Awake()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }
    }

    private void Start()
    {
        TryInitializeController();
    }

    private void TryInitializeController()
    {
        controllerDevice = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    private void Update()
    {
        if (!controllerDevice.isValid)
        {
            TryInitializeController();
            return;
        }

        // Check trigger press
        if (controllerDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed)
            && triggerPressed)
        {
            TryEmit();
        }
    }

    private void TryEmit()
    {
        Ray ray = new Ray(controllerTransform.position, controllerTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (emitRoutine != null)
                    StopCoroutine(emitRoutine);

                emitRoutine = StartCoroutine(EmitForTime());
            }
        }
    }

    private IEnumerator EmitForTime()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(emitDuration);
        particleSystem.Stop();
        emitRoutine = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (controllerTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(controllerTransform.position, controllerTransform.forward * rayDistance);
        }
    }
#endif
}
