using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private Rigidbody sphereRB;

    [SerializeField] private GameObject car;

    [SerializeField] private float aceleration = 8f;

    [SerializeField] private float maxSpeed = 50f;

    [SerializeField] private float turnForce = 180f;

    [SerializeField] private float holdTimeToBrake;

    [SerializeField] private float brakeDrag = 6f;

    [SerializeField] private float dragOnGroun;

    [SerializeField] private float dragOnAir;

    [SerializeField] private float jumpForce = 3;

    [SerializeField] private float gravityForce;

    [SerializeField] private Transform groundRaycastPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastSize = 5;


    public enum TouchState { Up, Click, Hold}
    public enum JumpState { Defalt, PreJump, Jump}

    private TouchState rigthState;
    private TouchState leftState;

    private JumpState jumpState;

    private float turnDirection;

    private float currentHoldTime;

    private bool grounded;

    private bool braking;

    private bool holdingToBrake;

    void Start()
    {
        jumpState = JumpState.Defalt;

        rigthState = TouchState.Up;
        leftState = TouchState.Up;

        grounded = false;

        holdingToBrake = false;

        braking = false;

        sphereRB.transform.parent = null;
    }

    void Update()
    {
        print(leftState);
        //Parar de virar
        if(rigthState == TouchState.Up && leftState == TouchState.Up)
        {
            turnDirection = 0;
        }

        //Freio
        if (holdingToBrake)
        {
            currentHoldTime -= Time.deltaTime;
            if (currentHoldTime <= 0)
            {
                braking = true;

                holdingToBrake = false;
            }
        }

        //Atualizar posição e rotação do carro
        if (grounded && braking==false)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnDirection * turnForce * Time.deltaTime, 0));
        }

        transform.position = sphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRaycastPos.position ,-transform.up, out hit, raycastSize, groundLayer))
        {
            grounded = true;

            car.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        }

        if (grounded)
        {
            sphereRB.drag = dragOnGroun;


            if (braking)
            {
                sphereRB.drag = brakeDrag;
            }
            else
            {
                sphereRB.AddForce(transform.forward * aceleration * 100);
            }

        }
        else
        {
            sphereRB.drag = dragOnAir;

            sphereRB.AddForce(-transform.up * gravityForce * 100);
        }
    }


    public void ChangeRigthState(int value)
    {
        switch (value)
        {
            case 0:     
                
                rigthState = TouchState.Up;

                //Não é preciso repetir as proximas 2 linhas em ambos os Change States, especialmente a corroutine.
                holdingToBrake = false;

                StartCoroutine(StopBrake());

                break;
            case 1:

                if (braking == false)
                {

                    if (jumpState == JumpState.Defalt)
                    {
                        jumpState = JumpState.PreJump;

                        StartCoroutine(SetJumpStateDefalt());
                    }
                    else if (jumpState == JumpState.PreJump)
                    {
                        StopCoroutine(SetJumpStateDefalt());

                        Jump();
                    }

                }

                

                break;
            case 2:
                rigthState = TouchState.Hold;

                turnDirection = 1;

                break;
        }

        Brake();
    }

    public void ChangeLeftState(int value)
    {
        switch (value)
        {
            case 0:

                leftState = TouchState.Up;

                break;
            case 1:

                
                if (braking == false)
                {

                    if (jumpState == JumpState.Defalt)
                    {
                        jumpState = JumpState.PreJump;

                        StartCoroutine(SetJumpStateDefalt());
                    }
                    else if (jumpState == JumpState.PreJump)
                    {
                        StopCoroutine(SetJumpStateDefalt());

                        Jump();
                    }

                }

       
                break;
            case 2:
                leftState = TouchState.Hold;

                turnDirection = -1;

                break;
        }


        Brake();
    }

    private void Brake()
    {
        //Freio
        if (rigthState == TouchState.Hold && leftState == TouchState.Hold)
        {
            currentHoldTime = holdTimeToBrake;

            holdingToBrake = true;
            
        }
    }

    private void Jump()
    {
        if (grounded)
        {
            sphereRB.AddForce(Vector3.up * jumpForce * 1000);
        }

        jumpState = JumpState.Defalt;
    }


    IEnumerator SetJumpStateDefalt()
    {

        yield return new WaitForSeconds(0.3f);

        jumpState = JumpState.Defalt;

        StopCoroutine(SetJumpStateDefalt());
    }

    IEnumerator StopBrake()
    {
        yield return new WaitForSeconds(0.1f);

        braking = false;

        StopCoroutine(StopBrake());
         
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(groundRaycastPos.position, new Vector3(groundRaycastPos.position.x, groundRaycastPos.position.y - raycastSize, groundRaycastPos.position.z));
    }
}
