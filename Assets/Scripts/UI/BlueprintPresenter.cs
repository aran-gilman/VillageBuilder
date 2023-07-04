using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlueprintPresenter : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Text blueprintNameDisplay;

    private Blueprint blueprint;
    public Blueprint Blueprint
    {
        get => blueprint;
        set
        {
            blueprint = value;
            UpdateDisplayedInfo();
        }
    }

    public UnityEvent OnClick => button.onClick;

    private void UpdateDisplayedInfo()
    {
        if (blueprint == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        if (blueprintNameDisplay != null)
        {
            blueprintNameDisplay.text = blueprint.name;
        }
    }
}
