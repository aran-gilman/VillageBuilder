using UnityEngine;

public class GameModeSetter : MonoBehaviour
{
    [SerializeField]
    private GameMode gameMode;

    private void OnEnable()
    {
        GameMode.Current = gameMode;
    }
}
