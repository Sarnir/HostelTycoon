using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : Person
{
    public int Wage { get; private set; }
    TaskType assignedTask;
    public string Title
    { get
        {
            return assignedTask == TaskType.Idle ? "Freeloader" : "Handyman";
        }
    }

    public override void Init(Hostel hostel, PersonData pData)
    {
        base.Init(hostel, pData);
        Wage = pData.DesiredWage;
        assignedTask = TaskType.Idle;
    }

    public void GiveRaise(int raise)
    {
        Wage += raise;
    }

    public void AssignTask(TaskType newTask)
    {
        //Task task = new Task(hostel, taskType);
        assignedTask = newTask;

        Debug.Log($"{ data.Name } is now { newTask }");
    }

    public TaskType GetCurrentTask()
    {
        return assignedTask;
    }

    /*public void Work()
    {
        if(assignedTask != null)
            assignedTask.WorkOn();
    }*/

    public void Fire()
    {
        Destroy(gameObject);
    }
}
