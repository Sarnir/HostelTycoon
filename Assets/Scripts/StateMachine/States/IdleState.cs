using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "StateMachine/States/IdleState")]
public class IdleState : State
{
    public override void EnterState(StateMachine controller)
    {
    }

    public override void UpdateState(StateMachine controller)
    {
        if (controller.TimerElapsed)
            SetNewDestination(controller);
    }

    public override void ExitState(StateMachine controller)
    {
    }

    void SetNewDestination(StateMachine controller)
    {
        var oldPos = controller.Agent.CurrentPosition;
        var distanceToNewPos = Random.insideUnitSphere * 3f;
        float newX = oldPos.x + distanceToNewPos.x;
        float newY = oldPos.y + distanceToNewPos.y;

        if(newX > 26f || newX < 0f)
            newX = oldPos.x - distanceToNewPos.x * 2f;
        if (newY > 16f || newY < 0f)
            newY = oldPos.y - distanceToNewPos.y * 2f;

        controller.Agent.desiredPosition = new Vector2(newX, newY);
        controller.SetTimer(Random.Range(2f, 8f));
    }
}
