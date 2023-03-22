using UnityEngine;
using UnityEngine.UI;

public class FloorButtonController : MonoBehaviour
{
    private GameObject targetObject;   // 需要控制的GameObject
    public static string buttonText;

    private void Start()
    {
         targetObject = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo");
         
    }
    private void Update()
    {

    }

    /// <summary>
    /// 按钮点击事件
    /// </summary>
    public void ClickButton()
    {
        //判断列表状态
        if(targetObject.activeSelf == false)
        {
            targetObject.SetActive(true);
        }
        else
        {
            targetObject.SetActive(false);
        }

        buttonText = gameObject.transform.GetChild(0).GetComponent<Text>().text;

        //将列表显示在所点击的按钮下方
        targetObject.transform.position = gameObject.transform.position - new Vector3(0, 20, 0);
    }
}