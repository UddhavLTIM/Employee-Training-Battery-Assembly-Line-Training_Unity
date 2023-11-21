using UnityEngine;

//Concrete State
public class ElevatorIdleState : ElevatorBaseState
{
    public override void EnterState(ElevatorStateManager elevator)
    {
        Debug.Log("The Elevator is in the Idle State");
    }

    public override void UpdateState(ElevatorStateManager elevator)
    {
        if (elevator.calledFloorsToVisit.Count > 0)
        {
            elevator.SwitchState(elevator.MovingState);
        }   
    }

    public override void OnCollisionEnter(ElevatorStateManager elevator, Collision collision)
    {
        
    }
}
