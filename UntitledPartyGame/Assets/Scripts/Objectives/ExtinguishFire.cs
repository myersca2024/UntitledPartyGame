using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
    public AudioClip extinguishSFX;
    LevelManager lv;
    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            //Visibly extinguish, sounds
            AudioSource.PlayClipAtPoint(extinguishSFX, transform.position);
            transform.Find("FireEffect").gameObject.SetActive(false);
            lv.FireComplete();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            AudioSource.PlayClipAtPoint(extinguishSFX, transform.position);
            transform.Find("FireEffect").gameObject.SetActive(false);
            lv.FireComplete();
        }
    }
}
