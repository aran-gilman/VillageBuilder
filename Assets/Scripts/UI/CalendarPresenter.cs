using UnityEngine;
using UnityEngine.UI;

public class CalendarPresenter : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;

    private string formatString = "";

    public void DisplayDate(Date date)
    {
        if (formatString.Length == 0)
        {
            formatString = textComponent.text;
        }
        textComponent.text = string.Format(formatString, date.Day + 1, date.Season.name, date.Year + 1);
    }

    private void Start()
    {
        DisplayDate(Calendar.Get().CurrentDate);
    }
}
