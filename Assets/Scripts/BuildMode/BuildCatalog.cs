using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Build Mode/Catalog")]
public class BuildCatalog : ScriptableObject
{
    [Serializable]
    public class CatalogEntry
    {
        public Blueprint Blueprint;
        public BuildCategory Category;
        public bool IsUnlocked = true;
    }

    [SerializeField]
    private CatalogEntry[] entries;
    public CatalogEntry[] Entries => entries;
}
