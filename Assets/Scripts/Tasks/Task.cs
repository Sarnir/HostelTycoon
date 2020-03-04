using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    Idle,
    FrontDesk,
    Cleaning,
    Maintenance
}

public class Task
{
    Hostel hostel;
    
    public TaskType TaskType { get; private set; }

    public Task(Hostel _hostel)
    {
        hostel = _hostel;
        TaskType = TaskType.Maintenance;
    }

    public void WorkOn()
    {
        var vendingMachines = hostel.FindItems(ItemId.VendingMachine);

        foreach (VendingMachine machine in vendingMachines)
        {
            machine.Restock(hostel);
        }
    }
}
