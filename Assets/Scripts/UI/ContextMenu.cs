using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContextMenu : UIPanel
{
    [SerializeField]
    Toggle ContextMenuItemPrefab = default;

    bool mouseIsOver;

    override protected void OnOpened()
    {
        // jeśli jest dirty to odśwież - fillcontent
    }

    protected override void Init()
    {
        base.Init();
    }

    public void ClearMenu()
    {
        foreach (RectTransform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddOption(string text, bool isSet)
    {
        var menuItem = Instantiate(ContextMenuItemPrefab, this.transform);
        menuItem.GetComponentInChildren<Text>().text = text;
        menuItem.group = GetComponent<ToggleGroup>();
        menuItem.isOn = isSet;
        menuItem.onValueChanged.AddListener(isOn => ToggleValueChanged(text, isOn));
    }

    void ToggleValueChanged(string text, bool isOn)
    {
        Debug.Log($"Option { text } was changed to { isOn }");
        if (isOn)
        {
            Close();
        }
    }

    public void SetContent()
    {
        // ustaw content, sprawdź czy ostatni content był inny niż ten i jeśli tak to ustaw na dirty
    }
}
