using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestsListPanel: BaseListPanel
{
    [SerializeField]
    PersonPanel GuestPanelPrefab = default;

    protected override void Init()
    {
        base.Init();
    }

    protected override void PopulateList()
    {
        var guests = hostel.Guests;

        foreach(var guest in guests)
        {
            var newGuestPanel = Instantiate(GuestPanelPrefab, scrollRectContent);
            newGuestPanel.Init(guest);
        }
    }
}
