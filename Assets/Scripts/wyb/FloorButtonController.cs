using UnityEngine;
using UnityEngine.UI;

public class FloorButtonController : MonoBehaviour
{
    private GameObject targetObject;   // ��Ҫ���Ƶ�GameObject
    private GameObject table;
    public static string floorButtonText;

    private void Start()
    {
        targetObject = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo");
        table = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo/Panel/Table");
    }
    /// <summary>
    /// ��ť����¼�
    /// </summary>
    public void ClickButton()
    {
        //������ťԭ����
        floorButtonText = gameObject.transform.GetChild(0).GetComponent<Text>().text;

        //���б���ʾ��������İ�ť�·�
        targetObject.transform.position = gameObject.transform.position - new Vector3(0, 20, 0);

        //�ж��б�״̬
        if (targetObject.activeSelf == false)
        {
            targetObject.SetActive(true);

            //�����б���Ϣ
            SelfInfoGet.selfInfoGet.CreateTable(SelfInfoGet.shelfHub);
        }
        else
        {
            targetObject.SetActive(false);
            //����ԭ�����б�
            //���������ť���ַ����仯
            for (int i = 1; i < table.transform.childCount; i++)
            {
                Destroy(table.transform.GetChild(i).gameObject);
            }
        }
    }
}