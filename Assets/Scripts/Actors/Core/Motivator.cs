using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Motivator : MonoBehaviour
{
    [Serializable]
    public class Threshold
    {
        public float MinValue;
        public float MaxValue;

        public UnityEvent FromBelow;
        public UnityEvent FromAbove;
    }

    [Serializable]
    public class Motive
    {
        public string Name; // TODO: Probably replace with SO
        public float ChangePerSecond; // TODO: Probably move this somewhere else
        public List<Threshold> Thresholds;

        [SerializeField]
        private float currentValue;
        public float CurrentValue
        {
            get => currentValue;
            set
            {
                float oldValue = currentValue;
                currentValue = value;
                foreach (Threshold threshold in Thresholds)
                {
                    if (currentValue > threshold.MinValue && currentValue < threshold.MaxValue)
                    {
                        if (oldValue > threshold.MaxValue)
                        {
                            threshold.FromAbove.Invoke();
                        }
                        else if (oldValue < threshold.MinValue)
                        {
                            threshold.FromBelow.Invoke();
                        }
                    }
                }
            }
        }
    }

    [SerializeField]
    private List<Motive> motives;

    private void FixedUpdate()
    {
        foreach (Motive motive in motives)
        {
            motive.CurrentValue = Mathf.Clamp(motive.CurrentValue + motive.ChangePerSecond * Time.fixedDeltaTime, 0.0f, 100.0f);
        }
    }
}
