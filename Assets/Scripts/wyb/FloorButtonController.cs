using UnityEngine;
using UnityEngine.UI;

public class FloorButtonController : MonoBehaviour
{
    private GameObject targetObject;   // 需要控制的GameObject
    private GameObject table;
    public static string floorButtonText;

    private void Start()
    {
        targetObject = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo");
        table = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo/Panel/Table");
    }
    /// <summary>
    /// 按钮点击事件
    /// </summary>
    public void ClickButton()
    {
        //层数按钮原名字
        floorButtonText = gameObject.transform.GetChild(0).GetComponent<Text>().text;

        //将列表显示在所点击的按钮下方
        targetObject.transform.position = gameObject.transform.position - new Vector3(0, 20, 0);

        //判断列表状态
        if (targetObject.activeSelf == false)
        {
            targetObject.SetActive(true);

            //创建列表信息
            SelfInfoGet.selfInfoGet.CreateTable(SelfInfoGet.shelfHub);
        }
        else
        {
            targetObject.SetActive(false);
            //销毁原来的列表
            //如果层数按钮名字发生变化
            for (int i = 1; i < table.transform.childCount; i++)
            {
                Destroy(table.transform.GetChild(i).gameObject);
            }
        }
    }
}