using UnityEngine;

public class OverheadCameraTargetController : MonoBehaviour
{
    [SerializeField]
    private Vector2GameEvent cameraMoveEvent;

    private Vector3 velocity;

    private Camera mainCamera;

    private Vector3 GetAbsoluteVelocity()
    {
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up).normalized;
        return forward * velocity.y + right * velocity.x;
    }

    private void HandleCameraMove(object sender, Vector2 delta)
    {
        velocity = delta;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
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
        transform.Translate(GetAbsoluteVelocity() * Time.fixedDeltaTime);
    }
}
