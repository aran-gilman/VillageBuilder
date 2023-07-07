using UnityEngine;

public class VillagerJoiner : ObjectSetting
{
    [SerializeField]
    private GameObject uiControlPrefab;

    [SerializeField]
    private IntGameEvent spawnVillagers;

    public int AvailableVillagers
    {
        get => availableVillagers;
        set
        {
            bool shouldRefresh = availableVillagers != value;
            availableVillagers = value;
            if (shouldRefresh)
            {
                refreshSettings.Raise();
            }
        }
    }

    private TogglePresenter button;
    private int availableVillagers;

    public override bool IsEnabled() => availableVillagers > 0;

    public override void InstantiateControl(Transform parent)
    {
        if (button != null)
        {
            button.OnClick.RemoveListener(HandleClick);
        }
        button = Instantiate(uiControlPrefab, parent).GetComponent<TogglePresenter>();
        button.SetLabel($"Accept {AvailableVillagers} new villagers");
        button.OnClick.AddListener(HandleClick);
    }

    private void HandleClick(bool currentValue)
    {
        spawnVillagers.Raise(AvailableVillagers);
        AvailableVillagers = 0;
    }
}
