/**
=====================
// license at:
Name:			RandomClicker.cs
Version:		0.1
Update Date:	15, April, 2023
Author:			Uddhav Labde
Description:	Random Clicker of elevator call buttons
=====================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class RandomClicker : MonoBehaviour
{
    // PUBLIC
    public static readonly float m_minInterval = 1f; // minimum time interval between button clicks
    public static readonly float m_maxInterval = 60f; // maximum time interval between button clicks
    public static readonly float m_maxIntervalInternal = 600f; // maximum time interval between button clicks from inside the cabin - this is what keeps the randoness in accidental button presses.
    // PROTECTED
    [SerializeField]
    //protected Button[] m_callButtons;
    protected XRBaseInteractable[] m_callButtons;
    [SerializeField]
    protected XRBaseInteractable[] m_callButtonsInternal;
    // PRIVATE
    private float m_nextClickTime; // time of the next button click
    private float m_nextClickTimeInternal; // time of the next button click
    // ACCESS

    // Start is called before the first frame update
    void Start()
    {
        // Set the time of the first button click
        m_nextClickTime = Time.time + Random.Range(m_minInterval, m_maxInterval);
        m_nextClickTimeInternal = Time.time + Random.Range(m_minInterval, m_maxIntervalInternal);
    }

    // Update is called once per frame
    void Update()
    {
        // check if it's time to click a button
        if (Time.time >= m_nextClickTime)
        {
            // randomly select a button and click it
            int randomIndex = Random.Range(0, m_callButtons.Length);
            //m_callButtons[randomIndex].onClick.Invoke();
            m_callButtons[randomIndex].selectEntered.Invoke(new SelectEnterEventArgs());

            // set the time of the next button click
            m_nextClickTime = Time.time + Random.Range(m_minInterval, m_maxInterval);
        }
        
        // check if it's time to click an internal button 
        if (Time.time >= m_nextClickTimeInternal)
        {
            // randomly select a button and click it
            int randomIndex = Random.Range(0, m_callButtonsInternal.Length);
            m_callButtonsInternal[randomIndex].selectEntered.Invoke(new SelectEnterEventArgs());

            // set the time of the next button click
            m_nextClickTimeInternal = Time.time + Random.Range(m_minInterval, m_maxIntervalInternal);
        }
    }
}