using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehavior : MonoBehaviour
{
    public float speedThreshold = 10f;
    public float explosionForce = 100f;
    public float explosionRadius = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Break()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.AddComponent<BoxCollider>();
        }

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, child.position, explosionRadius);
        }

        this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }

        Debug.Log(rb.velocity.magnitude.ToString());
        if (rb.velocity.magnitude >= speedThreshold)
        {
            if (collision.gameObject.CompareTag("Throwable") || collision.gameObject.CompareTag("Breakable"))
            {
                collision.gameObject.GetComponent<ThrowableBehavior>().Break();
            }
            Break();
        }
    }
}
