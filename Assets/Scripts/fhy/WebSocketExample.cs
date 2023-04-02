using UnityEngine;
using BestHTTP.WebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class WebSocketExample : MonoBehaviour
{
    public WebSocket webSocket;
    public string Msg;
    string LastMsg;
    public minimapMgr minimapMgr;
    public bool isLink;
    public GameObject linkUI, nolinkUI;
    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    Login();
        //}
        linkUI.SetActive(isLink);
        nolinkUI.SetActive(!isLink);
        if (Msg != LastMsg)
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
        string ServerUrl = GetComponent<PostMsg>().ServerUrl.Split('/')[GetComponent<PostMsg>().ServerUrl.Split('/').Length-1];
        string url = "ws://" + ServerUrl + $"/vw/ws/{token}/";

        webSocket = new WebSocket(new System.Uri(url));
        webSocket.OnOpen += OnWebSocketOpen;
        webSocket.OnMessage += OnWebSocketMessage;
        webSocket.OnError += OnWebSocketError;
        webSocket.OnClosed += OnWebSocketClosed;
        webSocket.Open();
    }

    void OnWebSocketOpen(WebSocket webSocket)
    {
        isLink = true;
        Debug.Log("WebSocket connected!");
    }

    void OnWebSocketMessage(WebSocket webSocket, string message)
    {
        isLink = true;

        Debug.Log("WebSocket received message: " + message);
        Msg = message;

        //GetComponent<JsonDataAnylize>().JsonData(message);
    }

    void OnWebSocketError(WebSocket webSocket, string error)
    {
        isLink = false;

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
