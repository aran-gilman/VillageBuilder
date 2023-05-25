using UnityEngine;

public class CameraEye : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        transform.LookAt(target);
        mainCamera.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
