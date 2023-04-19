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
    /// �����ť��ת��������Ϣ����
    /// </summary>
    public void ClickGoButton()
    {
        //��ȡ����ǰ������Ĳ�����ť����Ҫ�鿴�Ĳ���
        string[] arry01 = FloorButtonController.floorButtonText.Trim().Split('��');
        int nowFloor = int.Parse(arry01[0]);
        //��ȡ����ǰ�������Go��ť����Ҫ�鿴�Ļ�����Ϣ
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

        //������Ϣ���ڹرգ�������Ϣ���ڿ���
        WindowAnimator.SetBool("isClose", true);
        //MateriaWindow.
    }
}
