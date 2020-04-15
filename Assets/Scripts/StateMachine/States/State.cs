using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    [SerializeField]
    protected Transition[] transitions;

    public abstract void EnterState(StateMachine controller);

    public abstract void UpdateState(StateMachine controller);

    public abstract void ExitState(StateMachine controller);

    public void CheckTransitions(StateMachine controller)
    {
        for(int i = 0; i < transitions.Length; i++)
        {
            if (transitions[i].Check(controller))
                controller.SwitchState(transitions[i].ToState);
        }
    }
}
