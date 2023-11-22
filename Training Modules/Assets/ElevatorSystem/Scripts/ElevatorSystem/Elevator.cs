/**
=====================
// license at:
Name:			Elevator.cs
Version:		0.1
Update Date:	15, April, 2023
Author:			Uddhav Labde
Description:	Controles the movement and functionality of the elevator cabin
=====================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {Idle, Moving, DoorsOpening, DoorsClosing, ManualMovingUp, ManualMovingDown, ManualDoorsClosing, OutOfOrder}

public enum Direction { UP = 1, DOWN = -1 }

public class Elevator : MonoBehaviour
{
    // PUBLIC
    public static readonly float FloorHeight = 3.7f;
    public static readonly int MinFloor = 0;
    public static readonly int MaxFloor = 5;
    public static readonly float MoveSpeed = 0.5f;
    public static readonly float DoorSpeed = 0.5f;

    // PROTECTED
    [SerializeField]
    protected int m_currentFloor;
    [SerializeField] 
    protected int m_targetFloor;
    [SerializeField] 
    protected State m_currentState;
    [SerializeField] 
    protected State m_requestedState;
    [SerializeField] 
    protected Controller m_controler;
    [SerializeField]
    protected Controls m_controls;
    [SerializeField]
    protected ObstacleDetection m_doorSensor;
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
    [Space(10)]
    [Header("Floors: Index in ascending order")]
    [SerializeField]
    protected List<Transform> m_floors;
    [Space(10)]
    [Header("AudioSources")]
    [SerializeField]
    protected AudioSource ElevatorDoorSound_L;
    [SerializeField]
    protected AudioSource ElevatorDoorSound_R;
    [SerializeField]
    protected AudioClip ElevatorDoorOpenSoundClip;
    [SerializeField]
    protected AudioClip ElevatorDoorCloseSoundClip;
    [SerializeField]
    protected AudioSource ElevatorRideSound;
    [SerializeField]
    protected AudioSource ElecatorDingSound;
    [SerializeField]
    protected AudioSource ElecatorWaitingMusic;
    // PRIVATE
    private float WaitTime = 3f;
    private bool DoorClosed;
    // ACCESS

    /// <summary>
    /// Get Functionality
    /// </summary>    
    public bool IsDoorClosed()
    {
        return DoorClosed;
    }    

    public void SetTargetFloor(int tf)
    {
        if (tf < MinFloor || tf >= MaxFloor)
        {
            Debug.LogError("Invalid Target floor " + tf);
            return;
        }
        m_targetFloor = tf;
    }

    public int CurrentFloor()
    {
        m_currentFloor = Mathf.CeilToInt(transform.position.y / FloorHeight);
        //m_currentFloor = Mathf.Abs(Mathf.CeilToInt(transform.position.y / m_floors[0].position.y - m_floors[1].position.y));
        return m_currentFloor;
    }

    public float MovingDirection()
    {
        if (m_currentState == State.Moving)
        {
            if (GetTravelHeight(m_targetFloor) - transform.position.y > 0f)
                return 1f; //Moving Up
            else
                return -1f; //Moving Down
        }
        return 0f;
    }

    public float GetTravelHeight(int floor)
    {
        //return floor * FloorHeight;
        return m_floors[floor].position.y;
    }

    /// <summary>
    /// State Machine Functionality
    /// </summary>
    public State CurrentState()
    {
        return m_currentState;
    }

    public void RequestState(State rs)
    {
        Debug.Log("Requesting State " + rs + " Current State: " + m_currentState);

        m_requestedState = rs;
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
        if (m_doorSensor == null)
        {
            m_doorSensor = this.GetComponentInChildren<ObstacleDetection>();
        }

        m_currentState = State.Idle;
        RequestState(State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_currentState)
        {
            case State.Idle:
                // Do idle behavior
                if (m_targetFloor == CurrentFloor())
                {
                    //Debug.Log("Target Floor is same as Current Floor");
                }
                else if (m_targetFloor != CurrentFloor())
                {
                    RequestState(State.Moving);
                }
                break;
            case State.Moving:
                // Do moving behavior
                UpdateMoving();
                break;
            case State.DoorsOpening:
                // Do door opening behavior
                OpeningDoors();
                break;
            case State.DoorsClosing:
                // Do door opening behavior
                WaitTime -= Time.deltaTime;

                if (WaitTime <= 0)
                {
                    ClosingDoors();
                }
                break;
            case State.ManualMovingUp:
                // Do ManualMovingUp behavior
                ManualUpdateMoving(1);
                break;
            case State.ManualMovingDown:
                // Do ManualMovingDown behavior
                ManualUpdateMoving(-1);
                break;
            case State.ManualDoorsClosing:
                // Do ManualDoorsClosing behavior
                ManualDoorClosing();
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
            case State.Idle:
                if (m_requestedState == State.Moving || m_requestedState == State.DoorsOpening || m_requestedState == State.ManualDoorsClosing || m_requestedState == State.ManualMovingUp || m_requestedState == State.ManualMovingDown)
                    return true;
                else
                    return false;
            case State.Moving:
                if (m_requestedState == State.DoorsOpening) 
                    return true;
                else 
                    return false;
            case State.DoorsOpening:
                if (m_requestedState == State.DoorsClosing || m_requestedState == State.Idle) // || m_requestedState == State.ManualDoorsClosing 
                    return true;
                else
                    return false;
            case State.DoorsClosing:
                if (m_requestedState == State.Idle || m_requestedState == State.DoorsOpening)
                    return true;
                else
                    return false;
            case State.ManualMovingUp:
                if (m_requestedState == State.Idle)
                    return true;
                else
                    return false;
            case State.ManualMovingDown:
                if (m_requestedState == State.Idle)
                    return true;
                else
                    return false;
            case State.ManualDoorsClosing:
                if (m_requestedState == State.Idle || m_requestedState == State.DoorsOpening)
                    return true;
                else 
                    return false;
        }
        return true;
    }

    private void OnExit(State stateToExit)
    {
        switch (stateToExit)
        {
            case State.Idle:
                break;
            case State.Moving:
                ElevatorRideSound.Stop();
                ElecatorWaitingMusic.Stop();
                ElecatorDingSound.Play();
                break;
            case State.DoorsOpening:
                ElevatorDoorSound_L.Stop();
                ElevatorDoorSound_R.Stop();
                break;
            case State.DoorsClosing:
                WaitTime = 3f;

                ElevatorDoorSound_L.Stop();
                ElevatorDoorSound_R.Stop();
                break;
            case State.ManualMovingUp:
                ElevatorRideSound.Stop();
                break;
            case State.ManualMovingDown:
                ElevatorRideSound.Stop();
                break;
            case State.ManualDoorsClosing:
                ElevatorDoorSound_L.Stop();
                ElevatorDoorSound_R.Stop();
                break;
        }
    }

    private void OnEnter(State stateToEnter)
    {
        switch (stateToEnter)
        {
            case State.Idle:
                m_controler.RemoveServedCall();
                break;
            case State.Moving:
                ElevatorRideSound.Play();
                ElecatorWaitingMusic.PlayDelayed(2f);
                break;
            case State.DoorsOpening:
                ElevatorDoorSound_L.clip = ElevatorDoorOpenSoundClip;
                ElevatorDoorSound_R.clip = ElevatorDoorOpenSoundClip;

                ElevatorDoorSound_L.Play();
                ElevatorDoorSound_R.Play();
                break;
            case State.DoorsClosing:
                ElevatorDoorSound_L.clip = ElevatorDoorCloseSoundClip;
                ElevatorDoorSound_R.clip = ElevatorDoorCloseSoundClip;

                ElevatorDoorSound_L.PlayDelayed(3f);
                ElevatorDoorSound_R.PlayDelayed(3f);
                break;
            case State.ManualMovingUp:
                ElevatorRideSound.Play();
                break;
            case State.ManualMovingDown:
                ElevatorRideSound.Play();
                break;
            case State.ManualDoorsClosing:
                ElevatorDoorSound_L.clip = ElevatorDoorCloseSoundClip;
                ElevatorDoorSound_R.clip = ElevatorDoorCloseSoundClip;

                ElevatorDoorSound_L.Play();
                ElevatorDoorSound_R.Play();
                break;
        }
        m_currentState = stateToEnter;
    }

    /// <summary>
    /// In Car Functionality
    /// </summary>

    // Gets the target floor and moves the elevator to that floor
    private void UpdateMoving()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.y = GetTravelHeight(m_targetFloor);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * MoveSpeed);

        //if (transform.position.y == targetPosition.y)
        if (Mathf.Approximately(transform.position.y, targetPosition.y))
        {
            RequestState(State.DoorsOpening);
        }
    }

    // Opens the elevator doors
    private void OpeningDoors()
    {
        Vector3 OpenPos_L = elevatorDoorLTargetOpened.position;
        Vector3 OpenPos_R = elevatorDoorRTargetOpened.position;

        elevatorDoorL.position = Vector3.MoveTowards(elevatorDoorL.position, OpenPos_L, Time.deltaTime * DoorSpeed);
        elevatorDoorR.position = Vector3.MoveTowards(elevatorDoorR.position, OpenPos_R, Time.deltaTime * DoorSpeed);

        //if (elevatorDoorL.position == OpenPos_L)
        if (Mathf.Approximately(elevatorDoorL.position.z, OpenPos_L.z))
        {
            if (m_controls.InCarInspectionMode() == true)
            {
                DoorClosed = false;
                RequestState(State.Idle);
            }
            else if (m_controls.InCarInspectionMode() == false)
            {
                DoorClosed = false;
                RequestState(State.DoorsClosing);
            }
        }
    }

    // Waits for seconds and if no obsticle between doors closed the elevator doors
    private void ClosingDoors()
    {
        Vector3 ClosePos_L = elevatorDoorLTargetClosed.position;
        Vector3 ClosePos_R = elevatorDoorRTargetClosed.position;

        //WaitTime -= Time.deltaTime;

        //if (WaitTime <= 0)
        //{
            if (m_doorSensor.ObstaclePresent() == false)
            {
                elevatorDoorL.position = Vector3.MoveTowards(elevatorDoorL.position, ClosePos_L, Time.deltaTime * DoorSpeed);
                elevatorDoorR.position = Vector3.MoveTowards(elevatorDoorR.position, ClosePos_R, Time.deltaTime * DoorSpeed);

                //if (elevatorDoorL.position == ClosePos_L)
                if (Mathf.Approximately(elevatorDoorL.position.z, ClosePos_L.z))
                {
                    DoorClosed = true;
                    RequestState(State.Idle);
                }
            }
            else
            {
                RequestState(State.DoorsOpening);
            }
        //}
    }

    /// <summary>
    /// Car Top Functionality
    /// </summary>
    private void ManualUpdateMoving(float dir)
    {
        //transform.position += new Vector3 (0, Time.deltaTime * MoveSpeed * dir, 0);
        transform.Translate(new Vector3 (0, dir, 0) * Time.deltaTime * MoveSpeed);
        m_targetFloor = CurrentFloor();
    }

    private void ManualDoorClosing()
    {
        Vector3 ClosePos_L = elevatorDoorLTargetClosed.position;
        Vector3 ClosePos_R = elevatorDoorRTargetClosed.position;

        elevatorDoorL.position = Vector3.MoveTowards(elevatorDoorL.position, ClosePos_L, Time.deltaTime * DoorSpeed);
        elevatorDoorR.position = Vector3.MoveTowards(elevatorDoorR.position, ClosePos_R, Time.deltaTime * DoorSpeed);
        
        if (Mathf.Approximately(elevatorDoorL.position.z, ClosePos_L.z))
        {
            DoorClosed = true; 
            //RequestState(State.Idle);
        }
    }
}
