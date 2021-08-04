using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraXZAxis : CameraMovement
{
    [SerializeField] private MovementLimits movementLimits;

    protected override void Update()
    {
        if (moveInput.GetTouch() == false)
        {
            moving = false;
            return;
        }

        moving = true;

        Vector2 direction = CalculateDirection();

        currentVectorValue = ((movementSpeed * direction.x )* cameraParent.transform.right) + ((movementSpeed * direction.y) * cameraParent.transform.forward);

        movementLimits.CalculateObjectMovement(currentVectorValue * Time.deltaTime);
        //cameraParent.transform.position += currentVectorValue * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (moving)
        {
            movementLimits.CalculateObjectMovement(currentVectorValue * Time.deltaTime);
        }
    }


}
