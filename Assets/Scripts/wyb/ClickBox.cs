using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBox : MonoBehaviour
{
    public GameObject TipUI;
    public LineRenderer Line;

    public void OnMouseDown()
    {
        //��ʾ��ʾ��
        TipUI.SetActive(true);
        //��ʾ����
        gameObject.GetComponent<Outline>().enabled = true;
        //��ʾ��ͻ���仮��
        Line.enabled = true;
        DrawLine(transform.position, TipUI.transform.position + new Vector3(0, 0, 1.8f));
    }
    public void OnMouseUp()
    {
        //��ʾ��ʾ��
        TipUI.SetActive(true);
        //��ʾ����
        gameObject.GetComponent<Outline>().enabled = true;
        //��ʾ��ͻ���仮��
        Line.enabled = true;
        DrawLine(transform.position, TipUI.transform.position + new Vector3(0, 0, 1.8f));
    }

    //���߹���
    private void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        //�Ƿ�ʹ����������
        Line.useWorldSpace = true;
        //���ÿ�ʼ�ͽ���λ��0�����һ���㣬1����ڶ�����
        Line.SetPosition(0, startPos);
        Line.SetPosition(1, endPos);
        //��ʼ�ͽ���λ�õ���ɫ
        //Line.startColor = new Color(1, 0.8F, 0.1F, 0);
        //Line.endColor = Color.red;
        //��ʼ�ͽ�����ϸ��С
        //Line.startWidth = 0.02f;
        //Line.endWidth = 0.015f;
    }
}
