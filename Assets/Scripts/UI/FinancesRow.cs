using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinancesRow : MonoBehaviour
{
    [SerializeField]
    Text nameText = default;
    [SerializeField]
    Text priceText = default;
    [SerializeField]
    Text profitText = default;

    Price price;

    public void Init(Price _price)
    {
        price = _price;

        nameText.text = price.Id.ToString();
        priceText.text = price.ToString();
        profitText.text = price.ProfitString;

        if(price.Modifiable == false)
        {
            foreach (var button in GetComponentsInChildren<Button>())
                button.gameObject.SetActive(false);
        }
    }

    void UpdateTexts()
    {
        priceText.text = price.ToString();
        profitText.text = price.ProfitString;
    }

    public void IncreasePrice()
    {
        Debug.Log("Price increased");
        price.CurrentPrice += 0.5f;
        UpdateTexts();
    }

    public void DecreasePrice()
    {
        Debug.Log("Price decreased");
        price.CurrentPrice -= 0.5f;
        UpdateTexts();
    }
}
