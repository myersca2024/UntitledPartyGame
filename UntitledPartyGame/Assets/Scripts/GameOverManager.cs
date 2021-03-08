using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text sleepText;
    public TMP_Text sleepDescText;
    int hoursOfSleep;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        int noise = PlayerPrefs.GetInt("noise");
        int hoursCalc = 10 - (noise / 14);
        hoursOfSleep = hoursCalc;

        if (LevelManager.isGameOver)
        {
            Arrested();
        }
        else
        {
            if (hoursOfSleep < 4)
            {
                BadSleep();
            }
            else if (hoursOfSleep < 6)
            {
                OkaySleep();
            }
            else if (hoursOfSleep < 9)
            {
                GoodSleep();
            }
            else
            {
                GreatSleep();
            }
        }
    }

    void Arrested()
    {
        sleepText.text = "You got 0 hours of sleep.";
        sleepDescText.text = "You got caught on your huge rampage and spent all night in a jail cell wondering if it" +
            "was worth it to get yourself in this situation.";
    }

    void BadSleep()
    {
        sleepText.text = "You got " + hoursOfSleep.ToString() + " hours of sleep.";
        sleepDescText.text = "While you did your best, the party next door just kept going despite your interference." +
            "You didn't get a whole lot of sleep and felt pretty terrible the next morning.";
    }

    void OkaySleep()
    {
        sleepText.text = "You got " + hoursOfSleep.ToString() + " hours of sleep.";
        sleepDescText.text = "You managed to get the neighbors to calm down somewhat. You at least got more sleep" +
            "than you would have if you didn't interfere. You woke up the next morning groggy, but it wasn't anything" +
            "you weren't used to.";
    }

    void GoodSleep()
    {
        sleepText.text = "You got " + hoursOfSleep.ToString() + " hours of sleep.";
        sleepDescText.text = "All your meddling made them quiet down a good deal! Upon going home, you slept very well " +
            "and felt rested in the morning.";
    }

    void GreatSleep()
    {
        sleepText.text = "You got " + hoursOfSleep.ToString() + " hours of sleep.";
        sleepDescText.text = "You absolutely destroyed the party. You did not hear a single peep after going home. All your " +
            "meddling ruined the party and everyone went home. Because of that, you slept in and felt amazing the next morning!";
    }

    public void ExitGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
