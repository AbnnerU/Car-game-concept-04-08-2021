using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    //[SerializeField] private Targets targetsScript;
  
    [SerializeField] private Rigidbody sphereRB;

    [SerializeField] private GameObject car;

    [SerializeField] private float aceleration = 8f;

    //[SerializeField] private float maxSpeed = 50f;

    [SerializeField] private float turnForce = 180f;

    [SerializeField] private float dragOnGroun;

    [SerializeField] private float dragOnAir;

    //[SerializeField] private float jumpForce = 3;

    [SerializeField] private float gravityForce;

    [SerializeField] private Transform groundRaycastPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastSize = 5;

    /* [SerializeField] */
    private GameObject currentTarget;

    private bool grounded;

    private bool canMove=false;

    private void OnEnable ()
    {
        canMove = false;

        sphereRB.transform.parent = null;
        //sphereRB.AddForce(Vector3.forward * 50, ForceMode.Impulse);       
    }

    private void Update()
    {
        transform.position = sphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            grounded = false;

            RaycastHit hit;

            if (Physics.Raycast(groundRaycastPos.position, -transform.up, out hit, raycastSize, groundLayer))
            {
                grounded = true;

                car.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }

            if (grounded)
            {
                sphereRB.drag = dragOnGroun;

                Quaternion direction = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
                direction.x = 0;
                direction.z = 0;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, turnForce * Time.deltaTime);

                sphereRB.AddForce(transform.forward * aceleration * 10);


            }
            else
            {
                sphereRB.drag = dragOnAir;

            }
        }
        else
        {
            grounded = false;

            RaycastHit hit;

            if (Physics.Raycast(groundRaycastPos.position, -transform.up, out hit, raycastSize, groundLayer))
            {
                grounded = true;
                sphereRB.drag = dragOnGroun;

                car.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
            else
            {
                sphereRB.drag = dragOnAir;
            }
        }
    }

    public void ChangeCanMoveValue(bool value)
    {
        canMove = value;
    }

    public void SetTarget(GameObject target)
    {
        currentTarget = target;
    }


    public void SetAcelaration(float value)
    {
        aceleration = value;
    }

    public Rigidbody GetSphere()
    {
        return sphereRB;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(groundRaycastPos.position, new Vector3(groundRaycastPos.position.x, groundRaycastPos.position.y - raycastSize, groundRaycastPos.position.z));
    }

}
