using UnityEngine;

[CreateAssetMenu(menuName = "Time/Calendar Events")]
public class CalendarEvents : ScriptableObject
{
    [SerializeField]
    private IntGameEvent newDayEvent;
    public IntGameEvent NewDayEvent => newDayEvent;

    [SerializeField]
    private SeasonGameEvent newSeasonEvent;
    public SeasonGameEvent NewSeasonEvent => newSeasonEvent;

    [SerializeField]
    private IntGameEvent newYearEvent;
    public IntGameEvent NewYearEvent => newYearEvent;
}
