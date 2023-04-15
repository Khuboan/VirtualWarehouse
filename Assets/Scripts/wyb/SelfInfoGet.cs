using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfInfoGet : MonoBehaviour
{
    public static SelfInfoGet selfInfoGet;

    public GameObject Row_Prefab;   //表头预设
    public GameObject FloorButtonPre;   //按钮预设
    public GameObject ButtonGroup;    //按钮父物体
    public GameObject Panel;   //每层货物详细列表
    public Text ShelfName, ShelfIndex, ShelfFloor;
    public static ShelfHub shelfHub;
    public static BinHub binHub;

    void Start()
    {
        GameObject titleGoButton = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo/Panel/Table/Title/GoButton");
        Destroy(titleGoButton.gameObject);
    }
    private void Awake()
    {
        selfInfoGet = this;
    }

    void Update()
    {
        if (JsonDataAnylize.instance.rootObject.warehouse.Length == 0) return;
        string shelfName = ShelfName.text;
        if (CameraController.instance.targetPoint[CameraController.instance.camposIndex].parent.parent.GetComponent<ShelfHub>()!=null)
        {
            shelfHub = CameraController.instance.targetPoint[CameraController.instance.camposIndex].parent.parent.GetComponent<ShelfHub>();
            ShelfName.text = shelfHub.shelf.name;
            ShelfIndex.text = CameraController.instance.camposIndex.ToString();
            ShelfFloor.text = shelfHub.shelf.floor.Count.ToString();
            
            //如果货架名字发生变化
            if (ShelfName.text != shelfName)
            {
                //销毁原来的按钮
                for (int i = 0; i < ButtonGroup.transform.childCount; i++)
                {
                    Destroy(ButtonGroup.transform.GetChild(i).gameObject);
                }
                CreateButton(shelfHub);
            }
        }
        else
        {
            binHub = CameraController.instance.targetPoint[CameraController.instance.camposIndex].parent.GetComponent<BinHub>();
            ShelfName.text = binHub.bin.name;
            ShelfIndex.text = CameraController.instance.camposIndex.ToString();
            ShelfFloor.text = binHub.bin.floor.ToString();
            //如果货架名字发生变化
            if (ShelfName.text != shelfName)
            {
                //销毁原来的按钮
                for (int i = 0; i < ButtonGroup.transform.childCount; i++)
                {
                    Destroy(ButtonGroup.transform.GetChild(i).gameObject);
                }
                CreateButton(binHub);
            }
        }
       
    }

    /// <summary>
    /// 创建层数按钮
    /// </summary>
    public void CreateButton(ShelfHub shelfHub)
    {
       // Debug.Log("货架创建层数按钮");
        //获取货架层数
        int shelfFloor = shelfHub.shelf.floor.Count;
        //生成按钮
        for (int i = 0; i < shelfFloor; i++)
        {
            GameObject row = GameObject.Instantiate(FloorButtonPre, ButtonGroup.transform.position, ButtonGroup.transform.rotation) as GameObject;
            row.name = "Button" + (i + 1);
            row.transform.SetParent(ButtonGroup.transform);
            row.transform.localScale = Vector3.one;   //设置缩放比例1,1,1，不然默认的比例非常大

            //设置预设实例中的各个子物体的文本内容
            FindChildGameObject(row.gameObject, "Button").transform.GetChild(0).GetComponent<Text>().text = (i + 1) + " 层";
        }
    }
    /// <summary>
    /// 创建层数按钮
    /// </summary>
    public void CreateButton(BinHub shelfHub)
    {
        Debug.Log("库位创建层数按钮");

        //获取货架层数
        int shelfFloor = 1;
        //生成按钮
            GameObject row = GameObject.Instantiate(FloorButtonPre, ButtonGroup.transform.position, ButtonGroup.transform.rotation) as GameObject;
            row.name = "Button" + 1;
            row.transform.SetParent(ButtonGroup.transform);
            row.transform.localScale = Vector3.one;   //设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            FindChildGameObject(row.gameObject, "Button").transform.GetChild(0).GetComponent<Text>().text = 1 + " 层";
    }

    /// <summary>
    /// 创建每一层货物信息的列表
    /// </summary>
    /// <param name="shelfHub"></param>
    public void CreateTable(ShelfHub shelfHub)
    {
        //获取到当前鼠标点击按钮，即要查看的层数
        string[] arry = FloorButtonController.floorButtonText.Trim().Split('层');
        int nowFloor = int.Parse(arry[0]);

        //生成列表数据
        for (int i = 0; i < shelfHub.shelf.floor[nowFloor-1].material.Count; i++)
        {
            GameObject table = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo/Panel/Table");
            GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大

            //设置预设实例中的各个子物体的文本内容
            material material = shelfHub.shelf.floor[nowFloor-1].material[i];
            FindChildGameObject(row.gameObject, "Cell").GetComponent<Text>().text = material.name;
            FindChildGameObject(row.gameObject, "Cell1").GetComponent<Text>().text = material.code;
            FindChildGameObject(row.gameObject, "Cell2").GetComponent<Text>().text = material.count.ToString() + material.unit;
            FindChildGameObject(row.gameObject, "GoButton").transform.GetChild(1).GetComponent<Text>().text = (i+1).ToString();
        }
    }

    /// <summary>
    /// 创建每一层货物信息的列表
    /// </summary>
    /// <param name="shelfHub"></param>
    public void CreateTable(BinHub shelfHub)
    {
        //获取到当前鼠标点击按钮，即要查看的层数
        string[] arry = FloorButtonController.floorButtonText.Trim().Split('层');
        int nowFloor = int.Parse(arry[0]);

        //生成列表数据
        for (int i = 0; i < shelfHub.bin.material.Count; i++)
        {
            GameObject table = GameObject.Find("UI/ShelfInfoWindow/Shelf_Goods/FloorInfo/Panel/Table");
            GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大

            //设置预设实例中的各个子物体的文本内容
            material material = shelfHub.bin.material[i];
            FindChildGameObject(row.gameObject, "Cell").GetComponent<Text>().text = material.name;
            FindChildGameObject(row.gameObject, "Cell1").GetComponent<Text>().text = material.code;
            FindChildGameObject(row.gameObject, "Cell2").GetComponent<Text>().text = material.count.ToString() + material.unit;
            FindChildGameObject(row.gameObject, "GoButton").transform.GetChild(1).GetComponent<Text>().text = (i + 1).ToString();
        }
    }



    /// <summary>
    /// 获取子物体
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
