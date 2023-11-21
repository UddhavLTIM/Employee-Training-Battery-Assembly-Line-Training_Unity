/**
=====================
// license at:
Name:			ObstacleDetection.cs
Version:		0.1
Update Date:	15, April, 2023
Author:			Uddhav Labde
Description:	Detected if there is an obsticle present between doors.
=====================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    // PUBLIC
    // PROTECTED
    [SerializeField]
    protected bool m_obstaclePresent;
    // PRIVATE
    // ACCESS

    public bool ObstaclePresent()
    {
        return m_obstaclePresent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            m_obstaclePresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            m_obstaclePresent = false;
        }
    }
}
