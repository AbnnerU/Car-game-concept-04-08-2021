using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CameraMovement : MonoBehaviour
{
    [SerializeField] protected MovementInput moveInput;

    [SerializeField] protected GameObject cameraParent;

    [SerializeField] protected GameObject joyStickBorder;
    [SerializeField] protected GameObject joyStick;


    [SerializeField] protected float movementSpeed;

    //protected Touch touch;

    protected Transform joyStickTransform;

    protected Vector3 currentVectorValue;

    protected Vector2 startPosition;

    protected float limits;

    //protected int touchFinger;

    protected bool moving;

    protected virtual void Start()
    {
        //limits = (joyStickBorder.GetComponent<RectTransform>().rect.width / 2) - (joyStick.GetComponent<RectTransform>().rect.width / 2);
        //print(limits);
        joyStickTransform = joyStick.GetComponent<Transform>();

        startPosition = joyStickBorder.transform.position;

        //touchFinger = -1;
    }

    protected abstract void Update();

    protected virtual Vector2 CalculateDirection()
    {

        //joyStick.transform.localPosition =new Vector2(Mathf.Clamp(estee.GetTouchPosition().x, -limits, limits), Mathf.Clamp(estee.GetTouchPosition().y, -limits, limits));
        Vector2 direction = (Vector2)joyStickTransform.position - startPosition;
        direction.Normalize();

        //print(direction * movementSpeed);

        return direction;
    }
    
}
