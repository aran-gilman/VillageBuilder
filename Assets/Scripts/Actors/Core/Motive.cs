using UnityEngine;

[CreateAssetMenu]
public class Motive : ScriptableObject
{
    [SerializeField]
    private MotiveChangeEvent changeEvent;
    public MotiveChangeEvent ChangeEvent => changeEvent;

    [SerializeField]
    private MotiveChangeEvent setEvent;
    public MotiveChangeEvent SetEvent => setEvent;
}
