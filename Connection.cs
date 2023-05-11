using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class Connection : MonoBehaviour
{
    WebSocket websocket;
    public float temp;
    public float humid;


    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://192.168.8.100:80");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            

            // getting the message as a string
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("OnMessage! " + message);

            string[] StringValues = message.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            humid = float.Parse(StringValues[0]);
            temp = float.Parse(StringValues[1]);
        };

        InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {

#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending plain text
            await websocket.SendText("Hello");
        }
    }

    private async void OnApplicationQuit()
    {
   

        await websocket.Close();
    }
}
