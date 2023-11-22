using UnityEngine;

//Concrete State
public class ElevatorMovingState : ElevatorBaseState
{
    int currentFloor = 0;
    float floorHeight = 5f;
    float moveSpeed = 2f;
    int nextFloorToVisit;

    public override void EnterState(ElevatorStateManager elevator)
    {
        Debug.Log("The Elevator is in the Moving State");

        // Get next floor to visit. (There might be multiple floors to visit so which to move towards next.)
        nextFloorToVisit = elevator.calledFloorsToVisit[0];
    }

    public override void UpdateState(ElevatorStateManager elevator)
    {
        // Move elevator towards next floor
        float targetY = nextFloorToVisit * floorHeight;
        Vector3 targetPosition = new Vector3(elevator.transform.position.x, targetY, elevator.transform.position.z);
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if elevator has reached next floor
        if (elevator.transform.position.y == targetY)
        {
            elevator.SwitchState(elevator.DoorOperatingState);

            // Remove floor from list of floors to visit
            elevator.calledFloorsToVisit.RemoveAt(0);

            // Update current floor
            currentFloor = nextFloorToVisit;
        }
    }
    public override void OnCollisionEnter(ElevatorStateManager elevator, Collision collision)
    {

    }
}