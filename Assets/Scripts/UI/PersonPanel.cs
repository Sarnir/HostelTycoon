using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PersonPanel : Selectable
{
    [SerializeField]
    Image avatar = default;
    [SerializeField]
    Text nameText = default;
    [SerializeField]
    Text lengthOfStayText = default;
    [SerializeField]
    Button button = default;

    public Person Person { get; private set; }
    public PersonData PersonData { get; private set; }

    public Action<PersonPanel> SelectCallback;

    public void Init(Guest guest)
    {
        Person = guest;

        avatar.sprite = guest.Avatar;
        nameText.text = guest.Name;
        lengthOfStayText.text = "Staying for " + guest.LengthOfStay + " days";
        button.gameObject.SetActive(false);
    }

    public void Init(Employee employee)
    {
        Person = employee;
        PersonData = employee.GetData();

        InitWithEmployeeData(Person.GetData());
    }

    public void InitWithEmployeeData(PersonData employeeData)
    {
        PersonData = employeeData;

        avatar.sprite = employeeData.Avatar;
        nameText.text = employeeData.Name;
        lengthOfStayText.text = employeeData.Sex.ToString();
        button.GetComponentInChildren<Text>().text = $"Hire for { employeeData.DesiredWage }$";
        button.gameObject.SetActive(true);
    }

    public void ShowButton(bool show)
    {
        button.gameObject.SetActive(show);
    }

    public void OnButtonClicked(Action<PersonPanel> onButtonClick)
    {
        button.onClick.AddListener(delegate { onButtonClick(this); });
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.Select();

        SelectCallback?.Invoke(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        button.onClick.RemoveAllListeners();
    }
}
