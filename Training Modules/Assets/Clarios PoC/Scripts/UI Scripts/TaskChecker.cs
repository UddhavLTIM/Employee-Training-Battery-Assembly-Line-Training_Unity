/**
=====================
// license at:
Name:			TaskChecker.cs
Version:		0.1
Update Date:	31, Aug, 2023
Author:			Uddhav Labde
Description:	Checks every colliding object for correct placement.
=====================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChecker : MonoBehaviour
{
    // PUBLIC
    // PROTECTED
    [SerializeField] protected int m_score;
    // PRIVATE
    // ACCESS

    // PUBLIC FUNCTIONS
    // PRIVATE FUNCTIONS
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            Debug.Log("Success");
        }
    }
}
