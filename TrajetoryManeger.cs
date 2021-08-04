
using UnityEngine;

public class TrajetoryManeger : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private FinalPoint finalPoint;

    private void Start()
    {
        finalPoint.OnFinishLine += OnFinishLineEvent;
    }

    public void StartDrawTrajetory()
    {        
        particle.Play();
    }

    public void PauseTrajetory()
    {
        particle.Pause();
    }

    public void ClearTrajetory()
    {
        particle.Clear();
    }

    private void OnFinishLineEvent()
    {
        PauseTrajetory();
    }
   
}
