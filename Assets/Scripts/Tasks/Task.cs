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

    public Task(Hostel _hostel, TaskType type)
    {
        hostel = _hostel;
        TaskType = type;
    }

    public void WorkOn()
    {
        switch (TaskType)
        {
            case TaskType.Idle:
                break;
            case TaskType.FrontDesk:
                break;
            case TaskType.Cleaning:
                hostel.Qualities[HostelQuality.Cleanliness] += Random.Range(1, 4);
                Debug.Log($"Hostel cleanliness in now { hostel.Qualities[HostelQuality.Cleanliness] }%");
                break;
            case TaskType.Maintenance:
                var vendingMachines = hostel.FindItems(ItemId.VendingMachine);

                foreach (VendingMachine machine in vendingMachines)
                {
                    machine.Restock(hostel);
                }
                break;
            default:
                break;
        }
    }
}
