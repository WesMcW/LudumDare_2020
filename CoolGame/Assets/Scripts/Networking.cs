using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using SocketIO;

public class Networking : MonoBehaviour
{
    SocketIOComponent socket;

    void Start()
    {
        socket = GetComponent<SocketIOComponent>();

        // The lines below setup 'listener' functions
        socket.On("connectionmessage", onConnectionEstabilished);
        socket.On("serverMessage", serverMessage);

        socket.On("LoadScores", LoadScores);
    }

    // This is the listener function definition
    void onConnectionEstabilished(SocketIOEvent evt)
    {
        Debug.Log("Player is connected: " + evt.data.GetField("id"));
    }

    void serverMessage(SocketIOEvent evt)
    {
        Debug.Log("woot");
    }

    public void button(int num)
    {
        JSONObject test = new JSONObject(num);
        socket.Emit("buttonClicked", test);
    }

    public void LoadScores(SocketIOEvent evt)
    {
        List<Score> highScores = new List<Score>();
        JSONObject m = evt.data.GetField("names");
        
        //string[] names = evt.data.GetField("names");

    }

    public void SendScore(string name, float score)
    {
        Score thing = new Score(name, score);
        var finalName = JsonConvert.SerializeObject(thing);
        JSONObject send = new JSONObject(finalName);

        socket.Emit("EnterScore", send);
    }
}

public class Score
{
    public string name;
    public float score;

    public Score(string n, float s)
    {
        name = n;
        score = s;
    }
}