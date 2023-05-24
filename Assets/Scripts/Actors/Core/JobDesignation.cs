using UnityEngine;

public abstract class JobDesignation : MonoBehaviour
{
    [SerializeField]
    protected JobDispatcher jobDispatcher;

    [SerializeField]
    protected string displayName;
    public string DisplayName => displayName;

    public abstract bool CanCreateJob();
    public abstract bool TryCreateJob();
}
