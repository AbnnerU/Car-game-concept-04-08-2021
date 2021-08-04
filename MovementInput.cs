using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MovementInput : EventTrigger
{
    private bool touch = false;

    private Vector2 touchPosition;

    public override void OnPointerDown(PointerEventData eventData)
    {
        touch = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        touch = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        touchPosition = eventData.position;
    }


    public bool GetTouch()
    {
        return touch;
    }

    public Vector2 GetTouchPosition()
    {
        return touchPosition;
    }

}
