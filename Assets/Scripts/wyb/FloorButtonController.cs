using UnityEngine;
using UnityEngine.UI;

public class FloorButtonController : MonoBehaviour
{
    private GameObject targetObject;   // ��Ҫ���Ƶ�GameObject
    public static string buttonText;

    private void Start()
    {
         targetObject = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo");
         
    }
    private void Update()
    {

    }

    /// <summary>
    /// ��ť����¼�
    /// </summary>
    public void ClickButton()
    {
        //�ж��б�״̬
        if(targetObject.activeSelf == false)
        {
            targetObject.SetActive(true);
        }
        else
        {
            targetObject.SetActive(false);
        }

        buttonText = gameObject.transform.GetChild(0).GetComponent<Text>().text;

        //���б���ʾ��������İ�ť�·�
        targetObject.transform.position = gameObject.transform.position - new Vector3(0, 20, 0);
    }
}