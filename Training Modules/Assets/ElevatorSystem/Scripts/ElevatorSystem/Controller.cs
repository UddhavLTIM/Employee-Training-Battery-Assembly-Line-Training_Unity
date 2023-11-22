/**
=====================
// license at:
Name:			Control.cs
Version:		0.1
Update Date:	15, April, 2023
Author:			Uddhav Labde
Description:	The controller class takes accepts inputs from controls and sorts them so that the elevator class can access it whenever needed
=====================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // PUBLIC
    // PROTECTED
    [SerializeField]
    protected Elevator m_elevator;
    [SerializeField]
    protected List<int> m_elevatorCalls; // Alternatively can use Tuple to store both source and direction values
    [SerializeField]
    protected List<Direction> m_callDirections;
    //[SerializeField]
    //protected List<int> m_elevatorCallsCopy;
    [SerializeField]
    protected Light m_carTopLight;
    [SerializeField]
    protected Material GlassEmmissionMaterial;
    [SerializeField]
    protected Material GlassMaterial;
    [SerializeField]
    protected AudioSource Alarm;
    // PRIVATE

    // ACCESS

    /// <summary>
    /// In Car Functionality
    /// </summary>
    // Gets called floor number (and direction) and does the appropriate function
    public void AddElevatorCall(int calledFloor, Direction calledDirection)
    {
        // the elevator is about to close its doors, this opens the doors
        if (m_elevatorCalls.Contains(calledFloor))
        {
            if (m_elevator.CurrentFloor() == calledFloor)
            {
                if (m_elevator.CurrentState() == State.DoorsClosing)
                {
                    m_elevator.RequestState(State.DoorsOpening);
                }
            }
            else
            {
                return;
            }
        }
        else if (!m_elevatorCalls.Contains(calledFloor))
        {
            // if the elevator is stationary on the same floor and doors are closed, this opens the doors
            if (m_elevator.CurrentFloor() == calledFloor)
            {
                m_elevator.RequestState(State.DoorsOpening);
            }
            // adds a new call to the calls list
            else if (m_elevator.CurrentFloor() != calledFloor)
            {
                // Make a copy of the original elevator calls list 
                //m_elevatorCallsCopy = new List<int>(m_elevatorCalls);

                m_elevatorCalls.Add(calledFloor);
                m_callDirections.Add(calledDirection);
                //m_elevatorCalls.Add(calledFloor, calledDirection); // Alternatively, could use Tuple to send both source and direction values
            }
        }
    }

    public void ElevatorDoorsOpen()
    {
        if (m_elevator.CurrentState() != State.DoorsOpening && m_elevator.CurrentState() != State.Moving)
        {
            m_elevator.RequestState(State.DoorsOpening);
        }
    }

    public void ElevatorDoorsClose()
    {
        m_elevator.RequestState(State.DoorsClosing);
    }

    public void SafelyStop()
    {
        // Take elevator to the current floor and stop let the passanger leave
        if (m_elevator.CurrentState() == State.Moving)
        {
            m_elevator.SetTargetFloor(m_elevator.CurrentFloor());
            m_elevatorCalls.Clear();
        }
    }

    public void PlayAlarm()
    {
        Alarm.Play();
    }

    public void StopAlarm()
    {
        Alarm.Stop();
    }

    /// <summary>
    /// Car Top Functionality
    /// </summary>
    public void ManualMoveStart(Direction dir)
    {
        if (dir == Direction.UP)
        {
            m_elevator.RequestState(State.ManualMovingUp);
        }
        else if (dir == Direction.DOWN)
        {
            m_elevator.RequestState(State.ManualMovingDown);
        }
    }

    public void ManualMoveStop()
    {
        m_elevator.RequestState(State.Idle);
    }

    public void ElevatorDoorsStayOpen()
    {
        if (m_elevator.CurrentState() != State.DoorsOpening && m_elevator.CurrentState() != State.Moving)
        {
            m_elevator.RequestState(State.DoorsOpening);
            // Request doors to stay open - elevator should become idle
        }
    }

    public void ManualCloseDoorsStart()
    {
        m_elevator.RequestState(State.ManualDoorsClosing);
    }

    public void ManualCloseDoorTest()
    {
        if (m_elevator.IsDoorClosed() == true)
        {
            m_elevator.RequestState(State.Idle);
        }
        else
        {
            m_elevator.RequestState(State.DoorsOpening);
        }
    }

    public void ToggleLight()
    {
        m_carTopLight.enabled = !m_carTopLight.enabled;

        if (m_carTopLight.enabled == true)
        {
            m_carTopLight.GetComponentInParent<MeshRenderer>().material = GlassEmmissionMaterial;
        }
        else
        {
            m_carTopLight.GetComponentInParent<MeshRenderer>().material = GlassMaterial;
        }
    }


    /// <summary>
    /// Class Functionality
    /// </summary>
    public void RemoveServedCall()
    {
        if (m_elevatorCalls.Count != 0)
        {
            m_elevatorCalls.RemoveAt(0);
        }
        else
        {
            //Debug.LogError("There are no calls to remove");
        }
    }

    public void RemoveAllCalls()
    {
        m_elevatorCalls.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Attach and Reference Scripts 
        if (m_elevator == null)
        {
            if (this.GetComponent<Elevator>() == null)
            {
                //this.gameObject.AddComponent<Controller>();
                //m_controler = this.GetComponent<Controller>();
            }
            else
            {
                m_elevator = this.GetComponent<Elevator>();
            }
        }

        //Alarm = m_elevator.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //SequenceCalls();
        CommandLogic();
    }

    // LOOK scheduling algorithm
    // Takes in the the sequence list of requests, current floor of the elevator and current travel direction
    List<int> Look(List<int> requests, int currentFloor, Direction currentDirection)
    {
        int seek_count = 0;
        int distance, cur_track;

        List<int> down = new List<int>();
        List<int> up = new List<int>();
        List<int> seek_sequence = new List<int>();

        // Appending values which are
        // currently at up and down
        // direction from the currentFloor.
        for (int i = 0; i < requests.Count; i++)
        {
            if (requests[i] < currentFloor)
                down.Add(requests[i]);
            if (requests[i] > currentFloor)
                up.Add(requests[i]);
        }

        // Sorting up and down vectors
        // for servicing tracks in the
        // correct sequence.

        down.Sort();
        up.Sort();

        // Run the while loop two times.
        // one by one scanning down
        // and up side of the current floor
        int run = 2;
        while (run-- > 0)
        {
            if (currentDirection == Direction.DOWN)
            {
                for (int i = down.Count - 1; i >= 0; i--)
                {
                    cur_track = down[i];

                    // Appending current track to
                    // seek sequence
                    seek_sequence.Add(cur_track);

                    // Calculate absolute distance
                    distance = Mathf.Abs(cur_track - currentFloor);

                    // Increase the total count
                    seek_count += distance;

                    // Accessed track is now the new head
                    currentFloor = cur_track;
                }
                // Reversing the direction
                currentDirection = Direction.UP;
            }
            else if (currentDirection == Direction.UP)
            {
                for (int i = 0; i < up.Count; i++)
                {
                    cur_track = up[i];

                    // Appending current track to
                    // seek sequence
                    seek_sequence.Add(cur_track);

                    // Calculate absolute distance
                    distance = Mathf.Abs(cur_track - currentFloor);

                    // Increase the total count
                    seek_count += distance;

                    // Accessed track is now new head
                    currentFloor = cur_track;
                }
                // Reversing the direction
                currentDirection = Direction.DOWN;
            }
        }

        //Debug.Log("Total number of seek " + "operations = " + seek_count);

        //Debug.Log("Seek Sequence is");

        for (int i = 0; i < seek_sequence.Count; i++)
        {
            //Debug.Log(seek_sequence[i]);
        }

        return seek_sequence;
    }
    
    // Send commands to next to elevator
    void CommandLogic()
    {
        if (m_elevatorCalls.Count != 0)
        {
            m_elevator.SetTargetFloor(m_elevatorCalls[0]);
        }
        else if (m_elevatorCalls.Count == 0)
        {
            
        }
        //else if (m_elevator.CurrentState != ElevatorMover.States.Moving)
        {

        }
        //else if (m_elevator.CurrentState == ElevatorMover.States.Moving)
        {
            //float direction = m_elevatorMover.MovingDirection();
            //int currentFloor = m_elevatorMover.CurrentFloor();
        }
    }

    void SequenceCalls()
    {
        //if (m_elevatorCalls.Count != 0)
        //{
        // Check if the length of elevator calls is changed -> add updated list to LOOK
        //if (!m_elevatorCalls.SequenceEqual(m_elevatorCallsCopy))
        //{
        //Console.WriteLine("New call has been updated.");

        // LOOK Disk Scheduling algorithm
        List<int> m_callSequence = Look(m_elevatorCalls, m_elevator.CurrentFloor(), m_callDirections[0]);
        m_elevatorCalls = m_callSequence;
        //}
        //}
    }

}
