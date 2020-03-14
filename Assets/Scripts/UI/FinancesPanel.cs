using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinancesPanel: BaseListPanel
{
    [SerializeField]
    FinancesRow rowPrefab = default;

    IPricesCollection allPrices;

    // w inice czy czymś złap globalacces all prices i zrób rowy

    protected override void Init()
    {
        base.Init();

        allPrices = GlobalAccess.GetAllPrices();
    }

    protected override void PopulateList()
    {
        if (allPrices == null)
            return;

        foreach (Price price in allPrices)
        {
            if (price.IsUsed)
            {
                var newRow = Instantiate(rowPrefab, scrollRectContent);
                newRow.Init(price);
                //newRow.OnBuyClicked(hostel.BuyNewItem);
            }
        }
    }
}
