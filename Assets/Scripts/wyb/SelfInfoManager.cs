using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfInfoManager : MonoBehaviour
{
    public GameObject selfInfoWindow;       //货架信息窗口
    public GameObject floor;

    public static string[] floorInfo;

    private static int selfNumber;
    void Update()
    {
        PosJudgment();
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            SelfInfoShow();
        }
    }

    /// <summary>
    /// 判断当前用户所在货架
    /// </summary>
    public void PosJudgment()
    {
        GameObject Cam = GameObject.Find("Main Camera");   //获取摄像机
        List<Transform> targetPoint = CameraController.instance.targetPoint;
        Debug.Log("摄像机当前坐标" + Cam.transform.localPosition);
        Debug.Log(targetPoint[0].position);
        for (int i = 0; i < targetPoint.Count; i++)
        {
            Debug.Log(Cam.transform.position == targetPoint[i].position);
            if (Cam.transform.position == targetPoint[i].position)
            {
                selfNumber = i;    //获取当前货架编号
            }
        }
    }
    /// <summary>
    /// 货架信息展示
    /// </summary>
    private void SelfInfoShow()
    {
        //货架名字
        GameObject self_Name = selfInfoWindow.transform.Find("Self_Name").GetChild(1).gameObject;
        Text self_Name_Text = self_Name.GetComponent<Text>();
        self_Name_Text.text = JsonDataAnylize.rootObject.warehouse[0].shelf[selfNumber].name;
        //货架编号
        GameObject self_No = selfInfoWindow.transform.Find("Self_No").GetChild(1).gameObject;
        Text self_No_Text = self_No.GetComponent<Text>();
        self_No_Text.text = "未给";
        //货架层数
        GameObject self_Floor = selfInfoWindow.transform.Find("Self_Floor").GetChild(1).gameObject;
        Text self_Floor_Text = self_Floor.GetComponent<Text>();
        self_Floor_Text.text = JsonDataAnylize.rootObject.warehouse[0].shelf[selfNumber].floor.Count.ToString();
        //货架物品
        GameObject self_Goods_info = selfInfoWindow.transform.Find("Self_Goods").GetChild(2).gameObject;
        //Instantiate(floor, self_Goods_info.transform);

        //floorInfo = { JsonDataAnylize.rootObject.warehouse[0].shelf[selfNumber].floor[0].material[0].name ,
        //JsonDataAnylize.rootObject.warehouse[0].shelf[selfNumber].floor[0].material[0].count.ToString(),
        //JsonDataAnylize.rootObject.warehouse[0].shelf[selfNumber].floor[0].material[0].code,
        //JsonDataAnylize.rootObject.warehouse[0].shelf[selfNumber].floor[0].material[0].@class};
        
    }
}
