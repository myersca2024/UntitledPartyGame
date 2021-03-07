using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishFire : MonoBehaviour
{
    public AudioClip extinguishSFX;
    LevelManager lv;
    public AudioClip extinguishSFX;
    // Start is called before the first frame update
    void Start()
    {
        lv = FindObjectOfType<LevelManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable") || collision.gameObject.CompareTag("PlayerPunch"))
        {
            //Visibly extinguish, sounds
<<<<<<< Updated upstream
            AudioSource.PlayClipAtPoint(extinguishSFX, transform.position);
=======
            AudioSource.PlayClipAtPoint(extinguishSFX, gameObject.transform.position);
>>>>>>> Stashed changes
            lv.FireComplete();
        }
    }

}
