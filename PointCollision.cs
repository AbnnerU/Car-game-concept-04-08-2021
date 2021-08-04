using UnityEngine;

public class PointCollision : MonoBehaviour
{
    [SerializeField] private SphereCollider pointCollider;
    private Targets targetsScript;

    private void Start()
    {
        targetsScript = FindObjectOfType<Targets>();

        targetsScript = FindObjectOfType<Targets>();

        targetsScript.OnGameRestarted += OnGameRestartedEvent;
    }

    private void OnEnable()
    {
        pointCollider.enabled = false;       
    }

    private void OnTriggerEnter(Collider other)
    {
        targetsScript.ChangeCurrentTarget();
    }

    public void ActiveCollider(bool setActive)
    {
        pointCollider.enabled = setActive;
    }

    public void OnGameRestartedEvent()
    {
        pointCollider.enabled = false;
    }
}
