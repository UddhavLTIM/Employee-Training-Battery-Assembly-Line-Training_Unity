using UnityEngine;

//Concrete State
public class ElevatotDoorOperatingState : ElevatorBaseState
{
    public Transform elevatorDoor_Left;
    public Transform elevatorDoor_Right;

    float doorDistance = 0f;
    float doorSpeed = 5f;
    float doorOpenDistance = 0.5f;

    Vector3 doorLeftOpenPosition;
    Vector3 doorRightOpenPosition;

    public override void EnterState(ElevatorStateManager elevator)
    {
        Debug.Log("The Elevator Doors are operating");

        doorLeftOpenPosition = new Vector3( elevatorDoor_Left.transform.position.x,
                                            elevatorDoor_Left.transform.position.y,
                                            elevatorDoor_Left.transform.position.z - doorOpenDistance);
        doorRightOpenPosition = new Vector3(elevatorDoor_Right.transform.position.x,
                                            elevatorDoor_Right.transform.position.y,
                                            elevatorDoor_Right.transform.position.z + doorOpenDistance);
    }

    public override void UpdateState(ElevatorStateManager elevator)
    {
        // Open the doors
        while (doorDistance < doorOpenDistance)
        {
            // Move doors towards open position
            elevatorDoor_Left.transform.position = Vector3.MoveTowards(elevatorDoor_Left.transform.position, doorLeftOpenPosition, doorSpeed * Time.deltaTime);
            elevatorDoor_Right.transform.position = Vector3.MoveTowards(elevatorDoor_Right.transform.position, doorRightOpenPosition, doorSpeed * Time.deltaTime);

            // Calculate current door distance
            doorDistance = Vector3.Distance(elevatorDoor_Left.transform.position, doorLeftOpenPosition);
        }

        // Close the doors after a delay of 5 seconds
        float doorCloseTime = Time.time + 5f;
        doorDistance = doorOpenDistance;
        Vector3 doorLeftClosedPosition = new Vector3(   elevatorDoor_Left.transform.position.x, 
                                                        elevatorDoor_Left.transform.position.y, 
                                                        elevatorDoor_Left.transform.position.z + doorOpenDistance);
        Vector3 doorRightClosedPosition = new Vector3(  elevatorDoor_Right.transform.position.x, 
                                                        elevatorDoor_Right.transform.position.y, 
                                                        elevatorDoor_Right.transform.position.z - doorOpenDistance);

        while (doorDistance > 0f)
        {
            // Move doors towards closed position
            elevatorDoor_Left.transform.position = Vector3.MoveTowards(elevatorDoor_Left.transform.position, doorLeftClosedPosition, doorSpeed * Time.deltaTime);
            elevatorDoor_Right.transform.position = Vector3.MoveTowards(elevatorDoor_Right.transform.position, doorRightClosedPosition, doorSpeed * Time.deltaTime);

            // Calculate current door distance
            doorDistance = Vector3.Distance(elevatorDoor_Left.transform.position, doorLeftClosedPosition);

            // Check if the delay time has passed
            if (Time.time >= doorCloseTime)
            {
                break;
            }
        }

        elevator.SwitchState(elevator.IdleState);
    }
    public override void OnCollisionEnter(ElevatorStateManager elevator, Collision collision)
    {

    }
}