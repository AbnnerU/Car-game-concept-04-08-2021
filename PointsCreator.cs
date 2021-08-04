using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCreator : MonoBehaviour
{
    [SerializeField] private Targets targetsScript;

    [SerializeField] private CanvasManeger canvasManeger;

    [SerializeField] private GameObject pointsPrefab;

    [SerializeField] private LayerMask editionLayers;

    [SerializeField] private LayerMask pointLayer;

    public Action<bool> HaveTargets;

    public EventHandler<OnObjectSelectArgs> OnObjectSelect;
    public class OnObjectSelectArgs : EventArgs
    {
        public GameObject referenceObject;
        public bool selected;
    }

    private GameObject currentPoint;
    private Vector3 pointSize;

    private Vector3 beforeEditionPosition;

    private bool haveTarget;

    private void Start()
    {
      
        haveTarget = false;

        HaveTargets?.Invoke(false);
    }

    public void CreateNewPoint()
    {
        RaycastHit hitInfo = new RaycastHit();
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)), out hitInfo, Mathf.Infinity, editionLayers);
        Debug.DrawLine(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)).origin, hitInfo.point);

        currentPoint = PoolManager.SpawnObject(pointsPrefab);

        float pointRadius = currentPoint.GetComponent<SphereCollider>().radius;

        pointSize = Vector3.up * pointRadius;

        currentPoint.transform.position = hitInfo.point + pointSize;


        OnObjectSelect?.Invoke(this, new OnObjectSelectArgs { referenceObject = currentPoint, selected=true});       
    }

    public void ChangePointPosition()
    {
        RaycastHit hitInfo = new RaycastHit();
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, editionLayers);

        Debug.DrawLine(Camera.main.ScreenPointToRay(Input.mousePosition).origin, hitInfo.point);

        currentPoint.transform.position = hitInfo.point + pointSize;
    }

    #region To new points 

    public void ComfirmNewPoint()
    {
        if (/*targetsScript.GetFirstTarget() == null &&*/ haveTarget == false)
        {
            haveTarget = true;

            HaveTargets.Invoke(true);
        }

        targetsScript.AddNewTarget(currentPoint);

        OnObjectSelect?.Invoke(this, new OnObjectSelectArgs { referenceObject = currentPoint, selected = false });

        currentPoint = null;
    }

    public void CancelNewPoint()
    {
        PoolManager.ReleaseObject(currentPoint);
    }
    #endregion

    #region To edit points

    public void TrySelectPoint()
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, pointLayer))
        {

            currentPoint = hitInfo.collider.gameObject.transform.parent.gameObject;

            OnObjectSelect?.Invoke(this, new OnObjectSelectArgs { referenceObject = currentPoint, selected = true });

            beforeEditionPosition = currentPoint.transform.position;

            canvasManeger.EnablePointPositionEditor();
        }
    }    

    public void CancelNewPosition()
    {
        currentPoint.transform.position = beforeEditionPosition;

        OnObjectSelect?.Invoke(this, new OnObjectSelectArgs { referenceObject = currentPoint, selected = false });
    }

    public void DeselectPoint()
    {
        OnObjectSelect?.Invoke(this, new OnObjectSelectArgs { referenceObject = currentPoint, selected = false });
    }

    #endregion


    public void ResetAllPonits()
    {
        List<GameObject> allPoints = targetsScript.GetAllTargets();

        foreach(GameObject g in allPoints)
        {
            PoolManager.ReleaseObject(g);
        }

        haveTarget = false;

        targetsScript.RemoveAllTargets();

        HaveTargets?.Invoke(false);
    }
}
