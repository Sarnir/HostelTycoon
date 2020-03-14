using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee: Person
{
    public int Wage { get; private set; }
    Task assignedTask;
    public string Title
    { get
        {
            return assignedTask == null ? "Freeloader" : "Handyman";
        }
    }

    public Employee(Hostel hostel) : base(hostel)
    {
        Wage = Random.Range(0, 100);
    }

    public void GiveRaise(int raise)
    {
        Wage += raise;
    }

    public void AssignTask(TaskType taskType)
    {
        Task task = new Task(hostel, taskType);
        assignedTask = task;

        Debug.Log($"{ Name } is now { taskType }");
    }

    public TaskType GetCurrentTask()
    {
        if (assignedTask == null)
            return TaskType.Idle;
        return assignedTask.TaskType;
    }

    public void Work()
    {
        if(assignedTask != null)
            assignedTask.WorkOn();
    }

}
