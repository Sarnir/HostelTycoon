using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet
{
    float _money;
    public float Money { get { return _money; } private set { _money = value; OnMoneyChanged?.Invoke(_money); } }

    Action<float> OnMoneyChanged = null;

    public Wallet(float startingMoney)
    {
        Money = startingMoney;
    }

    public Wallet(float startingMoney, Action<float> callback)
    {
        OnMoneyChanged = callback;
        Money = startingMoney;
    }

    public void Pay(float amount, string remark = null)
    {
        Money -= amount;
        if (Money < 0)
            Debug.Log("Watch out, wallet is sub-zero!");
        LogExpenses(amount, remark);
    }

    public void Pay(float amount, Wallet recipient, string remark = null)
    {
        Money -= amount;
        recipient.AddMoney(amount, remark);
        LogExpenses(amount, remark);
    }

    public void AddMoney(float amount, string remark = null)
    {
        Money += amount;
        LogIncome(amount, remark);
    }

    void LogExpenses(float expenses, string type)
    {
        if (OnMoneyChanged == null)
            return;

        if(string.IsNullOrEmpty(type))
            Debug.Log($"<color=red>Paid {expenses.ToString("0.00")}$</color>");
        else
            Debug.Log($"<color=red>Paid {expenses.ToString("0.00")}$ for {type}</color>");
    }

    void LogIncome(float income, string type)
    {
        if (OnMoneyChanged == null)
            return;

        if (string.IsNullOrEmpty(type))
            Debug.Log($"<color=green>Got {income.ToString("0.00")}$</color>");
        else
            Debug.Log($"<color=green>Got {income.ToString("0.00")}$ from {type}</color>");
    }

    public bool CanAfford(float amount)
    {
        return Money >= amount;
    }
}
