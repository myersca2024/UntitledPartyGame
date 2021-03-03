using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{

    public Image reticleImage;
    public float throwStrength = 10f;
    public float grabRange = 5f;
    public Color reticleOnThrowable;

    GameObject player;
    bool holdingSomething;
    Color originalReticleColor;
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {

        }
    }
}
