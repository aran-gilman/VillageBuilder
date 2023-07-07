using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Time/Calendar Config")]
public class CalendarConfig : ScriptableObject
{
    [SerializeField]
    private int secondsPerDay;
    public int SecondsPerDay => secondsPerDay;

    [Serializable]
    public class SeasonInfo
    {
        public Season Season;
        public int DaysPerSeason;
    }

    [SerializeField]
    private SeasonInfo[] seasons;
    public SeasonInfo[] Seasons => seasons;
}
