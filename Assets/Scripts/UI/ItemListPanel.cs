using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemListPanel: BaseListPanel
{
    [SerializeField]
    ItemPanel ItemPanelPrefab = default;

    [SerializeField]
    Text PageName = default;

    bool withPrices;

    ItemDef[] itemDefs;

    protected override void Init()
    {
        base.Init();
    }

    protected override void PopulateList()
    {
        if (itemDefs == null)
            return;

        foreach(var itemDef in itemDefs)
        {
            var newItemPanel = Instantiate(ItemPanelPrefab, scrollRectContent);
            newItemPanel.Init(itemDef, withPrices);
            newItemPanel.OnBuyClicked(hostel.BuyNewItem);
        }
    }

    public void OpenShop()
    {
        PageName.text = "Shop";
        itemDefs = GlobalAccess.GetItemDefinitions().GetAllDefinitions();
        withPrices = true;
        Open();
    }

    public void OpenInventory()
    {
        if (needsInit)
            Init();

        PageName.text = "Inventory";
        itemDefs = hostel.GetAllItems().Select(item => item.Definition).ToArray();
        withPrices = false;
        Open();
    }
}
