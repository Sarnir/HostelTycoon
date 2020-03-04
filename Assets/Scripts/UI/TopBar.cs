using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : UIPanel
{
    [SerializeField]
    Text DayCounter = default;
    [SerializeField]
    Text MoneyCounter = default;
    [SerializeField]
    Text GuestsCounter = default;
    [SerializeField]
    Text SpaceCounter = default;
    [SerializeField]
    Text RatingCounter = default;

    public void UpdateDayCounter(int day)
    {
        DayCounter.text = $"Day { day }";
    }

    public void UpdateMoneyCounter(float money)
    {
        MoneyCounter.text = $"{ money.ToString("0.00") }$";
    }

    public void UpdateGuestsCounter(int guests)
    {
        GuestsCounter.text = $"Guests: { guests}";
    }

    public void UpdateSpaceCounter(int currentSpace, int totalSpace)
    {
        SpaceCounter.text = $"Space: { currentSpace } / { totalSpace}";
    }

    public void UpdateRatingCounter(float rating)
    {
        RatingCounter.text = $"✰ { rating.ToString("0.0") }/10";
    }
}
