using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CleaningState", menuName = "StateMachine/States/CleaningState")]
public class CleaningState : State
{
    public override void EnterState(StateMachine controller)
    {
        var oldPos = controller.Agent.CurrentPosition;
        var distanceToNewPos = Random.insideUnitSphere * 5f;
        float newX = oldPos.x + distanceToNewPos.x;
        float newY = oldPos.y + distanceToNewPos.y;

        if (newX > 26f || newX < 0f)
            newX = oldPos.x - distanceToNewPos.x * 2f;
        if (newY > 16f || newY < 0f)
            newY = oldPos.y - distanceToNewPos.y * 2f;

        controller.Agent.desiredPosition = new Vector2(newX, newY);
    }

    public override void UpdateState(StateMachine controller)
    {
        if (Vector2.Distance(controller.Agent.desiredPosition, controller.Agent.CurrentPosition) <= 0.05f)
        {
            Clean(controller);
        }
    }

    private void Clean(StateMachine controller)
    {
        controller.Agent.CurrentTile.IncrementDirt(-0.05f);

        if (controller.Agent.CurrentTile.Dirtyness < 0.01f)
            SetNewDestination(controller);
    }

    public override void ExitState(StateMachine controller)
    {
    }

    void SetNewDestination(StateMachine controller)
    {
        var nbs = controller.Agent.CurrentTile.Neighbors;
        var nextTile = nbs[Random.Range(0, 3)];

        for (int i = 0; i < 4; i++)
        {
            if (nbs[i] != null)
            {
                if (nextTile == null || nextTile.Dirtyness < nbs[i].Dirtyness)
                {
                    if(nbs[i].IsEmpty)
                        nextTile = nbs[i];
                }
            }
        }

        controller.Agent.desiredPosition = nextTile.transform.position;
    }
}
