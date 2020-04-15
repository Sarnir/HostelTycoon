using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShouldCleanDecision", menuName = "StateMachine/Decisions/ShouldCleanDecision")]
public class ShouldCleanDecision : Decision
{
    public override bool Check(StateMachine controller)
    {
        if (controller.Agent is Employee)
        {
            var employee = controller.Agent as Employee;

            return employee.GetCurrentTask() == TaskType.Cleaning;
        }
        else
            return false;
    }
}
