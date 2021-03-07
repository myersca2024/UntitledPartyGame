using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockoutHost : MonoBehaviour
{
    LevelManager lv;
    public AudioClip koSFX;
    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerPunch"))
        {
            //KO the character
            AudioSource.PlayClipAtPoint(koSFX, gameObject.transform.position);
            lv.HostComplete();
        }
    }
}
