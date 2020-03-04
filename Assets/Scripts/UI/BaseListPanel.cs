using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseListPanel: UIPanel
{
    protected Hostel hostel;

    protected RectTransform scrollRectContent;

    override protected void OnOpened()
    {
        RefreshContent();
    }

    protected override void Init()
    {
        base.Init();

        hostel = FindObjectOfType<Hostel>();

        scrollRectContent = gameObject.GetComponentInChildren<ScrollRect>().content;
    }

    public virtual void RefreshContent()
    {
        foreach (RectTransform child in scrollRectContent)
        {
            Destroy(child.gameObject);
        }

        PopulateList();
    }

    protected virtual void PopulateList()
    {
    }
}
