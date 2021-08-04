using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickEffect : MonoBehaviour
{
    [SerializeField] protected MovementInput moveInput;

    [SerializeField] protected GameObject joyStickBorder;
    [SerializeField] protected GameObject joyStick;

    private Transform joyStickTransform;

    protected Vector2 startPosition;

    private float limitsxRigth;

    private float limitsxLeft;

    private float limitsyRigth;

    private float limitsyLeft;

    private void Start()
    {
        joyStickTransform = joyStick.transform;

        float limitsValue = (joyStickBorder.GetComponent<RectTransform>().rect.width / 2) - (joyStick.GetComponent<RectTransform>().rect.width / 2);

        print(limitsValue);

        limitsxRigth = joyStickBorder.transform.position.x + limitsValue;

        limitsxLeft = joyStickBorder.transform.position.x - limitsValue;

        limitsyRigth = joyStickBorder.transform.position.y + limitsValue;

        limitsyLeft = joyStickBorder.transform.position.y - limitsValue;


        startPosition = joyStickBorder.transform.position;
    }

    private void Update()
    {
        if (moveInput.GetTouch() == false)
        {
            joyStickTransform.localPosition = Vector2.zero;

            return;
        }

        float x = Mathf.Clamp(moveInput.GetTouchPosition().x, limitsxLeft, limitsxRigth);

        float y = Mathf.Clamp(moveInput.GetTouchPosition().y, limitsyLeft, limitsyRigth);

        joyStickTransform.position = new Vector2(x, y);

        //Vector2 direction = moveInput.GetTouchPosition() - startPosition;
        //direction.Normalize();
        //print(direction);
        //joyStickTransform.localPosition = new Vector2(limits * direction.x, limits * direction.y);

    }
}
