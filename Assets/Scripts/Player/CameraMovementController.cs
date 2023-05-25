using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField]
    private Vector2GameEvent cameraMoveEvent;

    [SerializeField]
    private FloatGameEvent rotateCameraEvent;

    [SerializeField]
    private FloatGameEvent pitchCameraEvent;

    [SerializeField]
    private float minPitch = 0.0f;

    [SerializeField]
    private float maxPitch = 90.0f;

    private Vector3 velocity;
    private float rotationSpeed;
    private float pitchSpeed;

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

    private void OnRotateCamera(object sender, float amount)
    {
        rotationSpeed = amount;
    }

    private void OnPitchCamera(object sender, float amount)
    {
        pitchSpeed = amount;
    }

    private void OnEnable()
    {
        cameraMoveEvent.OnGameEvent += HandleCameraMove;
        rotateCameraEvent.OnGameEvent += OnRotateCamera;
        pitchCameraEvent.OnGameEvent += OnPitchCamera;
    }

    private void OnDisable()
    {
        cameraMoveEvent.OnGameEvent -= HandleCameraMove;
        rotateCameraEvent.OnGameEvent -= OnRotateCamera;
        pitchCameraEvent.OnGameEvent -= OnPitchCamera;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        transform.Translate(GetAbsoluteVelocity() * Time.fixedDeltaTime, Space.World);
        transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime, Space.World);
        transform.Rotate(Vector3.right, pitchSpeed * Time.fixedDeltaTime, Space.Self);

        if (transform.rotation.eulerAngles.x > 180)
        {
            float targetPitch = 360 + minPitch;
            if (transform.rotation.eulerAngles.x < targetPitch)
            {
                transform.rotation = Quaternion.Euler(targetPitch, transform.rotation.eulerAngles.y, 0.0f);
            }
        }
        else if (transform.rotation.eulerAngles.x > maxPitch)
        {
            transform.rotation = Quaternion.Euler(maxPitch, transform.rotation.eulerAngles.y, 0.0f);
        }
    }
}
