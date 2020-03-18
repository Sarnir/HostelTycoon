using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DailyEvents
{
    CheckOut = 11,
    CheckIn = 14,
    QuietHoursStart = 22,
    NewDay = 24,
    QuietHoursEnd = 6
}

public class GameTime : MonoBehaviour
{
    [SerializeField]
    TopBar topBar = default;

    public static readonly float GameHourInSeconds = 5f;

    int day;
    int hour;
    float elapsedTime;

    DailyEvents lastDailyEvent = DailyEvents.QuietHoursEnd;

    public delegate void DailyEvent(DailyEvents e);
    public delegate void DateEvent();
    public event DateEvent OnNewWeek;
    public event DailyEvent OnDailyEvent;

    void Start()
    {
        day = 1;
        hour = 1;
        elapsedTime = 0f;
        topBar.UpdateDayCounter(day);
        topBar.UpdateTimeCounter(hour);
    }

    public void SetTimeSpeed(int speed)
    {
        Time.timeScale = speed;
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > GameHourInSeconds)
        {
            IncrementHour();
            elapsedTime = 0f;
        }

        topBar.UpdateTimeProgressBar(elapsedTime / GameHourInSeconds);
    }

    void IncrementHour()
    {
        hour++;

        CheckForEvents(hour);

        if (hour > 24)
            NextDay();

        topBar.UpdateTimeCounter(hour);
    }

    void CheckForEvents(int hour)
    {
        DailyEvents currentEvent = (DailyEvents)hour;

        if(lastDailyEvent != currentEvent)
            OnDailyEvent?.Invoke(currentEvent);

        lastDailyEvent = currentEvent;
    }

    public void NextDay()
    {
        hour = 1;
        day++;

        if (day % 7 == 1)
        {
            OnNewWeek?.Invoke();
        }

        elapsedTime = 0f;

        topBar.UpdateTimeCounter(hour);
        topBar.UpdateDayCounter(day);
    }
}
