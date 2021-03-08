using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Transform player;
    public GameObject textInteraction;
    public GameObject textAfterInteraction;
    public RaycastHit hit;
    public AudioSource audioComponent;
    public bool isInteracted;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        textInteraction.SetActive(false);
        textAfterInteraction.SetActive(false);
        isInteracted = false;
    }

    private void Update()
    {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.0f))
            {
                if (hit.collider.tag == "Interactable")
                {
                    textInteraction.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if(!isInteracted)
                        {
                            Destroy(textInteraction);
                            textInteraction = textAfterInteraction;
                        isInteracted = true;
                            EngagedInteraction();
                        }
                    }
                }
            }
            else
            {
                textInteraction.SetActive(false);
            }
    }

    void EngagedInteraction()
    {
        if(textAfterInteraction != null)
        {

            if (GetComponent<AudioSource>() != null)
            {
                audioComponent = GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioComponent.clip, transform.position);
            }
        }
    }
}
