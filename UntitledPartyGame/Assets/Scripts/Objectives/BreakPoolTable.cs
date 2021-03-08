using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPoolTable : MonoBehaviour
{
    public int numHits = 5;
    public AudioClip breakPoolTableSFX;
    public float speedThreshold = 10f;
    public float explosionForce = 100f;
    public float explosionRadius = 5f;

    LevelManager lv;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
        rb = GetComponent<Rigidbody>();
    }

    void Break()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable") || other.CompareTag("PlayerHitbox"))
        {
            --numHits;
            if (numHits <= 0)
            {
                Break();
                AudioSource.PlayClipAtPoint(breakPoolTableSFX, Camera.main.transform.position);
                lv.TableComplete();
            }
        }
    }
}
