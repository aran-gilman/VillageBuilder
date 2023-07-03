using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildCategoryPresenter : MonoBehaviour
{
    [SerializeField]
    private BuildCatalog buildCatalog;

    [SerializeField]
    private GameObject buttonPrefab;

    private List<TogglePresenter> categoryToggles = new List<TogglePresenter>();

    private void UpdateDisplayedCategories()
    {
        var categories = buildCatalog.Categories;
        int numDisplayed = categories.Count();
        for (int i = 0; i < numDisplayed; i++)
        {
            TogglePresenter toggle;
            if (i >= categoryToggles.Count)
            {
                toggle = Instantiate(buttonPrefab, transform).GetComponent<TogglePresenter>();
                categoryToggles.Add(toggle);
            }
            else
            {
                toggle = categoryToggles[i];
                toggle.gameObject.SetActive(true);
            }
            var category = categories.ElementAt(i);
            toggle.SetLabel(category.DisplayName);
            toggle.Interactable = buildCatalog.IsUnlocked(category);
        }

        for (int i = numDisplayed; i < categoryToggles.Count; i++)
        {
            categoryToggles[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        UpdateDisplayedCategories();
    }
}
