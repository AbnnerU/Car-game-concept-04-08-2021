
using UnityEngine;

public class CameraModeController : MonoBehaviour
{
    [SerializeField] private FreeCamera freeCamera;

    [SerializeField] private FixedCamera fixedCamera;

    [SerializeField] private GameObject movementJoyStick;

    private enum CameraMode { Free, Fixed }

    private CameraMode currentCameraMode;

    private void Start()
    {
        currentCameraMode = CameraMode.Free;

        freeCamera.CameraMode();
    }

    public void ChangeCameraMode()
    {
        if (currentCameraMode == CameraMode.Free)
        {
            currentCameraMode = CameraMode.Fixed;

           fixedCamera.CameraMode();

            movementJoyStick.SetActive(false);
        }
        else
        {
            currentCameraMode = CameraMode.Free;

            freeCamera.CameraMode();

            movementJoyStick.SetActive(true);
        }
    }

}
