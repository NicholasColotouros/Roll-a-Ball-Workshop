using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour
{
    public float bumpDecay;
    void OnCollisionEnter(Collision collision)
    {
        // Reflection along normal:
        // r = d - 2 (d * n) n
        Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
        if(rb != null)
        {
            Vector3 d = rb.velocity;
            foreach (ContactPoint cp in collision.contacts)
            {
                Vector3 reflectedVelocity = Vector3.Reflect(d, Vector3.Normalize(cp.normal)) * bumpDecay;
                reflectedVelocity.y = 0;
                rb.AddForce(reflectedVelocity, ForceMode.Impulse);
            }
        }
    }
}
