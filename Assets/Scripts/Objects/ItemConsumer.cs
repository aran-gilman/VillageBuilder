using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemConsumer : ScriptableObject
{
    [Serializable]
    public class MotiveProperty
    {
        public Motive Motive;
        public ItemIntProperty Property;
    }

    [SerializeField]
    private List<MotiveProperty> motivePropertyMap;

    public ItemIntProperty GetProperty(Motive motive)
    {
        MotiveProperty kv = motivePropertyMap.Find(kv => kv.Motive == motive);
        if (kv != null)
        {
            return kv.Property;
        }
        return null;
    }

    public void ConsumeItem(Item item, Motivator target)
    {
        foreach (MotiveProperty kv in motivePropertyMap)
        {
            int value = kv.Property.Get(item);
            if (value != 0)
            {
                target.ChangeMotiveValue(kv.Motive, value);
            }
        }
    }
}
