using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestsListPanel: UIPanel
{
    public GuestPanel GuestPanelPrefab;

    Hostel hostel;

    RectTransform guestsScrollRectContent;

    override protected void OnOpened()
    {
        RefreshGuests();
    }

    protected override void Init()
    {
        base.Init();

        hostel = FindObjectOfType<Hostel>();

        guestsScrollRectContent = gameObject.GetComponentInChildren<ScrollRect>().content;
    }

    void RefreshGuests()
    {
        var guests = hostel.Guests;

        foreach (RectTransform child in guestsScrollRectContent)
        {
            Destroy(child.gameObject);
        }

        foreach(var guest in guests)
        {
            var newGuestPanel = Instantiate(GuestPanelPrefab, guestsScrollRectContent);
            newGuestPanel.Avatar.sprite = guest.Avatar;
            newGuestPanel.NameText.text = guest.Name;
            newGuestPanel.LengthOfStayText.text = "staying for " + guest.LengthOfStay + " nights";
        }
    }
}
