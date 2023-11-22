/**
=====================
// license at:
Name:			Controls.cs
Version:		0.1
Update Date:	15, April, 2023
Author:			Uddhav Labde
Description:	Controls class manages the User Interface between the user and the elevator controller.
=====================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Controls : MonoBehaviour
{
    // PUBLIC
    // PROTECTED
    [SerializeField]
    protected Controller m_controler;
    [SerializeField]
    protected Transform[] m_controlGroups; // Attach all the gameobjects which has controls 
    [SerializeField]
    protected List<XRBaseInteractable> m_controlsInteractables;
    [SerializeField]
    protected List<XRBaseInteractor> m_controlsInteractors;
    // PRIVATE
    [SerializeField]
    protected bool AccessSwitch;
    [SerializeField]
    protected bool InCarInspectionSwitch;
    [SerializeField]
    protected bool CarTopInspectionSwitch;
    [SerializeField]
    protected bool EStopSwitch;

    [SerializeField]
    protected bool CarTopEnableButton;
    [SerializeField]
    protected bool CarTopUpButton;
    [SerializeField]
    protected bool CarTopDownButton;
    // ACCESS

    public bool InCarInspectionMode()
    {
        return InCarInspectionSwitch;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Attach and Reference Scripts 
        if (m_controler == null)
        {
            if (this.GetComponent<Controller>() == null)
            {
                //this.gameObject.AddComponent<Controller>();
                //m_controler = this.GetComponent<Controller>();
            }
            else
            {
                m_controler = this.GetComponent<Controller>();
            }
        }

        // Reference each control
        if (m_controlsInteractables.Count == 0 && m_controlsInteractors.Count == 0)
        {
            for (int i = 0; i < m_controlGroups.Length; i++)
            {
                m_controlsInteractables.AddRange(m_controlGroups[i].GetComponentsInChildren<XRBaseInteractable>());
                m_controlsInteractors.AddRange(m_controlGroups[i].GetComponentsInChildren<XRBaseInteractor>());
            }
        }

        // Sort each control and Start I/0 with each control and add listeners to each control
        SortIO();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sort each control and Start I/0 with each control
    private void SortIO()
    {
        foreach (XRBaseInteractable control in m_controlsInteractables)
        {
            string[] nameParts = control.name.Split('_');

            if (nameParts[0] == "External")
            {
                if (nameParts[2] == "Call")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Button>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCallButtonClicked(control); });
                        control.GetComponent<XRSimpleInteractable>().selectExited.AddListener(delegate { OnCallButtonReleased(control); });
                        //control.GetComponent<Button>().onClick.AddListener(delegate { OnCallButtonClicked(control); });
                    }
                }
                else if (nameParts[2] == "Mode")
                {

                }
                else if (nameParts[2] == "Display")
                {

                }
            }
            else if (nameParts[0] == "Internal")
            {
                if (nameParts[2] == "Call")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Button>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCallButtonClicked(control); });
                        //control.GetComponent<Button>().onClick.AddListener(delegate { OnCallButtonClicked(control); });
                    }
                }
                else if (nameParts[2] == "Door")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Button>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnInCarDoorButtonPressed(control); });
                        control.GetComponent<XRSimpleInteractable>().selectExited.AddListener(delegate { OnInCarDoorButtonReleased(control); });
                        //control.GetComponent<Button>().onClick.AddListener(delegate { OnInCarDoorButtonClicked(control); });
                    }
                }
                else if (nameParts[2] == "Stop")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Button>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnInCarStopButtonClicked(control); });
                        //control.GetComponent<Button>().onClick.AddListener(delegate { OnInCarStopButtonClicked(control); });
                    }
                }
                else if (nameParts[2] == "Alarm")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Button>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnAlarmButtonPressed(control); });
                        control.GetComponent<XRSimpleInteractable>().selectExited.AddListener(delegate { OnAlarmButtonReleased(control); });
                        //control.GetComponent<Button>().onClick.AddListener(delegate { OnAlarmButtonPressed(control); });
                    }
                }
                else if (nameParts[2] == "Lights")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Toggle>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCarTopLightButtonToggled(control); });
                        //control.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnCarTopLightButtonToggled(control); });
                    }
                }
                else if (nameParts[2] == "Mode")
                {

                }
                else if (nameParts[2] == "Display")
                {

                }

            }
            else if (nameParts[0] == "CarTop")
            {
                if (nameParts[2] == "Call")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Button>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCarTopCallButtonPressed(control); });
                        control.GetComponent<XRSimpleInteractable>().selectExited.AddListener(delegate { OnCarTopCallButtonReleased(control); });
                        //control.GetComponent<Button>().onClick.AddListener(delegate { OnCarTopCallButtonClicked(control); });
                    }
                }
                else if (nameParts[2] == "Door")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Slider>() != null)
                    {
                        //control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCarTopDoorButtonPressed(control); });
                        //control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCarTopDoorButtonReleased(control); });
                        //control.GetComponent<Slider>().onValueChanged.AddListener(delegate { OnCarTopDoorButtonClicked(control); });
                        control.GetComponent<XRSlider>().onValueChange.AddListener(delegate { OnCarTopDoorButtonToggled(control); });
                    }
                }
                else if (nameParts[2] == "Stop")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Toggle>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCarTopStopButtonToggled(control); });
                        //control.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnCarTopStopButtonToggled(control); });
                    }
                }
                else if (nameParts[2] == "Alarm")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Button>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnAlarmButtonPressed(control); });
                        control.GetComponent<XRSimpleInteractable>().selectExited.AddListener(delegate { OnAlarmButtonReleased(control); });
                        //control.GetComponent<Button>().onClick.AddListener(delegate { OnAlarmButtonPressed(control); });
                    }
                }
                else if (nameParts[2] == "Lights")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Toggle>() != null)
                    {
                        control.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(delegate { OnCarTopLightButtonToggled(control); });
                        //control.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnCarTopLightButtonToggled(control); });
                    }
                }
                else if (nameParts[2] == "Mode")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractable>() != null || control.GetComponent<Toggle>() != null)
                    {
                        //control.GetComponent<XRSimpleInteractable>().selectExited.AddListener(delegate { OnCarTopInspectionSwitchToggle(control); });
                        //control.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnCarTopInspectionSwitchToggle(control); });
                        control.GetComponent<XRKnob>().onValueChange.AddListener(delegate { OnCarTopInspectionSwitchToggle(control); });
                    }
                }
            }
        }

        foreach (XRBaseInteractor control in m_controlsInteractors)
        {
            string[] nameParts = control.name.Split('_');

            if (nameParts[0] == "External")
            {
                if (nameParts[2] == "Mode")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractor>() != null || control.GetComponent<Toggle>() != null)
                    {
                        control.GetComponent<XRSocketInteractor>().selectEntered.AddListener(delegate { OnAccessKeyInserted(control); });
                        control.GetComponent<XRSocketInteractor>().selectExited.AddListener(delegate { OnAccessKeyRemoved(control); });
                        //control.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnAccessKeyToggle(control); });
                    }
                }
            }
            else if (nameParts[0] == "Internal")
            {
                if (nameParts[2] == "Mode")
                {
                    // Start listening to each button
                    if (control.GetComponent<XRBaseInteractor>() != null || control.GetComponent<Toggle>() != null)
                    {
                        control.GetComponent<XRSocketInteractor>().selectEntered.AddListener(delegate { OnInCarInspectionKeyInserted(control); });
                        control.GetComponent<XRSocketInteractor>().selectExited.AddListener(delegate { OnInCarInspectionKeyRemoved(control); });
                        //control.GetComponent<Toggle>().onValueChanged.AddListener(delegate { OnInCarInspectionKeyToggle(control); });
                    }
                }
            }

            else if (nameParts[0] == "CarTop")
            {
                if (nameParts[2] == "Mode")
                {

                }
            }
        }
    }

    /// <summary>
    /// Access Functionality
    /// </summary>
    private void OnAccessKeyInserted(XRBaseInteractor toggle)
    {
        if (toggle.GetComponent<XRBaseInteractor>().firstInteractableSelected.transform.tag == "AccessKey")
        {
            //Debug.Log("Access Key Inserted");
            AccessSwitch = true;
        }
    }

    private void OnAccessKeyRemoved(XRBaseInteractor toggle)
    {
        //Debug.Log("Access Key Removed");
        AccessSwitch = false;
    }

    /// <summary>
    /// In Car Functionality
    /// </summary>
    private void OnCallButtonClicked(XRBaseInteractable button)
    {
        //Debug.Log("Button " + button.name + " clicked!");

        // Set properties based on the button's name
        // [Control]_[Control]_[Type]_[Index]_[Data], Control_Internal_Floor_2_--, Control_Internal_Stop_--_--, Control_External_Call_0_Up
        string[] nameParts = button.name.Split('_');

        if (InCarInspectionSwitch == false && AccessSwitch == false && CarTopInspectionSwitch == false && EStopSwitch == false) //No keys used
        {
            if (nameParts[0] == "External")
            {
                // Set destination properties based on the button's name
                if (nameParts[2] == "Call")
                {
                    int Source = int.Parse(nameParts[4]);
                    Direction Direction = nameParts[3] == "UP" ? Direction.UP : Direction.DOWN;

                    m_controler.AddElevatorCall(Source, Direction);
                }
            }
            else if (nameParts[0] == "Internal")
            {
                if (nameParts[2] == "Call")
                {
                    int Destination = int.Parse(nameParts[4]);
                    Direction Direction = Direction.UP;

                    m_controler.AddElevatorCall(Destination, Direction);
                }
            }
        }
        else if (InCarInspectionSwitch == true && AccessSwitch == false && CarTopInspectionSwitch == false && EStopSwitch == false)
        {
            if (nameParts[0] == "Internal")
            {
                if (nameParts[2] == "Call")
                {
                    int Destination = int.Parse(nameParts[4]);
                    Direction Direction = Direction.UP;

                    m_controler.AddElevatorCall(Destination, Direction);
                }
            }
        }
        else if (InCarInspectionSwitch == true && AccessSwitch == true && EStopSwitch == false)
        {
            if (nameParts[0] == "External")
            {
                if (nameParts[4] == "3")
                {
                    if (nameParts[3] == "UP")
                    {
                        m_controler.ManualMoveStart(Direction.UP);
                    }
                    else if (nameParts[3] == "DOWN")
                    {
                        m_controler.ManualMoveStart(Direction.DOWN);
                    }
                }
            }
        }
    }

    private void OnCallButtonReleased(XRBaseInteractable button)
    {
        string[] nameParts = button.name.Split('_');

        if (InCarInspectionSwitch == true && AccessSwitch == true && EStopSwitch == false)
        {
            if (nameParts[0] == "External")
            {
                if (nameParts[4] == "3")
                {
                    if (nameParts[3] == "UP")
                    {
                        m_controler.ManualMoveStop();
                    }
                    else if (nameParts[3] == "DOWN")
                    {
                        m_controler.ManualMoveStop();
                    }
                }
            }
        }
    }

    private void OnInCarDoorButtonPressed(XRBaseInteractable button)
    {
        string[] nameParts = button.name.Split('_');

        if (InCarInspectionSwitch == false && AccessSwitch == false && CarTopInspectionSwitch == false && EStopSwitch == false)
        {
            if (nameParts[3] == "OPEN")
            {
                m_controler.ElevatorDoorsOpen();
            }
            else if (nameParts[3] == "CLOSE")
            {
                m_controler.ElevatorDoorsClose();
            }
        }
        else if (InCarInspectionSwitch == true && AccessSwitch == false && CarTopInspectionSwitch == false && EStopSwitch == false)
        {
            if (nameParts[3] == "OPEN")
            {
                // open doors
                m_controler.ElevatorDoorsStayOpen();
            }
            else if (nameParts[3] == "CLOSE")
            {
                // close doors
                m_controler.ManualCloseDoorsStart();
            }
        }
    }

    private void OnInCarDoorButtonReleased(XRBaseInteractable button)
    {
        string[] nameParts = button.name.Split('_');

        if (InCarInspectionSwitch == true && AccessSwitch == false && CarTopInspectionSwitch == false && EStopSwitch == false)
        {
            if (nameParts[3] == "OPEN")
            {
                
            }
            else if (nameParts[3] == "CLOSE")
            {
                m_controler.ManualCloseDoorTest();
            }
        }
    }

    private void OnInCarStopButtonClicked(XRBaseInteractable button)
    {
        string[] nameParts = button.name.Split('_');

        if (InCarInspectionSwitch == false && AccessSwitch == false && CarTopInspectionSwitch == false && EStopSwitch == false)
        {
            if (nameParts[2] == "Stop")
            {
                m_controler.SafelyStop();
            }
        }
        else if (InCarInspectionSwitch == true && AccessSwitch == false && CarTopInspectionSwitch == false && EStopSwitch == false)
        {
            if (nameParts[2] == "Stop")
            {
                m_controler.SafelyStop();
            }
        }
    }
    
    private void OnAlarmButtonPressed(XRBaseInteractable button)
    {
        // Play alarm when the button is pressed
        string[] nameParts = button.name.Split('_');

        if (nameParts[2] == "Alarm")
        {
            m_controler.PlayAlarm();
        }
    }

    private void OnAlarmButtonReleased(XRBaseInteractable button)
    {
        // Stop alarm when the button is released
        string[] nameParts = button.name.Split('_');

        if (nameParts[2] == "Alarm")
        {
            m_controler.StopAlarm();
        }
    }

    private void OnInCarInspectionKeyInserted(XRBaseInteractor toggle)
    {
        if (toggle.GetComponent<XRBaseInteractor>().firstInteractableSelected.transform.tag == "InspectionKey")
        {
            Debug.Log("Inspection Key Inserted");
            InCarInspectionSwitch = true;

            m_controler.RemoveAllCalls();
        }
    }

    private void OnInCarInspectionKeyRemoved(XRBaseInteractor toggle)
    {
        //Debug.Log("Inspection Key Removed");
        InCarInspectionSwitch = false;
    }

    /// <summary>
    /// Car Top Functionality
    /// </summary>
    private void OnCarTopCallButtonPressed(XRBaseInteractable button)
    {
        if (InCarInspectionSwitch == true && CarTopInspectionSwitch == true && EStopSwitch == false)
        {
            string[] nameParts = button.name.Split('_');

            if (nameParts[3] == "ENABLE")
            {
                CarTopEnableButton = true;
            }
            else if (nameParts[3] == "UP")
            {
                if (CarTopEnableButton == true && CarTopDownButton == false)
                {
                    CarTopUpButton = true;
                    m_controler.ManualMoveStart(Direction.UP);
                }
            }
            else if (nameParts[3] == "DOWN")
            {
                if (CarTopEnableButton == true && CarTopUpButton == false)
                {
                    CarTopDownButton = true;
                    m_controler.ManualMoveStart(Direction.DOWN);
                }
            }
        }
    }

    private void OnCarTopCallButtonReleased(XRBaseInteractable button)
    {
        string[] nameParts = button.name.Split('_');

        if (InCarInspectionSwitch == true && CarTopInspectionSwitch == true && EStopSwitch == false)
        {
            if (nameParts[3] == "ENABLE")
            {
                CarTopEnableButton = false;
                CarTopUpButton = false;
                CarTopDownButton = false;
            }
            else if (nameParts[3] == "UP")
            {
                if (CarTopEnableButton == true && CarTopDownButton == false)
                {
                    CarTopUpButton = false;
                    // stop moving
                    m_controler.ManualMoveStop();
                }
            }
            else if (nameParts[3] == "DOWN")
            {
                if (CarTopEnableButton == true && CarTopUpButton == false)
                {
                    CarTopDownButton = false;
                    // stop moving
                    m_controler.ManualMoveStop();
                }
            }
        }
    }

    private void OnCarTopDoorButtonToggled(XRBaseInteractable button)
    {
        string[] nameParts = button.name.Split('_');

        if (InCarInspectionSwitch == true && CarTopInspectionSwitch == true && EStopSwitch == false)
        {
            if (Mathf.Approximately(button.GetComponent<XRSlider>().value, 0))
            {
                m_controler.ElevatorDoorsStayOpen();
            }
            else if (Mathf.Approximately(button.GetComponent<XRSlider>().value, 0.5f))
            {
                //m_controler.ElevatorDoorsStayOpen();
            }
            else if (Mathf.Approximately(button.GetComponent<XRSlider>().value, 1f))
            {
                m_controler.ManualCloseDoorsStart();
            }
        }
    }

    private void OnCarTopStopButtonToggled(XRBaseInteractable toggle)
    {
        string[] nameParts = toggle.name.Split('_');

        if (nameParts[2] == "Stop")
        {
            EStopSwitch = !EStopSwitch;
            if (EStopSwitch == true)
            {
                toggle.GetComponent<XRPokeFollowAffordance>().returnToInitialPosition = false;
            }
            else
            {
                toggle.GetComponent<XRPokeFollowAffordance>().returnToInitialPosition = true;
            }
        }
    }

    private void OnCarTopLightButtonToggled(XRBaseInteractable toggle)
    {
        string[] nameParts = toggle.name.Split('_');

        if (nameParts[0] == "Internal")
        {
            if (nameParts[2] == "Lights")
            {
                //m_controler.ToggleInteriorLights();
            }
        }
        else if (nameParts[0] == "CarTop")
        {
            if (nameParts[2] == "Lights")
            {
                m_controler.ToggleLight();
            }
        }
    }
    
    private void OnCarTopInspectionSwitchToggle(XRBaseInteractable toggle)
    {
        //Debug.Log("knob");
        //if (toggle.GetComponent<XRKnob>().value <= 0f)
        if (Mathf.Approximately(toggle.GetComponent<XRKnob>().value, 0))
        {
            //Debug.Log("TOCI on");
            CarTopInspectionSwitch = true;
        }
        //else if (toggle.GetComponent<XRKnob>().value >= 1f)
        else if (Mathf.Approximately(toggle.GetComponent<XRKnob>().value, 1))
        {
            //Debug.Log("TOCI off");
            CarTopInspectionSwitch = false;
        }
    }
}