using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManeger : MonoBehaviour
{
    [SerializeField] private Targets targets;

    [SerializeField] private FinalPoint finalPoint;

    [SerializeField] private GameObject startPoint;

    [SerializeField] private Vector3 startRotation;

    [SerializeField] private GameObject playerCar;

    private CarMovement carMovementScript;

    private Rigidbody carSphere;

    private Vector3 firtPosition;

    private Quaternion firtRotation;

    private void Awake()
    {
        carMovementScript = playerCar.GetComponent<CarMovement>();

        carSphere = carMovementScript.GetSphere();

        //Events
        targets.OnLastTarget += OnLastTargetEvent;

        targets.OnChangeTarget += OnChangeTargetEvent;

        finalPoint.OnFinishLine += OnFinishLineEvent;

        firtPosition = startPoint.transform.position;
        firtRotation = Quaternion.Euler(startRotation);

        carSphere.transform.position = firtPosition;

        playerCar.transform.rotation = firtRotation;
        
    }

    #region Events
    private void OnChangeTargetEvent(GameObject newTarget)
    {
        carMovementScript.SetTarget(newTarget);
    }

    public void OnLastTargetEvent()
    {
        carMovementScript.SetTarget(targets.GetFinishPoint());
        
    }

    public void OnFinishLineEvent()
    {
        carMovementScript.ChangeCanMoveValue(false);
    }

    #endregion

    public void StartCarMovement()
    {
        ConfigCarSphere(false);

        carMovementScript.SetTarget(targets.GetFirstTarget());

        carMovementScript.ChangeCanMoveValue(true);
    }

    public void ResetCar()
    {
        //Target script
        targets.ResetCurrentTargetId();


        //Car
        ConfigCarSphere(true);

        carMovementScript.ChangeCanMoveValue(false);
    
        carSphere.position = firtPosition;

        playerCar.transform.rotation = firtRotation;

    }

    public GameObject GetPlayerCar()
    {
        return playerCar;
    }

    private void ConfigCarSphere(bool setKinematic)
    {
        carSphere.isKinematic = setKinematic;

        carSphere.velocity = Vector3.zero;
    }
}
