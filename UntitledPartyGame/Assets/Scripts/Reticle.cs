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

    float reload = 0.2f;
    float currentReload;
    bool isReload = false;
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
        currentReload = 0f;
    }

    // Update is called once per frame 
    void Update()
    {
        if (isReload)
        {
            currentReload -= Time.deltaTime;
        }
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
            if (Input.GetMouseButtonDown(1) && currentReload <= 0f) //I apparently can't have this in the if Raycast, despite the object literally being right in your face
            {
                Debug.Log("Throw");
                AudioSource.PlayClipAtPoint(throwSFX, transform.position);
                holdingSomething = false;
                transform.DetachChildren();
                Rigidbody rb = heldItem.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.AddForce((transform.forward + Vector3.up * 0.3f) * throwStrength, ForceMode.VelocityChange);
                heldItem = null;

                currentReload = reload;
                isReload = true;
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {
            if(hit.collider.CompareTag("Throwable") || hit.collider.CompareTag("LiquorBottle")) 
            {
                reticleImage.color = reticleOnThrowable;
                if (Input.GetMouseButtonDown(1) && currentReload <= 0f) //On right click...
                {
                    if(holdingSomething)
                    {
                        //Do Nothing
                    }
                    else
                    {
                        Debug.Log("Grab");
                        AudioSource.PlayClipAtPoint(grabSFX, transform.position);
                        holdingSomething = true;
                        heldItem = hit.collider.gameObject;
                        heldItem.transform.SetParent(gameObject.transform);

                        currentReload = reload;
                        isReload = true;
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
