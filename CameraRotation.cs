using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraRotation : CameraMovement
{
    //[SerializeField] private Text teste;
    [SerializeField] private float maxRotationOnX;

    private Vector2 finalPosition;

    private float rotationX;

    private int followTouch = 0;

    protected override void Update()
    {
        //JoyStick version

        if (moveInput.GetTouch() == false)
        {
            return;
        }

        Vector2 direction = CalculateDirection();

        //print(direction);
        currentVectorValue = new Vector3(movementSpeed * direction.x, movementSpeed * direction.y, 0);

        //X
        rotationX -= currentVectorValue.y;

        rotationX = Mathf.Clamp(rotationX, 0, maxRotationOnX);

        transform.localEulerAngles = new Vector3(rotationX, 0, 0);

        //Y
        cameraParent.transform.Rotate(Vector3.up * currentVectorValue.x);

        #region Input version
        //teste.text = Input.touchCount+"";

        ////Get Touch
        //if (Input.touchCount > 0)
        //{ 
        //    Touch touch = Input.GetTouch(0);

        //        switch (touch.phase)
        //        {
        //            case TouchPhase.Began:
        //                startPosition = touch.position;

        //                break;

        //            case TouchPhase.Moved:
        //                moving = true;
        //                finalPosition = touch.position;

        //                Vector2 direction = CalculateDirection();

        //                currentVectorValue = new Vector3(movementSpeed * direction.x, movementSpeed * direction.y, 0);



        //                break;

        //            case TouchPhase.Stationary:
        //                moving = false;

        //                break;

        //            case TouchPhase.Ended:
        //                moving = false;
        //                followTouch = -1;

        //                break;
        //        }

        //}



        ////Move

        ////X
        //if (moving)
        //{
        //    rotationX -= currentVectorValue.y;

        //    rotationX = Mathf.Clamp(rotationX, 0, maxRotationOnX);

        //    transform.localEulerAngles = new Vector3(rotationX, 0, 0);

        //    //Y
        //    cameraParent.transform.Rotate(Vector3.up * currentVectorValue.x);
        //}

        #endregion

    }

    //protected override Vector2 CalculateDirection()
    //{
    //    Vector2 direction = finalPosition - startPosition;
    //    direction.Normalize();

    //    return direction;
    //}

    //protected override void Update()
    //{


    //    if (moving)
    //    {
    //        //X 
    //        rotationX -= currentVectorValue.y;

    //        rotationX = Mathf.Clamp(rotationX, -maxRotationOnX, maxRotationOnX);

    //        transform.localEulerAngles = new Vector3(rotationX, 0, 0);

    //        //Y
    //        cameraParent.transform.Rotate(Vector3.up * currentVectorValue.x);
    //    }
    //}

    //public override void MoveCamera()
    //{
    //    moving = true;

    //    Vector2 direction = CalculateDirection();

    //    joyStick.transform.localPosition = new Vector3(limits * direction.x, limits * direction.y, 0);

    //    currentVectorValue = new Vector3(movementSpeed * direction.x, movementSpeed * direction.y,0);
    //    print(currentVectorValue);
    //}

    //public void StartMovePosition()
    //{
    //    touch = Input.GetTouch(0);

    //    startPosition = touch.position;
    //}

    //public override void StopMove()
    //{
    //    moving = false;

    //    joyStick.transform.localPosition = Vector3.zero;
    //}

    //public override void StartMove()
    //{
    //    throw new System.NotImplementedException();
    //}

}
