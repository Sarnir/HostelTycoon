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
    Text TimeCounter = default;
    [SerializeField]
    Text RatingCounter = default;

    Slider timeSlider;

    private void Awake()
    {
        timeSlider = TimeCounter.GetComponentInParent<Slider>();
    }

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

    public void UpdateTimeCounter(int hour)
    {
        if (hour > 12)
            TimeCounter.text = $"{hour - 12}PM";
        else
            TimeCounter.text = $"{hour}AM";
    }

    public void UpdateTimeProgressBar(float timePercent)
    {
        timeSlider.value = timePercent;
    }

    public void UpdateRatingCounter(float rating)
    {
        RatingCounter.text = $"✰ { rating.ToString("0.0") }/10";
    }
}
