using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreFunctions : MonoBehaviour
{
    Networking server;

    public GameObject nameList;
    public GameObject scoreList;

    // Start is called before the first frame update
    void Start()
    {
        server = Networking.inst;

        ClearTexts();
        Invoke("WaitToLoad", 0.5F);
    }

    void WaitToLoad()
    {
        server.LoadScores(this);
    }

    public void ReceiveScores(List<HighScore> scores)
    {
        Debug.Log("huh");

        for(int i = 0; i < scores.Count; i++)
        {
            string name = scores[i].name.Trim('"');

            nameList.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = name;
            scoreList.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = SecondsToTime(scores[i].score);
        }
    }

    public string SecondsToTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60F);
        seconds -= (float)(minutes * 60);
        seconds *= 100F;
        seconds = (float)Mathf.Round(seconds);
        seconds /= 100F;

        string time;
        if (seconds < 10) time = minutes.ToString() + ":0" + seconds.ToString();
        else time = minutes.ToString() + ":" + seconds.ToString();
        return time;
    }

    public void ClearTexts()
    {
        for(int i = 0; i < 5; i++)
        {
            nameList.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
            scoreList.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
