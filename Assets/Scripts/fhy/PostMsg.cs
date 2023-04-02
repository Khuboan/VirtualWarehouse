using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System.Net;
using System.Text;
using System.IO;
using System;
using BestHTTP.JSON.LitJson;

public class PostMsg : MonoBehaviour
{
    public static PostMsg instance;
    public string token;
    public string ServerUrl;
    private void Awake()
    {
        instance = this;
        ServerUrl = Login.instance.ServerUrl;
    }
    IEnumerator Start()
    {
        string url = ServerUrl + "/vw/get_token/";

        var request = new UnityWebRequest(url, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("Token: " + request.downloadHandler.text);
            GetToken getToken = JsonUtility.FromJson<GetToken>(request.downloadHandler.text);
            token = getToken.token;
            GetComponent<WebSocketExample>().Login();
        }
    }

}
public class GetToken
{
    public string msg;
    public string token;
}
