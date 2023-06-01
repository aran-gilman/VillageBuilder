using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PropertyTable<TKey, TValue> : ScriptableObject, ISerializationCallbackReceiver
{
    [Serializable]
    private class Entry
    {
        public TKey Key;
        public TValue Value;
    }
    [SerializeField]
    private List<Entry> serializedEntries;

    protected Dictionary<TKey, TValue> entries = new Dictionary<TKey, TValue>();
    public IReadOnlyDictionary<TKey, TValue> Entries => entries;

    public TValue Get(TKey key)
    {
        if (entries.TryGetValue(key, out TValue value))
        {
            return value;
        }
        return default;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        entries = new Dictionary<TKey, TValue>(serializedEntries.Count);
        foreach (Entry entry in serializedEntries)
        {
            if (entry != null && entry.Key != null)
            {
                entries.TryAdd(entry.Key, entry.Value);
            }
        }
    }
}
