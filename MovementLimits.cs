using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLimits : MonoBehaviour
{
    [SerializeField] private Transform limitXReference;

    [SerializeField] private Transform limitZReference;

    private float limitX;
    private float limitZ;

    private void Awake()
    {
        limitX = limitXReference.position.x;

        limitZ = limitZReference.position.z;

    }

    public void CalculateObjectMovement(Vector3 movementVector)
    {

        Vector3 nextPosition = transform.position + movementVector;

        print(nextPosition);

        float movementX = Mathf.Clamp(nextPosition.x, -limitX, limitX);

        print("X :"+movementX);

        float movementZ = Mathf.Clamp(nextPosition.z, -limitZ, limitZ);

        print("Z ;"+movementZ);

        Vector3 filtredPosition = new Vector3(movementX, nextPosition.y, movementZ);

        transform.position = filtredPosition;


    }


    private void OnDrawGizmos()
    {
        if (limitZReference != null)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(new Vector3(-limitXReference.position.x, limitXReference.position.y, limitXReference.position.z), 1f);


            Gizmos.color = Color.red;
            
            Gizmos.DrawSphere(new Vector3(limitZReference.position.x, limitZReference.position.y, -limitZReference.position.z), 1f);
        }
    }
}
