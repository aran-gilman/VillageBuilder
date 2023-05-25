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
            Toggle.OnClick.AddListener(HandleClick);
        }

        public void SetJob(JobDesignation newJob)
        {
            Toggle.gameObject.SetActive(newJob != null);
            if (JobDesignation != null)
            {
                JobDesignation.OnAllJobsCompleted.RemoveListener(HandleJobCompleted);
            }
            JobDesignation = newJob;
            if (JobDesignation != null)
            {
                Toggle.SetLabel(JobDesignation.DisplayName);
                Toggle.IsOn = JobDesignation.HasActiveJob();
                JobDesignation.OnAllJobsCompleted.AddListener(HandleJobCompleted);
            }
        }

        private void HandleClick(bool currentValue)
        {
            if (currentValue)
            {
                List<Job> jobs = new List<Job>(JobDesignation.CurrentJobs);
                foreach (Job job in jobs)
                {
                    job.Cancel();
                }
                if (JobDesignation != null)
                {
                    Toggle.IsOn = JobDesignation.HasActiveJob();
                }
            }
            else
            {
                JobDesignation.DispatchJob();
                Toggle.IsOn = JobDesignation.HasActiveJob();
            }
        }

        private void HandleJobCompleted()
        {
            Toggle.IsOn = false;
            if (!JobDesignation.CanCreateJobs())
            {
                SetJob(null);
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
            if (gameObject.activeSelf)
            {
                UpdateJobDisplay();
            }
        }
    }

    private void UpdateJobDisplay()
    {
        if (JobSource == null)
        {
            gameObject.SetActive(false);
        }

        JobDesignation[] jobDesignations = JobSource.GetComponentsInChildren<JobDesignation>();
        int numJobsDisplayed = 0;
        for (int i = 0; i < jobDesignations.Length; i++)
        {
            if (jobDesignations[i].CanCreateJobs() || jobDesignations[i].HasActiveJob())
            {
                if (toggles.Count == i)
                {
                    TogglePresenter toggle = Instantiate(togglePrefab, transform).GetComponent<TogglePresenter>();
                    Assert.IsNotNull(toggle, "togglePrefab must have a TogglePresenter component!");
                    toggles.Add(new ToggleJobPair(toggle));
                }
                toggles[i].SetJob(jobDesignations[i]);
                numJobsDisplayed++;
            }
        }

        for (int i = numJobsDisplayed; i < toggles.Count; i++)
        {
            toggles[i].SetJob(null);
        }
    }

    private void OnEnable()
    {
        UpdateJobDisplay();
    }
}
