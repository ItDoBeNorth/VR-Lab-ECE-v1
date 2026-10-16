using System.Collections.Generic;
using UnityEngine;

public class OnEnterTrigger : MonoBehaviour
{
    public List<GameObject> objectToReveal; // Drag your hidden object here in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectToReveal)
            {
                obj.SetActive(true); // Makes the object visible
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectToReveal)
            {
                obj.SetActive(false);
            }
        }
    }
}