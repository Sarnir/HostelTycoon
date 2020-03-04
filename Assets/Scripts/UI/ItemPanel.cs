using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    Image Avatar = default;
    [SerializeField]
    Text NameText = default;
    [SerializeField]
    Button BuyButton = default;
    [SerializeField]
    Text SpaceRequired = default;

    ItemId itemDefId;

    public void Init(ItemDef definition, bool showPrice)
    {
        itemDefId = definition.Id;

        Avatar.sprite = definition.Avatar;
        NameText.text = definition.Name;
        SpaceRequired.text = "Requires " + definition.TilesNeeded + " space";

        BuyButton.GetComponentInChildren<Text>().text = definition.Price + "$";
        BuyButton.gameObject.SetActive(showPrice);
    }

    public void OnBuyClicked(Action<ItemId> onBuyClick)
    {
        BuyButton.onClick.AddListener(delegate { onBuyClick(itemDefId); });
    }

    void OnDestroy()
    {
        BuyButton.onClick.RemoveAllListeners();
    }
}
