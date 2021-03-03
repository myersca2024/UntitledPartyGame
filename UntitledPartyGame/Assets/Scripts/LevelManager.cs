using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public int startingNoiseLevel = 200;
    public int winNoiseThreshold = 75;
    public int timeToCops = 300;
    public int sideObjectiveNoiseReduction = 25;
    public int mainObjectiveNoiseReduction = 50;

    public Slider noiseSlider;
    public Text timeToCopsText;

    int currentNoiseLevel;
    string timeToCopsStartPrefixText = "Time Until Cops Are Called: ";
    float timer;
    bool cops = false;
    GameObject[] partygoers;
    
    // Start is called before the first frame update
    void Start()
    {
        currentNoiseLevel = startingNoiseLevel;
        noiseSlider.value = currentNoiseLevel;
        timer = timeToCops;
        partygoers = GameObject.FindGameObjectsWithTag("Partygoer");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                cops = true;
            }

            SetTimeText();
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(0))
        {
            MainObjectiveComplete();
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.R))
        {
            SideObjectciveComplete();
        }
    }

    public void ReduceNoiseLevel(int amount)
    {
        currentNoiseLevel -= amount;
        noiseSlider.value = currentNoiseLevel;

        if (currentNoiseLevel <= winNoiseThreshold)
        {
            Chadstorm();
        }
    }

    public void SideObjectciveComplete()
    {
        ReduceNoiseLevel(sideObjectiveNoiseReduction);
        PartgoersLeave();
    }

    public void MainObjectiveComplete()
    {
        ReduceNoiseLevel(mainObjectiveNoiseReduction);
    }

    private void PartgoersLeave()
    {
        if (partygoers.Length >= 1)
        {
            Destroy(partygoers[partygoers.Length - 1]);

            partygoers = GameObject.FindGameObjectsWithTag("Partygoer");
        }
    }

    private void SetTimeText()
    {
        if (cops)
        {
            timeToCopsText.text = "Cops Are On The Way!";
        }
        else
        {
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds;
            if (timer < 10)
            {
                seconds = (timer % 60).ToString("00.00");
            }
            else
            {
                seconds = (timer % 60).ToString("00");
            }

            timeToCopsText.text = timeToCopsStartPrefixText + minutes + ":" + seconds;
        }
    }

    private void Chadstorm()
    {
        foreach (GameObject partygoer in partygoers)
        {
            partygoer.tag = "Chad";
        }
    }
}
