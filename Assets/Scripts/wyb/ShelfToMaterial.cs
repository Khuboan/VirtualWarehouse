using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfToMaterial : MonoBehaviour
{
    private Animator WindowAnimator;
    public static string goButtonText;
    //public GameObject MateriaWindow;
    private void Start()
    {
        WindowAnimator = GameObject.Find("UI").GetComponent<Animator>();
        //MateriaWindow = GameObject.Find("MateriaWindow");
    }
    /// <summary>
    /// 点击按钮跳转至货物信息窗口
    /// </summary>
    public void ClickGoButton()
    {
        //获取到当前鼠标点击的层数按钮，即要查看的层数
        string[] arry01 = FloorButtonController.floorButtonText.Trim().Split('层');
        int nowFloor = int.Parse(arry01[0]);
        //获取到当前鼠标点击的Go按钮，即要查看的货物信息
        goButtonText = this.gameObject.transform.GetChild(1).GetComponent<Text>().text;
        int nowMaterial = int.Parse(goButtonText);
        // MaterialInfoGet.materialInfoGet.GetInfo(nowFloor, nowMaterial);

        try
        {
            MaterialInfoGet.materialInfoGet.GetInfo(nowFloor, nowMaterial);

        }
        catch
        {
            MaterialInfoGet.materialInfoGet.GetInfo(nowMaterial);

        }

        //货架信息窗口关闭，货物信息窗口开启
        WindowAnimator.SetBool("isClose", true);
        //MateriaWindow.
    }
}
