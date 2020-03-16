using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireEmployeesPanel : BaseListPanel
{

    [SerializeField]
    PersonPanel EmployeePanelPrefab = default;

    List<PersonData> candidatesRoster;

    protected override void Init()
    {
        base.Init();

        hostel.OnNewWeek += GenerateRoster;
        
        candidatesRoster = new List<PersonData>();

        GenerateRoster();
    }

    void GenerateRoster()
    {
        candidatesRoster.Clear();
        int roster = Random.Range(2, 10);

        for (int i = 0; i < roster; i++)
        {
            PersonData candidate = new PersonData();
            candidatesRoster.Add(candidate);
        }

        RefreshContent();
    }

    protected override void PopulateList()
    {
        var persons = candidatesRoster;

        foreach (var person in persons)
        {
            var newGuestPanel = Instantiate(EmployeePanelPrefab, scrollRectContent);
            newGuestPanel.InitWithEmployeeData(person);
            newGuestPanel.OnButtonClicked(HireEmployee);
        }
    }

    void HireEmployee(PersonPanel panel)
    {
        Employee employee = panel.PersonData.SpawnEmployee(hostel);
        hostel.HireEmployee(employee);
        candidatesRoster.Remove(panel.PersonData);

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
