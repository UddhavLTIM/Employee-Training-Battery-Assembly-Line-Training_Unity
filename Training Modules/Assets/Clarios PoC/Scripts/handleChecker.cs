using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class handleChecker : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    protected int m_batterisCounted = 0; 
    protected int m_score = 0;

    [SerializeField]
    public UnityEvent unityEvent;

    public int GetCount()
    {
        return m_batterisCounted;
    }

    public int GetScore()
    {
        return m_score;
    }


    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        if (m_batterisCounted >= 10)
        {
            animator.SetBool("isPlaying", false);
        }
        else
        {
            animator.SetBool("isPlaying", true);
        }

        if(m_batterisCounted >= 10 && other.gameObject.CompareTag("Battery"))
        {
            unityEvent.Invoke();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            m_batterisCounted++;
            Debug.Log("Batteries Counted: " + m_batterisCounted);
            foreach (XRSocketInteractor socketInteractor in other.gameObject.GetComponentsInChildren<XRSocketInteractor>())
            {
                if (socketInteractor != null && socketInteractor.gameObject.tag=="Handle")
                {
                    if (socketInteractor.GetOldestInteractableSelected().transform.gameObject.name == "everstart h24 battery handle_curved" || socketInteractor.GetOldestInteractableSelected().transform.gameObject.tag == "Handle")
                    {
                        m_score++;
                        Debug.Log("Score: " + m_score);
                    }
                }
                else
                {
                    Debug.Log("Socket Interactor is null!");
                }
            }

        }
        else
        {
            Debug.Log("Battery Tag not found");
        }
    }
}
