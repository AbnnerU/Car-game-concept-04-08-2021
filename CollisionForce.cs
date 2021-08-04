using UnityEngine;

public class CollisionForce : MonoBehaviour
{
    [SerializeField] private int layerIDToDetection;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == layerIDToDetection)
        {
            Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
            print(collisionForce);
        }
    }
}
