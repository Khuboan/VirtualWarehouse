using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfInfoGet : MonoBehaviour
{
    public static SelfInfoGet selfInfoGet;

    public GameObject Row_Prefab;   //��ͷԤ��
    public GameObject FloorButtonPre;   //��ťԤ��
    public GameObject ButtonGroup;    //��ť������
    public GameObject Panel;   //ÿ�������ϸ�б�
    public Text ShelfName, ShelfIndex, ShelfFloor;
    public static ShelfHub shelfHub;

    void Start()
    {
        
    }
    private void Awake()
    {
        selfInfoGet = this;
    }

    void Update()
    {
        shelfHub = CameraController.instance.targetPoint[CameraController.instance.camposIndex].parent.parent.GetComponent<ShelfHub>();
        //����ԭ����
        string shelfName = ShelfName.text;
        //��ʾ�������֣���ţ�����
        ShelfName.text = shelfHub.shelf.name;
        ShelfIndex.text = CameraController.instance.camposIndex.ToString();
        ShelfFloor.text = shelfHub.shelf.floor.Count.ToString();

        //����������ַ����仯
        if (ShelfName.text != shelfName)
        {
            //����ԭ���İ�ť
            for (int i = 0; i < ButtonGroup.transform.childCount; i++)
            {
                Destroy(ButtonGroup.transform.GetChild(i).gameObject);
            }
            CreateButton(shelfHub);
        }
    }

    /// <summary>
    /// ����������ť
    /// </summary>
    public void CreateButton(ShelfHub shelfHub)
    {
        //��ȡ���ܲ���
        int shelfFloor = shelfHub.shelf.floor.Count;
        //���ɰ�ť
        for (int i = 0; i < shelfFloor; i++)
        {
            GameObject row = GameObject.Instantiate(FloorButtonPre, ButtonGroup.transform.position, ButtonGroup.transform.rotation) as GameObject;
            row.name = "Button" + (i + 1);
            row.transform.SetParent(ButtonGroup.transform);
            row.transform.localScale = Vector3.one;   //�������ű���1,1,1����ȻĬ�ϵı����ǳ���

            //����Ԥ��ʵ���еĸ�����������ı�����
            FindChildGameObject(row.gameObject, "Button").transform.GetChild(0).GetComponent<Text>().text = (i + 1) + " ��";
        }
    }

    /// <summary>
    /// ����ÿһ�������Ϣ���б�
    /// </summary>
    /// <param name="shelfHub"></param>
    public void CreateTable(ShelfHub shelfHub)
    {
        //��ȡ����ǰ�������ť����Ҫ�鿴�Ĳ���
        string[] arry = FloorButtonController.buttonText.Trim().Split('��');
        int nowFloor = int.Parse(arry[0]);
        //Debug.LogError("���� = " + FloorButtonController.buttonText.Trim() + "  ����= " + arry.Length + "���� = " + shelfHub.shelf.floor.Count  + "��ǰ�㼶 = " + nowFloor);
        //Debug.LogError("�������� = " + shelfHub.shelf.floor[nowFloor].material.Count);

        //�����б�����

        for (int i = 0; i < shelfHub.shelf.floor[nowFloor-1].material.Count; i++)
        {
            GameObject table = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo/Panel/Table");
            GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//�������ű���1,1,1����ȻĬ�ϵı����ǳ���

            //����Ԥ��ʵ���еĸ�����������ı�����
            material material = shelfHub.shelf.floor[nowFloor-1].material[i];
            FindChildGameObject(row.gameObject, "Cell").GetComponent<Text>().text = material.name;
            FindChildGameObject(row.gameObject, "Cell1").GetComponent<Text>().text = material.code;
            FindChildGameObject(row.gameObject, "Cell2").GetComponent<Text>().text = material.count.ToString() + material.unit;
        }
    }

    /// <summary>
    /// ��ȡ������
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="childName"></param>
    /// <returns></returns>
    public GameObject FindChildGameObject(GameObject parent, string childName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == childName)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
