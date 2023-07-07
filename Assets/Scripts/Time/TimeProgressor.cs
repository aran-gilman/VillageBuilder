using UnityEngine;

public class TimeProgressor : MonoBehaviour
{
    [SerializeField]
    private CalendarConfig calendarConfig;

    [SerializeField]
    private CalendarEvents calendarEvents;

    private Calendar calendar;

    private void FixedUpdate()
    {
        calendar.AdvanceTime(Time.fixedDeltaTime);
    }

    private void Awake()
    {
        calendar = Calendar.Get();

        // TODO: Move this wherever other game state initialization will go
        calendar.Initialize(calendarConfig, calendarEvents);
    }
}
