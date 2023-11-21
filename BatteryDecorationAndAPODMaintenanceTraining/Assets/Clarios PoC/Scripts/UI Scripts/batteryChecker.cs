using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.Events;

public class batteryChecker : MonoBehaviour
{
    //[SerializeField] 
    protected int m_batterisCounted = 0;
    //[SerializeField] 
    protected int m_score = 0;
    [SerializeField]
    private Animator animator;

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

        if(m_batterisCounted == 10 && other.gameObject.CompareTag("Battery"))
        {
            unityEvent.Invoke();
            this.gameObject.SetActive(false);
        }

        /*if (other.gameObject.CompareTag("Battery"))
        {
            Destroy(other.gameObject);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            m_batterisCounted++;
            Debug.Log("Batteries Counted: " + m_batterisCounted);

            XRSocketInteractor socketInteractor1 = null;
            XRSocketInteractor socketInteractor2 = null;

            /*socketInteractor1 = other.gameObject.FindGameObjectWithTag("+ terminal").GetComponent<XRSocketInteractor>();
            socketInteractor2 = other.gameObject.FindGameObjectWithTag("- terminal").GetComponent<XRSocketInteractor>();*/

            foreach (XRSocketInteractor socketInteractor in other.gameObject.GetComponentsInChildren<XRSocketInteractor>())
            {
                //XRSocketInteractor socketInteractor = child.GetComponentInChildren<XRSocketInteractor>();
                if (socketInteractor != null)
                {
                    string[] nameParts = socketInteractor.name.Split('_');

                    if (socketInteractor1 == null && nameParts[1] == "-")
                    {
                        socketInteractor1 = socketInteractor;
                        //Debug.Log(socketInteractor1.name);
                        Debug.Log("-ve found");
                    }
                    else
                    {
                        socketInteractor2 = socketInteractor;
                        //Debug.Log(socketInteractor2.name);
                        Debug.Log("+ve found");
                        break;
                    }
                }
                else
                {
                    Debug.Log("Socket Interactor is null!");
                }
            }

            if (socketInteractor1 != null && socketInteractor2 != null)
            {
                if (/*socketInteractor1.gameObject.CompareTag("Terminal") && socketInteractor2.gameObject.CompareTag("Terminal") &&*/ socketInteractor1.GetOldestInteractableSelected().transform.gameObject != null && socketInteractor2.GetOldestInteractableSelected().transform.gameObject != null)
                {
                    //if (socketInteractor1.GetOldestInteractableSelected().transform.gameObject.CompareTag("Terminal") && socketInteractor2.GetOldestInteractableSelected().transform.gameObject.CompareTag("Terminal"))
                    //{
                        string[] nameParts1 = socketInteractor1.GetOldestInteractableSelected().transform.gameObject.name.Split('_');
                        string[] nameParts2 = socketInteractor2.GetOldestInteractableSelected().transform.gameObject.name.Split('_');

                        if (nameParts1[1] == "-" && nameParts2[1] == "+")
                        {
                            m_score++;
                            Debug.Log(nameParts1[1]);
                            Debug.Log("Score: " + m_score);
                        }
                        else if (nameParts1[1] == "+" || nameParts2[1] == "-")
                        {
                            Debug.Log("Score: " + m_score);
                            Debug.LogWarning("Incorrect placemnet of terminals");
                        }
                    //}
                    //else if (socketInteractor1.GetOldestInteractableSelected().transform.gameObject.CompareTag("Terminal") || socketInteractor2.GetOldestInteractableSelected().transform.gameObject.CompareTag("Terminal"))
                    //{
                        //Debug.LogWarning("Missing 1 Attachment");
                    //}
                    //else
                    //{
                        //Debug.Log("Missing Both Attachments");
                    //}
                }
                else
                {
                    Debug.LogWarning("One or both socket interactors have null/active selectTarget.");
                }
            }
            else
            {
                Debug.LogWarning("Missing XR Socket Interactor.");
            }
        }
        else
        {
            Debug.Log("Battery Tag not found");
        }
    }
}
