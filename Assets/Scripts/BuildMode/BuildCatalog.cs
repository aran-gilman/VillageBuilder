using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Build Mode/Catalog")]
public class BuildCatalog : ScriptableObject, ISerializationCallbackReceiver
{
    [Serializable]
    private class CatalogEntry
    {
        public Blueprint Blueprint;
        public BuildCategory Category;
        public bool IsUnlocked = true;
    }

    [SerializeField]
    private List<CatalogEntry> entries;

    private class CategoryInfo
    {
        private List<CatalogEntry> entries = new List<CatalogEntry>();
        public IReadOnlyList<CatalogEntry> Entries => entries;

        public bool IsUnlocked { get; private set; } = false;

        public void AddEntry(CatalogEntry entry)
        {
            entries.Add(entry);
            IsUnlocked = IsUnlocked || entry.IsUnlocked;
        }

        public void UpdateEntry(CatalogEntry entry)
        {
            if (entry.IsUnlocked)
            {
                IsUnlocked = true;
            }
            else
            {
                IsUnlocked = false;
                foreach (var e in entries)
                {
                    IsUnlocked = IsUnlocked || e.IsUnlocked;
                }
            }
        }
    }
    private Dictionary<BuildCategory, CategoryInfo> entriesByCategory = new Dictionary<BuildCategory, CategoryInfo>();
    private Dictionary<Blueprint, CatalogEntry> entriesByBlueprint = new Dictionary<Blueprint, CatalogEntry>();

    public IEnumerable<BuildCategory> Categories => entriesByCategory.Keys;

    public IEnumerable<Blueprint> GetBlueprintsInCategory(BuildCategory category)
    {
        return entriesByCategory[category].Entries.Select(e => e.Blueprint);
    }

    public bool IsUnlocked(Blueprint blueprint)
    {
        return entriesByBlueprint[blueprint].IsUnlocked;
    }

    public bool IsUnlocked(BuildCategory category)
    {
        return entriesByCategory[category].IsUnlocked;
    }

    public void Unlock(Blueprint blueprint)
    {
        CatalogEntry entry = entriesByBlueprint[blueprint];
        entry.IsUnlocked = true;
        entriesByCategory[entry.Category].UpdateEntry(entry);
    }

    private void UpdateCachedInfo()
    {
        entriesByCategory.Clear();
        entriesByBlueprint.Clear();
        foreach (CatalogEntry entry in entries)
        {
            entriesByCategory.TryAdd(entry.Category, new CategoryInfo());
            entriesByCategory[entry.Category].AddEntry(entry);
            if(!entriesByBlueprint.TryAdd(entry.Blueprint, entry))
            {
                Debug.LogError($"Build catalog {name} containes multiple entries for blueprint {entry.Blueprint.name}");
            }
        }
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        UpdateCachedInfo();
    }
}
