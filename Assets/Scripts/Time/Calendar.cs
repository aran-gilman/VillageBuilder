using System;
using UnityEngine;

[Serializable]
public class Calendar
{
    private static Calendar instance;
    public static Calendar Get()
    {
        if (instance == null)
        {
            instance = new Calendar();
        }
        return instance;
    }

    public float CurrentSecondsInDay { get; private set; }
    public int CurrentDayInSeason { get; private set; }
    private int currentSeasonIndex;
    public Season CurrentSeason { get => calendarConfig.Seasons[currentSeasonIndex].Season; }
    public int CurrentYear { get; private set; }
    public int TotalDaysInCurrentSeason { get => calendarConfig.Seasons[currentSeasonIndex].DaysPerSeason; }

    public Date CurrentDate { get; private set; }

    private CalendarConfig calendarConfig;
    private CalendarEvents calendarEvents;

    public void AdvanceTime(float seconds)
    {
        CurrentSecondsInDay += seconds;
        if (CurrentSecondsInDay > calendarConfig.SecondsPerDay)
        {
            CurrentSecondsInDay -= calendarConfig.SecondsPerDay;
            CurrentDayInSeason += 1;
            if (CurrentDayInSeason >= TotalDaysInCurrentSeason)
            {
                CurrentDayInSeason = 0;
                currentSeasonIndex += 1;
                if (currentSeasonIndex >= calendarConfig.Seasons.Length)
                {
                    CurrentYear += 1;
                    currentSeasonIndex = 0;
                    calendarEvents.NewYearEvent.Raise(CurrentYear);
                }
                calendarEvents.NewSeasonEvent.Raise(CurrentSeason);
            }
            calendarEvents.NewDayEvent.Raise(CurrentDayInSeason);

            CurrentDate = new Date() { Day = CurrentDayInSeason, Season = CurrentSeason, Year = CurrentYear };
            calendarEvents.NewDateEvent.Raise(CurrentDate);
        }
    }

    public void Initialize(CalendarConfig config, CalendarEvents events)
    {
        Initialize(config, events, 0, 0, 0, 0);
    }

    public void Initialize(CalendarConfig config, CalendarEvents events, float seconds, int day, Season season, int year)
    {
        for (int i = 0; i < calendarConfig.Seasons.Length; i++)
        {
            if (calendarConfig.Seasons[i].Season == season)
            {
                Initialize(config, events, seconds, day, i, year);
                return;
            }
        }
        Debug.LogError($"Tried to initialize calendar with season {season.name}, but this season does not exist in the config");
        Initialize(config, events, seconds, day, 0, year);
    }

    public void Initialize(CalendarConfig config, CalendarEvents events, float seconds, int day, int seasonIndex, int year)
    {
        CurrentSecondsInDay = seconds;
        CurrentDayInSeason = day;
        currentSeasonIndex = seasonIndex;
        CurrentYear = year;
        calendarConfig = config;
        calendarEvents = events;
        CurrentDate = new Date() { Day = CurrentDayInSeason, Season = CurrentSeason, Year = CurrentYear };
    }

    private Calendar() { }
}
