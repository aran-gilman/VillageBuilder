using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class JobTogglesPresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject togglePrefab;

    [SerializeField]
    private GameObjectGameEvent destroyEvent;

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
            if (JobDesignation != null && JobDesignation.CurrentJob != null)
            {
                JobDesignation.CurrentJob.OnJobCompleted -= HandleJobCompleted;
            }
            JobDesignation = newJob;
            if (newJob != null)
            {
                Toggle.SetLabel(newJob.DisplayName);
                Toggle.IsOn = JobDesignation.CurrentJob != null;
                if (JobDesignation.CurrentJob != null)
                {
                    JobDesignation.CurrentJob.OnJobCompleted += HandleJobCompleted;
                }
            }
        }

        private void HandleClick(bool currentValue)
        {
            if (!currentValue && JobDesignation != null)
            {
                JobDesignation.DispatchJob();
                if (JobDesignation.CurrentJob != null)
                {
                    JobDesignation.CurrentJob.OnJobCompleted += HandleJobCompleted;
                    Toggle.IsOn = true;
                }
                else
                {
                    Toggle.IsOn = false;
                }
            }
        }

        private void HandleJobCompleted(object sender, object args)
        {
            ((Job)sender).OnJobCompleted -= HandleJobCompleted;
            Toggle.IsOn = false;
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

    private void OnObjectDestroyed(object sender, GameObject destroyedObject)
    {
        if (destroyedObject == JobSource)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        destroyEvent.OnGameEvent += OnObjectDestroyed;
    }

    private void OnDisable()
    {
        destroyEvent.OnGameEvent -= OnObjectDestroyed;
    }
}
