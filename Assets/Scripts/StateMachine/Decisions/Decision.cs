using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogicOperator
{
    Equal,
    Less,
    Greater
}

public abstract class Decision : ScriptableObject
{
    public LogicOperator condition;

    public abstract bool Check(StateMachine controller);
}
