using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public Decision decision;
    public State ToState;
    public bool SwitchIfTrue;

    public bool Check(StateMachine controller)
    {
        return decision.Check(controller) == SwitchIfTrue;
    }
}
