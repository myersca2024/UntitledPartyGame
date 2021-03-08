using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    public GameObject textInteraction;
    public GameObject textAfterInteraction;
    public GameObject errorText;
    public AudioSource audioComponent;
    public bool isInteracted = false;
    public string obj;
    public LevelManager lv;

    public static bool haveNumber = false;

    private bool didError = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textInteraction.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && !isInteracted)
            {
                if (obj == "Fridge")
                {
                    Destroy(textInteraction);
                    textInteraction = textAfterInteraction;
                    isInteracted = true;
                    EngagedInteraction();
                    textInteraction.SetActive(true);
                    haveNumber = true;
                }
                else if (obj == "Phone")
                {
                    if (haveNumber)
                    {
                        Destroy(textInteraction);
                        textInteraction = textAfterInteraction;
                        isInteracted = true;
                        EngagedInteraction();
                        textInteraction.SetActive(true);
                        lv.ParentsComplete();
                    }
                    else
                    {
                        didError = true;
                        textInteraction.SetActive(false);
                        errorText.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (didError)
        {
            errorText.SetActive(false);
            didError = false;
        }
        else
        {
            textInteraction.SetActive(false);
        }
    }

    void EngagedInteraction()
    {
        if (textAfterInteraction != null)
        {
            if (GetComponent<AudioSource>() != null)
            {
                AudioSource.PlayClipAtPoint(audioComponent.clip, transform.position);
            }
        }
    }
}
