using System;

using UnityEngine;

public class FinalPoint : MonoBehaviour
{
    public Action OnFinishLine;

    private void OnTriggerEnter(Collider other)
    {
        OnFinishLine?.Invoke();
        print("Finish");
    }

    
}
