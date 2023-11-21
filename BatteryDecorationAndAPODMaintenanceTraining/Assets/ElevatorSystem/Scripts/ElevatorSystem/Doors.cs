/**
=====================
// license at:
Name:			Doors.cs
Version:		0.1
Update Date:	15, April, 2023
Author:			Uddhav Labde
Description:	The Floor class has a list of all the doors on all floors. 
                It check for elevator's current hlated floor and operates the landing doors on respective floor.
                //iska cable pull karega toh ye khulega.
                //cable kaise b pull kar - elevator se ya door key se
=====================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum DoorState { Idle, DoorsOpening, DoorsClosing}

public class Doors : MonoBehaviour
{
    // PUBLIC
    public static readonly float DoorSpeed = 0.5f;
    // PROTECTED
    [SerializeField]
    protected DoorState m_currentState;
    [SerializeField]
    protected DoorState m_requestedState;
    [SerializeField]
    protected Elevator m_elevator;
    [SerializeField]
    protected Transform m_doorSensor;
    [SerializeField]
    protected int CurrentFloorNo;
    [Space(10)]
    [Header("Door Objects")]
    [SerializeField]
    protected Transform elevatorDoorL;
    [SerializeField]
    protected Transform elevatorDoorR;
    [SerializeField]
    protected Transform elevatorDoorLTargetOpened;
    [SerializeField]
    protected Transform elevatorDoorRTargetOpened;
    [SerializeField]
    protected Transform elevatorDoorLTargetClosed;
    [SerializeField]
    protected Transform elevatorDoorRTargetClosed;
    [SerializeField]
    protected XRSocketInteractor doorWedgeToolSocket;
    [SerializeField]
    protected XRSocketInteractor doorKeySocket;
    [SerializeField]
    protected XRSocketInteractor doorKeySocket2;
    [SerializeField]
    protected AudioSource Door_L_Sound;
    [SerializeField]
    protected AudioSource Door_R_Sound;
    [SerializeField]
    protected AudioClip OpeningSoundClip;
    [SerializeField]
    protected AudioClip ClosingSoundClip;
    [SerializeField]
    protected float WaitTime = 3f;
    [SerializeField]
    protected bool WedgeToolAttached;
    // PRIVATE
    // ACCESS

    public DoorState CurrentState()
    {
        return m_currentState;
    }

    public void RequestState(DoorState rs)
    {
        Debug.Log("Requesting State " + rs + " Current State: " + m_currentState);

        m_requestedState = rs;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Attach and Reference Scripts 
        if (m_elevator == null)
            m_elevator = GameObject.Find("Elevator").GetComponent<Elevator>();

        doorKeySocket.hoverEntered.AddListener(delegate { OnDoorKeyHowered(); });
        doorKeySocket2.hoverEntered.AddListener(delegate { OnDoorKeyHowered(); });
        doorWedgeToolSocket.selectEntered.AddListener(delegate { OnDoorWedgeAttached(); });
        doorWedgeToolSocket.selectExited.AddListener(delegate { OnDoorWedgeRemoved(); });

        m_currentState = DoorState.Idle;
        RequestState(DoorState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_currentState)
        {
            case DoorState.Idle:
                if (m_elevator.CurrentFloor() == CurrentFloorNo && m_elevator.CurrentState() == State.DoorsOpening)
                {
                    //Debug.Log("Landing Doors Aligned and Opening");
                    RequestState(DoorState.DoorsOpening);
                }
                else if (m_elevator.CurrentFloor() == CurrentFloorNo && m_elevator.CurrentState() == State.DoorsClosing)
                {
                    if (WedgeToolAttached == false)
                    {
                        //Debug.Log("Landing Doors Closing");
                        RequestState(DoorState.DoorsClosing);
                    }
                }
                break;
            case DoorState.DoorsOpening:
                // Do door opening behavior
                OpeningDoors();
                break;
            case DoorState.DoorsClosing:
                // Do door opening behavior

                WaitTime -= Time.deltaTime;

                if (WaitTime <= 0)
                {
                    ClosingDoors();
                }
                break;
        }
        if (m_currentState != m_requestedState)
        {
            if (ValidateSwitch() == false)
            {
                Debug.LogError("Current State: " + m_currentState + " failded to switch to Requested State:" + m_requestedState);
            }
            else
            {
                OnExit(m_currentState);
                OnEnter(m_requestedState);
            }
        }
    }

    private bool ValidateSwitch()
    {
        switch (m_currentState)
        {
            case DoorState.Idle:
                if (m_requestedState == DoorState.DoorsOpening || m_requestedState == DoorState.DoorsClosing)
                    return true;
                else
                    return false;
            case DoorState.DoorsOpening:
                if (m_requestedState == DoorState.DoorsClosing || m_requestedState == DoorState.Idle)
                    return true;
                else
                    return false;
            case DoorState.DoorsClosing:
                if (m_requestedState == DoorState.Idle || m_requestedState == DoorState.DoorsOpening)
                    return true;
                else
                    return false;
        }
        return true;
    }

    private void OnExit(DoorState stateToExit)
    {
        switch (stateToExit)
        {
            case DoorState.Idle:
                break;
            case DoorState.DoorsOpening:
                Door_L_Sound.Stop();
                Door_R_Sound.Stop();
                break;
            case DoorState.DoorsClosing:
                Door_L_Sound.Stop();
                Door_R_Sound.Stop();
                WaitTime = 3f;
                break;
        }
    }

    private void OnEnter(DoorState stateToEnter)
    {
        switch (stateToEnter)
        {
            case DoorState.Idle:
                break;
            case DoorState.DoorsOpening:
                Door_L_Sound.clip = OpeningSoundClip;
                Door_L_Sound.Play();
                //Door_R_Sound.clip = OpeningSoundClip;
                //Door_R_Sound.Play();
                break;
            case DoorState.DoorsClosing:
                //Door_L_Sound.clip = ClosingSoundClip;
                //Door_L_Sound.Play();
                //Door_R_Sound.clip = ClosingSoundClip;
                //Door_R_Sound.Play();
                break;
        }

        m_currentState = stateToEnter;
    }

    private void OpeningDoors()
    {
        Vector3 OpenPos_L = elevatorDoorLTargetOpened.position;
        Vector3 OpenPos_R = elevatorDoorRTargetOpened.position;

        elevatorDoorL.position = Vector3.MoveTowards(elevatorDoorL.position, OpenPos_L, Time.deltaTime * DoorSpeed);
        elevatorDoorR.position = Vector3.MoveTowards(elevatorDoorR.position, OpenPos_R, Time.deltaTime * DoorSpeed);
            
        if (elevatorDoorL.position == OpenPos_L)
        {
            RequestState(DoorState.Idle);
            //RequestState(DoorState.DoorsClosing);
        }
    }

    private void ClosingDoors()
    {
        Vector3 ClosePos_L = elevatorDoorLTargetClosed.position;
        Vector3 ClosePos_R = elevatorDoorRTargetClosed.position;

        //WaitTime -= Time.deltaTime;

        //if (WaitTime <= 0)
        //{
            //if (m_doorSensor.GetComponent<ObstacleDetection>().ObstaclePresent() == false)
            /*if (m_elevator.CurrentState() == State.DoorsClosing)
            {


            if (m_elevator.CurrentState() == State.Idle)
            {
                RequestState(DoorState.Idle);
            }
            }*/
            if (m_elevator.CurrentState() == State.DoorsOpening)
            {
                RequestState(DoorState.DoorsOpening);
            }
            else 
            {
                elevatorDoorL.position = Vector3.MoveTowards(elevatorDoorL.position, ClosePos_L, Time.deltaTime * DoorSpeed);
                elevatorDoorR.position = Vector3.MoveTowards(elevatorDoorR.position, ClosePos_R, Time.deltaTime * DoorSpeed);

                if (elevatorDoorL.position == ClosePos_L)
                {
                    RequestState(DoorState.Idle);
                }
            }
        //}        
    }

    private void OnDoorWedgeAttached()
    {
        WedgeToolAttached = true;
    }

    private void OnDoorWedgeRemoved()
    {
        WedgeToolAttached = false;
    }

    private void OnDoorKeyHowered()
    {
        if (CurrentState() == DoorState.Idle)
        {
            RequestState(DoorState.DoorsOpening);
        }
    }
}
