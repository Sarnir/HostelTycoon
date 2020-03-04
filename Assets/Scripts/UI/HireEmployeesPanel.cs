using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireEmployeesPanel : BaseListPanel
{

    [SerializeField]
    PersonPanel EmployeePanelPrefab = default;

    List<Employee> candidatesRoster;

    protected override void Init()
    {
        base.Init();

        hostel.OnNewWeek += GenerateRoster;
        
        candidatesRoster = new List<Employee>();

        GenerateRoster();
    }

    void GenerateRoster()
    {
        candidatesRoster.Clear();
        int roster = Random.Range(2, 10);

        for (int i = 0; i < roster; i++)
        {
            Employee candidate = new Employee(hostel);
            candidatesRoster.Add(candidate);
        }
    }

    protected override void PopulateList()
    {
        var persons = candidatesRoster;

        foreach (var person in persons)
        {
            var newGuestPanel = Instantiate(EmployeePanelPrefab, scrollRectContent);
            newGuestPanel.Init(person);
            newGuestPanel.OnButtonClicked(HireEmployee);
        }
    }

    void HireEmployee(PersonPanel panel)
    {
        Employee employee = panel.Person as Employee;
        hostel.HireEmployee(employee);
        candidatesRoster.Remove(employee);

        panel.gameObject.SetActive(false);
        Destroy(panel);
    }

    private void OnDestroy()
    {
        hostel.OnNewWeek -= GenerateRoster;
    }

    public override void RefreshContent()
    {
        base.RefreshContent();
    }
}
