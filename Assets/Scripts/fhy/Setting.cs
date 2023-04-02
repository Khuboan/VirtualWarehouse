using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public InputField url;
    public bool isShowLink = true;
    public bool isShowRef = true;
    public Toggle showLink, ShowRef;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        isShowLink = Login.instance.isShowLink;
        isShowRef = Login.instance.isShowRef;
        showLink.isOn = isShowLink;
        ShowRef.isOn = isShowRef;
        url.text = Login.instance.ServerUrl;
    }
    public void SetUrl()
    {
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.localScale = Vector3.zero;

    }
    public void Close()
    {
        transform.localScale = Vector3.zero;
    }
    public void Open()
    {
        transform.localScale = Vector3.one;
    }
    // Update is called once per frame
    void Update()
    {
        isShowLink = showLink.isOn;
        isShowRef = ShowRef.isOn;
        Login.instance.isShowLink = isShowLink;
        Login.instance.isShowRef = isShowRef;
        Login.instance.ServerUrl = url.text;

    }
}
