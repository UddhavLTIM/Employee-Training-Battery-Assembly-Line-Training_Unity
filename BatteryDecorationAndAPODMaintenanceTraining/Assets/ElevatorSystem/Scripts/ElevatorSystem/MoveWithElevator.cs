/**
=====================
// license at:
Name:			ObstacleDetection.cs
Version:		0.1
Update Date:	1, May, 2023
Author:			Uddhav Labde
Description:	Detected if there is an player present on the elevator and makes it the child and when not removes from child
=====================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithElevator : MonoBehaviour
{
    // PUBLIC
    // PROTECTED
    [SerializeField]
    protected bool m_playerPresent;

    [SerializeField]
    protected Transform m_XRRig;
    // PRIVATE
    // ACCESS
    
    public bool PlayerPresent()
    {
        return m_playerPresent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_playerPresent = true;

            // make the other object the child of this object
            other.transform.parent = this.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_playerPresent = false;
            other.transform.parent = m_XRRig.transform;
        }
    }
}
