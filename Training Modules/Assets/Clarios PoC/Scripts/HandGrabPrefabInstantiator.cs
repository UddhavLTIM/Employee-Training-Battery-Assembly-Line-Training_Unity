using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrabPrefabInstantiator : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    private XRGrabInteractable grabbedObject;
    private bool isGrabbed = false;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            InstantiatePrefabOnHand(other);
        }
    }

    private void InstantiatePrefabOnHand(Collider other)
    {
        // Instantiate the prefab on the hand's position and rotation
        GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, transform.position, transform.rotation);

        // Attach the instantiated prefab to the hand
        //other.gameObject.GetComponent<Xr>.attachTransform = instantiatedPrefab.transform;

        // Set the grabbed object
        //grabbedObject = interactable;
        isGrabbed = true;
    }
}

