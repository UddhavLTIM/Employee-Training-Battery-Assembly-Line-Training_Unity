using UnityEngine;

//Abstract State
public abstract class ElevatorBaseState
{
    public abstract void EnterState(ElevatorStateManager elevator);
    public abstract void UpdateState(ElevatorStateManager elevator);
    public abstract void OnCollisionEnter(ElevatorStateManager elevator, Collision collision);
}
