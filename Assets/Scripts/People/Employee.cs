using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : Person
{
    public int Wage { get; private set; }
    Task assignedTask;
    public string Title
    { get
        {
            return assignedTask == null ? "Freeloader" : "Handyman";
        }
    }

    public override void Init(Hostel hostel, PersonData pData)
    {
        base.Init(hostel, pData);
        Wage = pData.DesiredWage;
    }

    public void GiveRaise(int raise)
    {
        Wage += raise;
    }

    public void AssignTask(TaskType taskType)
    {
        Task task = new Task(hostel, taskType);
        assignedTask = task;

        Debug.Log($"{ data.Name } is now { taskType }");
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

    public void Fire()
    {
        Destroy(gameObject);
    }
}
