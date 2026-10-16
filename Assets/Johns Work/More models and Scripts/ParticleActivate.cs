using UnityEngine;
using UnityEngine.XR;
using System.Collections;


public class ParticleActivate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created+
    public ParticleSystem particleSystem;
    public float activeTime = 1f;

    private bool isActive = false;

    public void startParticles()
    {
        StartCoroutine(ActivateParticles());
    }

    private IEnumerator ActivateParticles()
    {
        isActive = true;

        particleSystem.Play();

        yield return new WaitForSeconds(activeTime);

        particleSystem.Stop();
        isActive = false;
    }
}
