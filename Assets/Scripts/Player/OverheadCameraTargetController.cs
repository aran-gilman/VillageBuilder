using UnityEngine;

public class OverheadCameraTargetController : MonoBehaviour
{
    [SerializeField]
    private Vector2GameEvent cameraMoveEvent;

    private Vector3 velocity;

    private Camera mainCamera;

    private void HandleCameraMove(object sender, Vector2 delta)
    {
        Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up);
        Vector3 right = Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up);
        velocity = forward * delta.y + right * delta.x;
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
        transform.Translate(velocity * Time.fixedDeltaTime);
    }
}
