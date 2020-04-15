using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    State currentState;

    [HideInInspector]
    public Person Agent;

    float timer;

    public bool TimerElapsed { get { return timer < 0f; } }

    private void Start()
    {
        Agent = GetComponent<Person>();
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
        currentState.CheckTransitions(this);

        timer -= Time.deltaTime;
    }

    public void SwitchState(State newState)
    {
        currentState.ExitState(this);
        newState.EnterState(this);
        currentState = newState;
    }

    public void SetTimer(float time)
    {
        timer = time;
    }
}
