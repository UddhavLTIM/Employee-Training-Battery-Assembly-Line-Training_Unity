using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MarkerAreaTrigger : MonoBehaviour
{
    [SerializeField]
    public UnityEvent unityEvent;

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Test");
            unityEvent.Invoke();   
    }
}
