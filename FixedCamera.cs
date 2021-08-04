using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraParent;

    [SerializeField] private Transform fixedModePosition;

    [SerializeField] private float startRotationX;

    [SerializeField] private float startRotationY;

    public void CameraMode()
    {
        //X
        transform.localEulerAngles = new Vector3(startRotationX,0,0);

        //Y
        cameraParent.localEulerAngles= new Vector3(0, startRotationY, 0);

        //Position
        cameraParent.transform.position = fixedModePosition.position;

    }

}
