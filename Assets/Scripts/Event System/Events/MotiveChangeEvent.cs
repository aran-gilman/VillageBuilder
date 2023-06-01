using UnityEngine;

[CreateAssetMenu(menuName = "Game Events/Motive Change")]
public class MotiveChangeEvent : GameEvent<MotiveChangeEvent.Args>
{
    public class Args
    {
        public Motivator Actor;
        public Motive Motive;
        public float Amount;
    }
}
