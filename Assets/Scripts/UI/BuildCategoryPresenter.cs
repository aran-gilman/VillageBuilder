using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BuildCategoryPresenter : MonoBehaviour
{
    [SerializeField]
    private BuildCatalog buildCatalog;
    public BuildCatalog BuildCatalog => buildCatalog;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private UnityEvent<BuildCategory> onSelectCategory;
    public UnityEvent<BuildCategory> OnSelectCategory => onSelectCategory;

    private List<ToggleCategoryPair> categoryToggles = new List<ToggleCategoryPair>();

    private class ToggleCategoryPair
    {
        public TogglePresenter Toggle { get; private set; }
        public BuildCategoryPresenter Owner { get; private set; }

        private BuildCategory category;
        public BuildCategory Category
        {
            get => category;
            set
            {
                category = value;
                UpdateDisplayedInfo();
            }
        }

        public ToggleCategoryPair(TogglePresenter toggle, BuildCategoryPresenter owner)
        {
            Toggle = toggle;
            Toggle.OnClick.AddListener(HandleClick);
            Owner = owner;
        }

        private void HandleClick(bool currentValue)
        {
            if (currentValue)
            {
                Toggle.IsOn = false;
                Owner.OnSelectCategory.Invoke(null);
            }
            else
            {
                Toggle.IsOn = true;
                Owner.OnSelectCategory.Invoke(Category);
            }
        }

        private void UpdateDisplayedInfo()
        {
            if (category == null)
            {
                Toggle.gameObject.SetActive(false);
            }
            else
            {
                Toggle.SetLabel(category.DisplayName);
                Toggle.Interactable = Owner.BuildCatalog.IsUnlocked(category);
                Toggle.gameObject.SetActive(true);
            }
        }
    }

    private void UpdateDisplayedCategories()
    {
        var categories = buildCatalog.Categories;
        int numDisplayed = categories.Count();
        for (int i = 0; i < numDisplayed; i++)
        {
            ToggleCategoryPair toggle;
            if (i >= categoryToggles.Count)
            {
                var t = Instantiate(buttonPrefab, transform).GetComponent<TogglePresenter>();
                toggle = new ToggleCategoryPair(t, this);
                categoryToggles.Add(toggle);
            }
            else
            {
                toggle = categoryToggles[i];
            }
            toggle.Category = categories.ElementAt(i);
        }

        for (int i = numDisplayed; i < categoryToggles.Count; i++)
        {
            categoryToggles[i].Category = null;
        }
    }

    private void OnEnable()
    {
        UpdateDisplayedCategories();
    }
}
