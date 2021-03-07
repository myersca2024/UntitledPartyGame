using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBehavior : MonoBehaviour
{
    public float speedThreshold = 10f;
    public float explosionForce = 100f;
    public float explosionRadius = 5f;
    public AudioClip breakSFX;
    private Rigidbody rb;
    private DestroyLiquorObjective dlo;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        dlo = FindObjectOfType<DestroyLiquorObjective>();

        if (gameObject.CompareTag("LiquorBottle"))
        {
            dlo.IncreaseBottles();
        }
    }

    void Break()
    {
        if (gameObject.CompareTag("LiquorBottle"))
        {
            dlo.DecreaseBottles();
        }

        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<Rigidbody>();
            child.gameObject.AddComponent<BoxCollider>();
        }

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, child.position, explosionRadius);
        }

        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(this.gameObject.GetComponent<Rigidbody>());
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
            AudioSource.PlayClipAtPoint(breakSFX, transform.position);
            if (collision.gameObject.CompareTag("Throwable") || collision.gameObject.CompareTag("Breakable") || collision.gameObject.CompareTag("LiquorBottle"))
            {
                collision.gameObject.GetComponent<ThrowableBehavior>().Break();
            }
            Break();
        }
    }
}
