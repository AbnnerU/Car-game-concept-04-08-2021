using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    [SerializeField] private GameObject finishPoint;

    [SerializeField]private List<GameObject> targetsObjects;

    private int currentTagetId;

    public Action<GameObject> OnChangeTarget;

    public Action OnLastTarget;

    public Action OnGameRestarted;

    public void Start()
    {
        currentTagetId = 0;
    }

    public void ChangeCurrentTarget()
    {
        targetsObjects[currentTagetId].GetComponent<PointCollision>().ActiveCollider(false);

        currentTagetId++;

        if (currentTagetId < targetsObjects.Count)
        {
           

            targetsObjects[currentTagetId].GetComponent<PointCollision>().ActiveCollider(true);

            OnChangeTarget?.Invoke(targetsObjects[currentTagetId]);
        }
        else
        {
            OnLastTarget?.Invoke();
        }
    }

    public void AddNewTarget(GameObject obj)
    {
        //print(obj);

        targetsObjects.Add(obj);


        EnumerateObject(obj, targetsObjects.IndexOf(obj));
    }

    public void RemoveAllTargets()
    {
        targetsObjects.Clear();
    }

    public void ResetCurrentTargetId()
    {
        currentTagetId = 0; 
    }

    public void GameRestarted()
    {
        OnGameRestarted?.Invoke();
    }

    public GameObject GetFinishPoint()
    {
        return finishPoint;
    }

    public GameObject GetFirstTarget()
    {
        targetsObjects[0].GetComponent<PointCollision>().ActiveCollider(true);

        return targetsObjects[0];
    }

    public List<GameObject> GetAllTargets()
    {
        return targetsObjects;
    }
   
    public void EnumerateObject(GameObject obj, int number)
    {
        obj.GetComponent<PointNumber>().SetPointNumber(number+1);
    }

}
