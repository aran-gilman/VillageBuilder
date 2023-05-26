using UnityEngine;

public class ObjectSettingsPresenter : MonoBehaviour
{
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
            setting.InstantiateControl(transform);
        }
    }

    private void OnEnable()
    {
        UpdateDisplay();
    }
}
