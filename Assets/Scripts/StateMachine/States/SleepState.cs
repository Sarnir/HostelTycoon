using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SleepState", menuName = "StateMachine/States/SleepState")]
public class SleepState : State
{

    public override void EnterState(StateMachine controller)
    {
    }

    public override void UpdateState(StateMachine controller)
    {
        if (controller.Agent is Guest)
        {
            Guest guest = controller.Agent as Guest;

            if (guest.Bed.InUse)
                guest.SleepIncreaseModifier = -2f;
            else if (Vector2.Distance(guest.Bed.transform.position, controller.Agent.CurrentPosition) > 1f)
                controller.Agent.desiredPosition = guest.Bed.transform.position;
            else
                GoToSleep(guest);
        }
        else if(controller.Agent is Employee)
        {
            Employee employee = controller.Agent as Employee;

            var spawnPos = controller.Agent.hostel.World.SpawnPoint.transform.position;
            if (controller.Agent.IsOut)
                employee.SleepIncreaseModifier = -2f;
            else if (Vector2.Distance(spawnPos, employee.CurrentPosition) > 1f)
                employee.desiredPosition = spawnPos;
            else
                employee.IsOut = true;
        }
    }

    public override void ExitState(StateMachine controller)
    {
        controller.Agent.SleepIncreaseModifier = 1f;

        if (controller.Agent is Employee)
        {
            controller.Agent.IsOut = false;
        }
        else if (controller.Agent is Guest)
        {
            Guest guest = controller.Agent as Guest;
            WakeUp(guest);
        }
    }

    public void GoToSleep(Guest agent)
    {
        agent.Animator.SetBool("IsSleeping", true);

        agent.Bed.Use(agent);
    }

    public void WakeUp(Guest agent)
    {
        agent.Animator.SetBool("IsSleeping", false);
    }
}
