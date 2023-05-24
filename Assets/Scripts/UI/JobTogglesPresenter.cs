using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class JobTogglesPresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject togglePrefab;

    private class ToggleJobPair
    {
        public TogglePresenter Toggle { get; private set; }
        public JobDesignation JobDesignation { get; private set; }

        public ToggleJobPair(TogglePresenter toggle)
        {
            Toggle = toggle;
            Toggle.OnTrue.AddListener(TryCreateJob);
        }

        public void SetJob(JobDesignation newJob)
        {
            Toggle.gameObject.SetActive(newJob != null);
            if (newJob != null)
            {
                Toggle.SetLabel(newJob.DisplayName);
            }
        }

        private void TryCreateJob()
        {
            if (JobDesignation != null)
            {
                Toggle.IsOn = JobDesignation.TryCreateJob();
            }
        }
    }
    private List<ToggleJobPair> toggles = new List<ToggleJobPair>();

    private GameObject jobSource;
    public GameObject JobSource
    {
        get => jobSource;
        set
        {
            jobSource = value;
            UpdateJobDisplay();
        }
    }

    private void UpdateJobDisplay()
    {
        if (JobSource == null)
        {
            gameObject.SetActive(false);
        }

        JobDesignation[] jobDesignations = JobSource.GetComponentsInChildren<JobDesignation>();
        for (int i = 0; i < jobDesignations.Length; i++)
        {
            if (jobDesignations[i].CanCreateJob())
            {
                if (toggles.Count == i)
                {
                    TogglePresenter toggle = Instantiate(togglePrefab, transform).GetComponent<TogglePresenter>();
                    Assert.IsNotNull(toggle, "togglePrefab must have a TogglePresenter component!");
                    toggles.Add(new ToggleJobPair(toggle));
                }
                toggles[i].SetJob(jobDesignations[i]);
            }
        }

        for (int i = jobDesignations.Length; i < toggles.Count; i++)
        {
            toggles[i].SetJob(null);
        }
    }
}
