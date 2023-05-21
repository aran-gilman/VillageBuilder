using Cinemachine;
using UnityEngine;

public class CameraInputProvider : MonoBehaviour, AxisState.IInputAxisProvider
{
    [SerializeField]
    private FloatGameEvent rotateCameraEvent;

    [SerializeField]
    private FloatGameEvent pitchCameraEvent;

    private float xAxisValue = 0.0f;
    private float yAxisValue = 0.0f;

    /// <summary>
    /// Retrieves the current value associated with given axis.
    /// </summary>
    /// <param name="axis">The axis being retrieved. X=0, Y=1, Z=2</param>
    public float GetAxisValue(int axis)
    {
        return axis switch
        {
            0 => xAxisValue,
            1 => yAxisValue,
            _ => 0,
        };
    }

    private void OnRotateCamera(object sender, float amount)
    {
        xAxisValue = amount;
    }

    private void OnPitchCamera(object sender, float amount)
    {
        yAxisValue = amount;
    }

    private void OnEnable()
    {
        rotateCameraEvent.OnGameEvent += OnRotateCamera;
        pitchCameraEvent.OnGameEvent += OnPitchCamera;
    }

    private void OnDisable()
    {
        rotateCameraEvent.OnGameEvent -= OnRotateCamera;
        pitchCameraEvent.OnGameEvent -= OnPitchCamera;
    }
}
