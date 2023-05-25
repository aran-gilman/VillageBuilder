using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HaulDesignation))]
public class AutoHaul : ObjectSetting
{
    [SerializeField]
    private bool defaultValue;

    [SerializeField]
    private UnityEvent<bool> onValueChanged;
    public UnityEvent<bool> OnValueChanged => onValueChanged;

    [SerializeField]
    protected GameObject uiControlPrefab;

    [SerializeField]
    private string displayName;

    private bool currentValue;
    public bool CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = value;
            onValueChanged.Invoke(currentValue);
        }
    }

    private HaulDesignation haulDesignation;
    private TogglePresenter toggle;

    public override void InstantiateControl(Transform parent)
    {
        if (toggle != null)
        {
            toggle.OnClick.RemoveListener(HandleClick);
            onValueChanged.RemoveListener(toggle.SetValue);
        }
        GameObject go = Instantiate(uiControlPrefab, parent);
        toggle = go.GetComponent<TogglePresenter>();
        toggle.IsOn = CurrentValue;
        toggle.SetLabel(displayName);
        toggle.OnClick.AddListener(HandleClick);
        onValueChanged.AddListener(toggle.SetValue);
    }

    private void HandleClick(bool displayedValue)
    {
        CurrentValue = !CurrentValue;
    }

    private void HandleInventoryAdd(Item item, int quantity)
    {
        haulDesignation.DispatchJob();
    }

    private void HandleValueChanged(bool newValue)
    {
        if (newValue)
        {
            haulDesignation.Source.Inventory.OnAdd.AddListener(HandleInventoryAdd);
            haulDesignation.DispatchJob();
        }
        else
        {
            haulDesignation.Source.Inventory.OnAdd.RemoveListener(HandleInventoryAdd);
        }
    }

    private void Awake()
    {
        haulDesignation = GetComponent<HaulDesignation>();
        onValueChanged.AddListener(HandleValueChanged);
        CurrentValue = defaultValue;
    }
}
