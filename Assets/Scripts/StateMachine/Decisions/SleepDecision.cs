using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SleepDecision", menuName = "StateMachine/Decisions/SleepDecision")]
public class SleepDecision : Decision
{
    public float SleepingThreshold;

    public override bool Check(StateMachine controller)
    {
        if(condition == LogicOperator.Equal)
            return controller.Agent.SleepingNeed == SleepingThreshold;
        else if (condition == LogicOperator.Less)
            return controller.Agent.SleepingNeed < SleepingThreshold;
        else if (condition == LogicOperator.Greater)
            return controller.Agent.SleepingNeed > SleepingThreshold;

        return false;
    }
}
