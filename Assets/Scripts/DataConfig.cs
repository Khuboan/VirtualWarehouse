using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataConfig : MonoBehaviour
{
    public string ServerURL;
    public string getdata, lastdata;
    public InputField url;
    public Text test;
    public static DataConfig instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(lastdata!=getdata)
        {
            ServerURL = getdata;
        }
        lastdata = getdata;
    }
    public void SaveData()
    {
        //PlayerPrefs.DeleteKey("ServerURL");
        ServerURL = url.text;
        PlayerPrefs.SetString("myData", ServerURL);
        Debug.Log(PlayerPrefs.GetString("myData"));
        PlayerPrefs.Save();
    }
    public void OnLoadDataFromLocalStorage(string data)
    {
        getdata = data;
        Debug.Log("My data from localStorage: " + getdata);
        test.text = data;
        // Do something with myData
    }
}
