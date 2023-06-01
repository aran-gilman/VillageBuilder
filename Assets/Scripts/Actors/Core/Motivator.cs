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
    public class MotiveInfo
    {
        public Motive Motive;
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
    private List<MotiveInfo> motives;

    private void HandleSetEvent(object sender, MotiveChangeEvent.Args args)
    {
        if (args.Actor != this)
        {
            return;
        }
        MotiveInfo motive = motives.Find(m => m.Motive == args.Motive);
        if (motive != null)
        {
            motive.CurrentValue = args.Amount;
        }
    }

    private void HandleChangeEvent(object sender, MotiveChangeEvent.Args args)
    {
        if (args.Actor != this)
        {
            return;
        }
        MotiveInfo motive = motives.Find(m => m.Motive == args.Motive);
        if (motive != null)
        {
            motive.CurrentValue += args.Amount;
        }
    }

    private void OnEnable()
    {
        foreach (MotiveInfo motive in motives)
        {
            motive.Motive.ChangeEvent.OnGameEvent += HandleChangeEvent;
            motive.Motive.SetEvent.OnGameEvent += HandleSetEvent;
        }
    }

    private void OnDisable()
    {
        foreach (MotiveInfo motive in motives)
        {
            motive.Motive.ChangeEvent.OnGameEvent -= HandleChangeEvent;
            motive.Motive.SetEvent.OnGameEvent -= HandleSetEvent;
        }
    }

    private void FixedUpdate()
    {
        foreach (MotiveInfo motive in motives)
        {
            motive.CurrentValue = Mathf.Clamp(motive.CurrentValue + motive.ChangePerSecond * Time.fixedDeltaTime, 0.0f, 100.0f);
        }
    }
}
