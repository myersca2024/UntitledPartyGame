using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{

    public Image reticleImage;
    public float throwStrength = 50f;
    public float grabRange = 3f;
    public float holdDistance = 1f;
    public Color reticleOnThrowable;

    public AudioClip grabSFX;
    public AudioClip throwSFX;

    GameObject player;
    bool holdingSomething;
    Color originalReticleColor;
    GameObject heldItem;
    // Start is called before the first frame update
    void Start()
    {
        originalReticleColor = reticleImage.color;
        holdingSomething = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame 
    void Update()
    {
        HandleGrab();
    }

    void HandleGrab()
    {
        RaycastHit hit;
        if(holdingSomething)
        {
            //Debug.Log("Holding");
            heldItem.transform.localPosition = new Vector3(0, 0, holdDistance);
            heldItem.transform.rotation = transform.rotation;
            if (Input.GetMouseButtonDown(1)) //I apparently can't have this in the if Raycast, despite the object literally being right in your face
            {
                Debug.Log("Throw");
                holdingSomething = false;
                transform.DetachChildren();
                heldItem.GetComponent<Rigidbody>().AddForce(transform.forward * throwStrength, ForceMode.VelocityChange);
                heldItem = null;
                AudioSource.PlayClipAtPoint(throwSFX, transform.position);
                }
            }
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {
            if(hit.collider.CompareTag("Throwable"))
            {
                reticleImage.color = reticleOnThrowable;
                if (Input.GetMouseButtonDown(1)) //On right click...
                {
                    if(holdingSomething)
                    {
                        //Do Nothing
                    }
                    else
                    {
                        Debug.Log("Grab");
                        holdingSomething = true;
                        heldItem = hit.collider.gameObject;
                        heldItem.transform.SetParent(gameObject.transform);
                        AudioSource.PlayClipAtPoint(grabSFX, transform.position);
                    }
                }
            }
            else
            {
                reticleImage.color = originalReticleColor;
            }
        }
    }
}
