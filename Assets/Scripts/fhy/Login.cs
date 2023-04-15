using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField Url;
    //string url;
    public GameObject[] UI;
    public bool isLogin;
    public static Login instance;
    public string ServerUrl;
    public bool isShowLink = true;
    public bool isShowRef = true;
    // Start is called before the first frame update
    private string fileName = "Url.txt";
    private void Start()
    {
        StartCoroutine(GetData());
    }
    IEnumerator GetData()
    {
        var uri = new System.Uri(Path.Combine(Application.streamingAssetsPath, "Url.txt"));
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            string jsonStr = www.downloadHandler.text;
            Url.text = jsonStr;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        ServerUrl = Url.text;
    }
    public void UrlLogin()
    {
        isLogin = true;
        transform.localScale = Vector3.zero;
        SceneManager.LoadScene(1);

        //DataConfig.instance.url.text = ServerUrl ;
        //DataConfig.instance.getdata = ServerUrl;
        // PostMsg.instance.Serverurl = url ;
        //PostMsg.instance.LoginPost();
        //CreateNewHouse.instance.GetHouseData();
        //minimapMgr.instance.WarehouseMapCreat();
        //minimapMgr.instance.WarehousesCreat();
    }
}
