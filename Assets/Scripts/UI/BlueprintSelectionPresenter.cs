using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlueprintSelectionPresenter : MonoBehaviour
{
    [SerializeField]
    private BuildCatalog buildCatalog;

    [SerializeField]
    private GameObject buttonPrefab;

    private BuildCategory currentCategory;
    public BuildCategory CurrentCategory
    {
        get => currentCategory;
        set
        {
            currentCategory = value;
            gameObject.SetActive(currentCategory != null);
        }
    }

    private class BlueprintPresenter
    {
        public TogglePresenter Toggle { get; private set; }

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

        public BlueprintPresenter(TogglePresenter toggle)
        {
            Toggle = toggle;
            Toggle.OnClick.AddListener(HandleClick);
        }

        private void HandleClick(bool currentValue)
        {
            Debug.Log($"Selected blueprint: {blueprint.name}");
        }

        private void UpdateDisplayedInfo()
        {
            if (blueprint == null)
            {
                Toggle.gameObject.SetActive(false);
            }
            else
            {
                Toggle.SetLabel(blueprint.name);
                Toggle.gameObject.SetActive(true);
            }
        }
    }

    private List<BlueprintPresenter> children = new List<BlueprintPresenter>();

    private void UpdateButtons()
    {
        if (CurrentCategory == null)
        {
            foreach (var child in children)
            {
                child.Blueprint = null;
            }
        }
        else
        {
            IEnumerable<Blueprint> blueprints = buildCatalog.GetBlueprintsInCategory(CurrentCategory).Where(b => buildCatalog.IsUnlocked(b));
            int numDisplayed = blueprints.Count();
            for (int i = 0; i < numDisplayed; i++)
            {
                BlueprintPresenter child;
                if (i >= children.Count)
                {
                    child = new BlueprintPresenter(Instantiate(buttonPrefab, transform).GetComponent<TogglePresenter>());
                    children.Add(child);
                }
                else
                {
                    child = children[i];
                }
                child.Blueprint = blueprints.ElementAt(i);
            }

            for (int i = numDisplayed; i < children.Count; i++)
            {
                children[i].Blueprint = null;
            }
        }
    }

    private void OnEnable()
    {
        UpdateButtons();
    }
}
