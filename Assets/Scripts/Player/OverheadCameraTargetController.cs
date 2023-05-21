using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCameraTargetController : MonoBehaviour
{
    [SerializeField]
    private Vector2GameEvent cameraMoveEvent;

    private Vector3 velocity;

    private void HandleCameraMove(object sender, Vector2 delta)
    {
        velocity = new Vector3(delta.x, 0, delta.y);
    }

    private void OnEnable()
    {
        cameraMoveEvent.OnGameEvent += HandleCameraMove;
    }

    private void OnDisable()
    {
        cameraMoveEvent.OnGameEvent -= HandleCameraMove;
    }

    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);
    }
}
