using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableCreate : MonoBehaviour
{
    public GameObject Row_Prefab;//��ͷԤ��

    void Start()
    {
        for (int i = 0; i < 10; i++)//��Ӳ��޸�Ԥ��Ĺ��̣�������10��
        {
            GameObject table = GameObject.Find("UI/SelfInfoWindow/Self_Goods/Panel/Table");
            GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//�������ű���1,1,1����ȻĬ�ϵı����ǳ���
                                                   //����Ԥ��ʵ���еĸ�����������ı�����
            FindChildGameObject(row.gameObject, "Cell").GetComponent<Text>().text = (i + 1) + "";
            //row.transform.FindChild("cell").GetComponent<Text>().text = (i + 1) + "";
            FindChildGameObject(row.gameObject, "Cell1").GetComponent<Text>().text = "name" + (i + 1);
            FindChildGameObject(row.gameObject, "Cell2").GetComponent<Text>().text = "class" + (i + 1);
        }
    }

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
