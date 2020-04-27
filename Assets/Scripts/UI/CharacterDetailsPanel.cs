using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailsPanel : UIPanel
{
    Hostel hostel;
    Person person;
    DetailCamera detailCamera;

    [SerializeField]
    Text NameLabel = default;
    [SerializeField]
    Image AvatarImage = default;
    [SerializeField]
    Text CurrentRoleLabel = default;
    [SerializeField]
    Text CashLabel = default;
    [SerializeField]
    Text CurrentTaskLabel = default;

    protected override void Init()
    {
        base.Init();

        hostel = FindObjectOfType<Hostel>();
        detailCamera = FindObjectOfType<DetailCamera>();
    }

    protected override void OnOpened()
    {
        base.OnOpened();

        Refresh();
    }

    public void SetTarget(Person target)
    {
        if (needsInit)
            Init();

        person = target;
        detailCamera.Target = target;
        Refresh();
    }

    void Refresh()
    {
        if(person != null)
        {
            NameLabel.text = person.Name;
            AvatarImage.sprite = person.Avatar;
            CurrentRoleLabel.text = person is Guest ? "Guest" : "Employee";
            CashLabel.text = person.MoneyAmount + "$ in wallet";
            CurrentTaskLabel.text = person.CurrentState;
        }
    }
}
