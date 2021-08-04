using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float slowMotionForce;

    private bool onSlowMotion;

    public void Start()
    {
        onSlowMotion = false;
    }

    public void ChangeGameVelocity()
    {
        if (onSlowMotion)
        {
            onSlowMotion = false;

            Time.timeScale = 1;
        }
        else
        {
            onSlowMotion = true;

            Time.timeScale = slowMotionForce;
        }
    }
}
