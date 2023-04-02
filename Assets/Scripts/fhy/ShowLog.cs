using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLog : MonoBehaviour
{
    [TextArea]
    public string Log;
    public Text Logtext;
    public InputField testlog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Logtext.text = Log;
    }
    public void GetLog(string a)
    {
        Log += "\n\n" + a;
    }
    public void GetLog()
    {

        Log = "\n\n" + testlog.text + Log;
        testlog.text = "";
    }
}
