using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestsListPanel: BaseListPanel
{
    [SerializeField]
    GuestPanel GuestPanelPrefab = default;

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
            newGuestPanel.Avatar.sprite = guest.Avatar;
            newGuestPanel.NameText.text = guest.Name;
            newGuestPanel.LengthOfStayText.text = "staying for " + guest.LengthOfStay + " nights";
        }
    }
}
