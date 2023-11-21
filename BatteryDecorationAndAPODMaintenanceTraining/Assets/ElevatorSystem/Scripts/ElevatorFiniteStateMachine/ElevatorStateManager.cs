using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Context State
public class ElevatorStateManager : MonoBehaviour
{
    //public Transform elevatorDoor_Left;
    //public Transform elevatorDoor_Right;

    public List<int> calledFloorsToVisit = new List<int>();

    // abstract state not instantiated yet
    ElevatorBaseState currentState;

    // Instantiatiing a new instantce of each concrete states
    public ElevatorIdleState IdleState = new ElevatorIdleState();
    public ElevatorMovingState MovingState = new ElevatorMovingState();
    public ElevatotDoorOperatingState DoorOperatingState = new ElevatotDoorOperatingState();
    // Add more states here

    // Start is called before the first frame update
    void Start()
    {
        // Starting State set
        // ElevatorBaseState is assigned an instance of IdleState
        currentState = IdleState;

        // In this particular instance of IdleState, EnterState function is called 
        currentState.EnterState(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
    // Update is called once per frame
    void Update()
    {
        // UpdateState function called of this Instance
        currentState.UpdateState(this);
    }

    public void SwitchState(ElevatorBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void AddCalledFloorsToVisit(int floor)
    {
        // Add floor to list of floors to visit
        calledFloorsToVisit.Add(floor);
    }
}
