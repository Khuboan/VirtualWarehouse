using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBox : MonoBehaviour
{
    public GameObject TipUI;
    public LineRenderer Line;

    public void OnMouseDown()
    {
        //显示提示牌
        TipUI.SetActive(true);
        //显示高亮
        gameObject.GetComponent<Outline>().enabled = true;
        //提示板和货箱间划线
        Line.enabled = true;
        DrawLine(transform.position, TipUI.transform.position + new Vector3(0, 0, 1.8f));
    }
    public void OnMouseUp()
    {
        //显示提示牌
        TipUI.SetActive(true);
        //显示高亮
        gameObject.GetComponent<Outline>().enabled = true;
        //提示板和货箱间划线
        Line.enabled = true;
        DrawLine(transform.position, TipUI.transform.position + new Vector3(0, 0, 1.8f));
    }

    //划线功能
    private void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        //是否使用世界坐标
        Line.useWorldSpace = true;
        //设置开始和结束位置0代表第一个点，1代表第二个点
        Line.SetPosition(0, startPos);
        Line.SetPosition(1, endPos);
        //开始和结束位置的颜色
        //Line.startColor = new Color(1, 0.8F, 0.1F, 0);
        //Line.endColor = Color.red;
        //开始和结束粗细大小
        //Line.startWidth = 0.02f;
        //Line.endWidth = 0.015f;
    }
}
