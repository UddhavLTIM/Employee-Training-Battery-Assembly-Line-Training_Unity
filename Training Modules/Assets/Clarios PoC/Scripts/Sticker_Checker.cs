using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Sticker_Checker : MonoBehaviour
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

        if (m_batterisCounted >= 1)
        {
            animator.SetBool("isPlaying", true);
        }
        else
        {
            animator.SetBool("isPlaying",false);
        }

        if(m_batterisCounted >= 10)
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
            foreach (XRSocketInteractor socketInteractor in other.gameObject.GetComponentsInChildren<XRSocketInteractor>())
            {
                if (socketInteractor != null && socketInteractor.gameObject.tag == "Sticker")
                {
                    if (socketInteractor.GetOldestInteractableSelected().transform.gameObject.name == "Top sticker_T" || socketInteractor.GetOldestInteractableSelected().transform.gameObject.CompareTag("Sticker"))
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

    /*if (other.gameObject.CompareTag("Battery"))
    {
        m_batterisCounted++;
        Debug.Log("Batteries Counted: " + m_batterisCounted);

        XRSocketInteractor socketInteractor1 = null;
        XRSocketInteractor socketInteractor2 = null;


        foreach (XRSocketInteractor socketInteractor in other.gameObject.GetComponentsInChildren<XRSocketInteractor>())
        {
            //XRSocketInteractor socketInteractor = child.GetComponentInChildren<XRSocketInteractor>();
            if (socketInteractor != null)
            {
                string[] nameParts = socketInteractor.name.Split('_');

                if (socketInteractor1 == null && nameParts[1] == "T")
                {
                    socketInteractor1 = socketInteractor;
                    //Debug.Log(socketInteractor1.name);
                    Debug.Log("Top Stick found");
                }
                else
                {
                    socketInteractor2 = socketInteractor;
                    //Debug.Log(socketInteractor2.name);
                    Debug.Log("Front Stick found");
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
            if (socketInteractor1.isSelectActive == true && socketInteractor2.isSelectActive == true && socketInteractor1.GetOldestInteractableSelected().transform.gameObject != null && socketInteractor2.GetOldestInteractableSelected().transform.gameObject != null)
            {
                //if (socketInteractor1.GetOldestInteractableSelected().transform.gameObject.CompareTag("Terminal") && socketInteractor2.GetOldestInteractableSelected().transform.gameObject.CompareTag("Terminal"))

                string[] nameParts1 = socketInteractor1.GetOldestInteractableSelected().transform.gameObject.name.Split('_');
                string[] nameParts2 = socketInteractor2.GetOldestInteractableSelected().transform.gameObject.name.Split('_');

                if (nameParts1[1] == "T" && nameParts2[1] == "F")
                {
                    m_score++;
                    Debug.Log(nameParts1[1]);
                    Debug.Log("Score: " + m_score);
                }
                else if (nameParts1[1] == "F" || nameParts2[1] == "T")
                {
                    Debug.Log("Score: " + m_score);
                    Debug.LogWarning("Incorrect placemnet of Stickers");
                }

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
    }*/

}
