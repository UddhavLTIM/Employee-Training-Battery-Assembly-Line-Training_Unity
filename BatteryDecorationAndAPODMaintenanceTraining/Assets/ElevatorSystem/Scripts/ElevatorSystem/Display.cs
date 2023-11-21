/**
=====================
// license at:
Name:			Display.cs
Version:		0.1
Update Date:	15, April, 2023
Author:			Uddhav Labde
Description:	Display is another class that shows the floor that the elevator is on, and the direction it is moving in.
                // Take the currentFloor value from the elevator and the direction it is moving and update it on displays.
=====================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Display : MonoBehaviour
{
    // PUBLIC
    // PROTECTED
    [SerializeField]
    protected Elevator m_elevator;
    [SerializeField]
    protected TMP_Text m_displayFloor;
    [SerializeField]
    protected TMP_Text m_displayDirection;
    // PRIVATE
    // ACCESS

    // Start is called before the first frame update
    void Start()
    {
        // Attach and Reference Scripts 
        if (m_elevator == null)
            m_elevator = GameObject.Find("Elevator").GetComponent<Elevator>();

        //m_displayFloor.text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_displayFloor.text = Convert.ToString(m_elevator.CurrentFloor()); // STRING display current floor = STRInG (INT m_elevator.CurrentFloor())
        if (m_elevator.MovingDirection() == 1f)
        {
            m_displayDirection.text = @"/\";
        }
        else if (m_elevator.MovingDirection() == -1f)
        {
            m_displayDirection.text = @"\/";
        }
    }
}
