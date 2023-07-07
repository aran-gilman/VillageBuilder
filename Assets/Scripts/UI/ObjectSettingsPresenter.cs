using UnityEngine;

public class ObjectSettingsPresenter : MonoBehaviour
{
    [SerializeField]
    private VoidGameEvent refreshEvent;

    private GameObject source;
    public GameObject Source
    {
        get => source;
        set
        {
            GameObject oldSource = source;
            source = value;
            if (gameObject.activeSelf && source != oldSource)
            {
                UpdateDisplay();
            }
        }
    }

    public void SetSourceToNull()
    {
        Source = null;
    }

    private void UpdateDisplay()
    {
        if (Source == null)
        {
            gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        ObjectSetting[] settings = Source.GetComponentsInChildren<ObjectSetting>();
        foreach (ObjectSetting setting in settings)
        {
            if (setting.IsEnabled())
            {
                setting.InstantiateControl(transform);
            }
        }
    }

    private void OnEnable()
    {
        UpdateDisplay();
        refreshEvent.OnGameEvent += HandleRefreshEvent;
    }

    private void OnDisable()
    {
        refreshEvent.OnGameEvent -= HandleRefreshEvent;
    }

    private void HandleRefreshEvent(object sender, object arg)
    {
        UpdateDisplay();
    }
}
