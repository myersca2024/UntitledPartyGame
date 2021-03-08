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

    public GameObject copPrefab;
    public Transform spawnPoint;
    public int numberOfCops = 5;

    [Header("UI Elements")]
    public Slider noiseSlider;
    public Text timeToCopsText;
    public Text objectiveText;
    public Text noiseLevelText;
    public Color sliderStartColor = Color.blue;
    public Color sliderWinColor = Color.red;

    // Strings for each objective
    [Header("Main Objectives")]
    public string tvText = "Break The TV";
    public string speakerText = "Throw The Speakers Out Of The House";
    public string djText = "Punch The DJ";
    public string parentsText = "Call The Host's Parents";
    public string poolText = "Drain The Pool";

    [Header("Side Objectives")]
    public string punchText = "Spill Punch";
    public string liquorText = "Destroy The Liquor Bottles";
    public string foodText = "Throw All Of The Food Onto The Floor";
    public string tableText = "Break The Pool Table In The Garage";
    public string pongText = "Break The Table Tennis Table";
    public string fireText = "Extinguish Fire Pit";
    public string hostText = "Knock Out The Host";
    public string soloText = "Throw Out The Red Solo Cups";

    // booleans for each objective's completion status
    bool tv;
    bool speaker;
    bool dj;
    bool parents;
    bool pool;
    bool punch;
    bool liquor;
    bool food;
    bool table;
    bool pong;
    bool fire;
    bool host;
    bool solo;

    public static int currentNoiseLevel;
    string timeToCopsStartPrefixText = "Time Until Cops Are Called: ";
    float timer;
    bool cops = false;
    List<GameObject> partygoers;
    bool haveSpawned = false;
    bool chadstorm = false;
    string noiseLevelWinText = "It is quiet enough now.\nReturn home to win!";

    void Start()
    {
        currentNoiseLevel = startingNoiseLevel;
        noiseSlider.value = currentNoiseLevel;
        timer = timeToCops;
        partygoers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Partygoer"));
        SetObjectiveTextList();
        SetSliderColor(sliderStartColor);
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

                if (!haveSpawned)
                {
                    SpawnCops();
                }
            }

            SetTimeText();
        }

/*        if (Input.GetKeyDown(KeyCode.T) && !tv)
        {
            TVComplete();
            //MainObjectiveComplete();
        }

        if (Input.GetKeyDown(KeyCode.D) && !dj)
        {
            DJComplete();
        }

        if (Input.GetKeyDown(KeyCode.P) && !punch)
        {
            PongComplete();
            //SideObjectiveComplete();
        }

        if (Input.GetKeyDown(KeyCode.S) && !solo)
        {
            SoloComplete();
        }*/
    }

    public void ReduceNoiseLevel(int amount)
    {
        currentNoiseLevel -= amount;
        noiseSlider.value = currentNoiseLevel;

        if (currentNoiseLevel <= winNoiseThreshold)
        {
            chadstorm = true;
            Chadstorm();
            SetSliderColor(sliderWinColor);
            noiseLevelText.text = noiseLevelWinText;
        }
    }

    private void SetSliderColor(Color color)
    {
        noiseSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color 
            = color;
    }

    public void SideObjectiveComplete()
    {
        ReduceNoiseLevel(sideObjectiveNoiseReduction);
        PartgoersLeave();
        SetObjectiveTextList();
    }

    public void MainObjectiveComplete()
    {
        ReduceNoiseLevel(mainObjectiveNoiseReduction);
        SetObjectiveTextList();
    }

    private void PartgoersLeave()
    {
        if (partygoers.Count >= 1 && !chadstorm)
        {
            NPCAI npc = partygoers[partygoers.Count - 1].GetComponent<NPCAI>();
            npc.MakeNPCLeave();

            partygoers.RemoveAt(partygoers.Count - 1);
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

    // Strikesthrough the given string and changes it's color to green
    private string ObjectiveTextComplete(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }

        s = strikethrough;
        s = string.Format("<color=green>{0}</color>", s);
        return s;
    }

    private void SetObjectiveTextList()
    {
        objectiveText.text = string.Format("<B>Main Objectives</B>\n") +
                              tvText + "\n" +
                              speakerText + "\n" +
                              djText + "\n" +
                              parentsText + "\n" +
                              poolText + "\n\n" +
                              string.Format("<B>Side Objectives</B>\n") +
                              punchText + "\n" +
                              liquorText + "\n" +
                              foodText + "\n" +
                              tableText + "\n" +
                              pongText + "\n" +
                              fireText + "\n" +
                              hostText + "\n" +
                              soloText;
    }

    private void Chadstorm()
    {
        foreach (GameObject partygoer in partygoers)
        {
            NPCAI npc = partygoer.GetComponent<NPCAI>();
            npc.TurnChad();
        }
    }

    private void SpawnCops()
    {
        float x = 1f;
        for (int i = 0; i < numberOfCops; i++)
        {
            Invoke("SpawnACop", x);
            x += 1f;
        }
        haveSpawned = true;
    }
    
    private void SpawnACop()
    {
        Instantiate(copPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // Helper Methods to denote the completion of each objective, both main and side

    public void TVComplete()
    {
        if (!tv)
        {
            tv = true;
            tvText = ObjectiveTextComplete(tvText);
            MainObjectiveComplete();
        }
    }

    public void SpeakerComplete()
    {
        if (!speaker)
        {
            speaker = true;
            speakerText = ObjectiveTextComplete(speakerText);
            MainObjectiveComplete();
        }
    }

    public void DJComplete()
    {
        if (!dj)
        {
            dj = true;
            djText = ObjectiveTextComplete(djText);
            MainObjectiveComplete();
        }
    }

    public void ParentsComplete()
    {
        if (!parents)
        {
            parents = true;
            parentsText = ObjectiveTextComplete(parentsText);
            MainObjectiveComplete();
        }
    }

    public void PoolComplete()
    {
        if (!pool)
        {
            pool = true;
            poolText = ObjectiveTextComplete(poolText);
            MainObjectiveComplete();
        }
    }

    public void PunchComplete()
    {
        if (!punch)
        {
            punch = true;
            punchText = ObjectiveTextComplete(punchText);
            SideObjectiveComplete();
        }
    }

    public void LiquorComplete()
    {
        if (!liquor)
        {
            liquor = true;
            liquorText = ObjectiveTextComplete(liquorText);
            SideObjectiveComplete();
        }
    }

    public void FoodComplete()
    {
        if (!food)
        {
            food = true;
            foodText = ObjectiveTextComplete(foodText);
            SideObjectiveComplete();
        }
    }

    public void TableComplete()
    {
        if (!table)
        {
            table = true;
            tableText = ObjectiveTextComplete(tableText);
            SideObjectiveComplete();
        }
    }

    public void PongComplete()
    {
        if (!pong)
        {
            pong = true;
            pongText = ObjectiveTextComplete(pongText);
            SideObjectiveComplete();
        }
    }

    public void FireComplete()
    {
        if (!fire)
        {
            fire = true;
            fireText = ObjectiveTextComplete(fireText);
            SideObjectiveComplete();
        }
    }

    public void HostComplete()
    {
        if (!host)
        {
            host = true;
            hostText = ObjectiveTextComplete(hostText);
            SideObjectiveComplete();
        }
    }

    public void SoloComplete()
    {
        if (!solo)
        {
            solo = true;
            soloText = ObjectiveTextComplete(soloText);
            SideObjectiveComplete();
        }
    }
}
