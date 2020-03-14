using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContextMenu : UIPanel
{
    [SerializeField]
    Toggle ContextMenuItemPrefab = default;

    [SerializeField]
    ToggleGroup content = default;

    override protected void OnOpened()
    {
        transform.SetAsLastSibling();

        // jeśli jest dirty to odśwież - fillcontent
    }

    protected override void Init()
    {
        base.Init();
    }

    // TODO: poprawić to gówno
    public void ClearMenu()
    {
        foreach (RectTransform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    // TODO: poprawić to gówno
    public void AddOption(string text, int id, bool isSet, Action<int> callback)
    {
        var menuItem = Instantiate(ContextMenuItemPrefab, content.transform);
        menuItem.GetComponentInChildren<Text>().text = text;
        menuItem.group = content;
        menuItem.isOn = isSet;
        menuItem.onValueChanged.AddListener(isOn => { if (isOn) callback(id); });
    }

    public void SetContent()
    {
        // ustaw content, sprawdź czy ostatni content był inny niż ten i jeśli tak to ustaw na dirty
    }
}
