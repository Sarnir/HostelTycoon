using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyStaffPanel : BaseListPanel
{

    [SerializeField]
    PersonPanel EmployeePanelPrefab = default;

    [SerializeField]
    Transform EmployeeDetailsPanel = default;
    [SerializeField]
    Transform NoEmployeesPanel = default;

    [SerializeField]
    Image avatar = default;
    [SerializeField]
    Text nameText = default;
    [SerializeField]
    Text titleText = default;
    [SerializeField]
    Text wageText = default;

    Employee currentEmployee;

    [SerializeField]
    ContextMenu contextMenu;


    protected override void Init()
    {
        base.Init();

    }

    protected override void PopulateList()
    {
        var persons = hostel.Staff;

        foreach (var person in persons)
        {
            var newGuestPanel = Instantiate(EmployeePanelPrefab, scrollRectContent);
            newGuestPanel.Init(person);
            newGuestPanel.ShowButton(false);
            newGuestPanel.SelectCallback = SelectPerson;
        }

        if (persons.Length > 0)
        {
            ShowDetails(persons[0]);

            EmployeeDetailsPanel.gameObject.SetActive(true);
            NoEmployeesPanel.gameObject.SetActive(false);
        }
        else
        {
            EmployeeDetailsPanel.gameObject.SetActive(false);
            NoEmployeesPanel.gameObject.SetActive(true);
        }
    }

    void SelectPerson(PersonPanel panel)
    {
        ShowDetails(panel.Person as Employee);
    }

    void ShowDetails(Employee employee)
    {
        currentEmployee = employee;

        avatar.sprite = employee.Avatar;
        nameText.text = employee.Name;
        titleText.text = employee.Title;
        wageText.text = $"{ employee.Wage }$";
    }

    public void GiveRaise()
    {
        currentEmployee.GiveRaise(5);
        wageText.text = $"{ currentEmployee.Wage }$";
    }

    public void FireEmployee()
    {
        hostel.FireEmployee(currentEmployee);
        RefreshContent();
    }

    public void AssignTasks()
    {
        // show context menu
        // fill it with possible tasks and current task
        // close menu after checking some task


        // BARDZO ŹLE XD
        contextMenu.ClearMenu();

        foreach(var task in Enum.GetNames(typeof(TaskType)))
        {
            contextMenu.AddOption(task, currentEmployee.GetCurrentTask().ToString() == task);
        }

        contextMenu.Open();

        //currentEmployee.AssignTask(TaskType.Cleaning);
    }
}
