using UnityEngine;
using BestHTTP.WebSocket;
using System;
using System.Collections;
using System.Collections.Generic;

public class WebSocketExample : MonoBehaviour
{
    public WebSocket webSocket;
    public string Msg;
    string LastMsg;
    public minimapMgr minimapMgr;
    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    Login();
        //}
        if(Msg!=LastMsg)
        {
            JsonDataAnylize.instance.JsonData(Msg);
            CreateNewHouse.instance.GetHouseData();
            minimapMgr.WarehouseMapCreat();
            minimapMgr.WarehousesCreat();
        }
        LastMsg = Msg;
    }


    public void Login()
    {
        string token = GetComponent<PostMsg>().token;
        string url = $"ws://116.62.71.145:8888/vw/ws/{token}/";

        webSocket = new WebSocket(new System.Uri(url));
        webSocket.OnOpen += OnWebSocketOpen;
        webSocket.OnMessage += OnWebSocketMessage;
        webSocket.OnError += OnWebSocketError;
        webSocket.OnClosed += OnWebSocketClosed;
        webSocket.Open();
    }

    void OnWebSocketOpen(WebSocket webSocket)
    {
        Debug.Log("WebSocket connected!");
    }

    void OnWebSocketMessage(WebSocket webSocket, string message)
    {
        Debug.Log("WebSocket received message: " + message);
        Msg = message;

        //GetComponent<JsonDataAnylize>().JsonData(message);
    }

    void OnWebSocketError(WebSocket webSocket, string error)
    {
        Debug.Log("WebSocket error: " + error);
        webSocket.Close();
        StartCoroutine(ReconnectAfterDelay(5));
    }
    IEnumerator ReconnectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        webSocket.Open();
    }
    void OnWebSocketClosed(WebSocket webSocket, UInt16 code, string message)
    {
        Debug.Log("WebSocket closed with code " + code + " and message: " + message);
    }
    private void OnApplicationQuit()
    {
        if (webSocket != null)
        {
            webSocket.Close();
            webSocket = null;
        }
    }
    void OnDestroy()
    {
        if (webSocket != null)
        {
            webSocket.Close();
            webSocket = null;
        }
    }
}
