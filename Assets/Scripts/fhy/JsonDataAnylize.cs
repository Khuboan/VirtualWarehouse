using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
// 访问数据的方法：
// rootObject.warehouse[i].name
// rootObject.warehouse[i].position[j]
// rootObject.warehouse[i].door[j].name
// rootObject.warehouse[i].door[j].position[k]
// rootObject.warehouse[i].barrier[j].name
// rootObject.warehouse[i].barrier[j].position[k]
// rootObject.warehouse[i].bin[j].name
// rootObject.warehouse[i].bin[j].position[k]
// rootObject.warehouse[i].bin[j].material[k].name
// rootObject.warehouse[i].bin[j].material[k].count
// rootObject.warehouse[i].bin[j].material[k].class
// rootObject.warehouse[i].bin[j].material[k].code
// rootObject.warehouse[i].bin[j].material[k].unit
// rootObject.warehouse[i].shelf[j].name
// rootObject.warehouse[i].shelf[j].position[k]
// rootObject.warehouse[i].shelf[j].floor[k].floor_num
// rootObject.warehouse[i].shelf[j].floor[k].material[l].name
// rootObject.warehouse[i].shelf[j].floor[k].material[l].count
// rootObject.warehouse[i].shelf[j].floor[k].material[l].class
// rootObject.warehouse[i].shelf[j].floor[k].material[l].code
// rootObject.warehouse[i].shelf[j].floor[k].material[l].unit





public class JsonDataAnylize : MonoBehaviour
{
    public string GetJsonData;
    public RootObject rootObject;
    public NewRootObject newRootObject;
    public static JsonDataAnylize instance;
    public bool isDemoData;
    public GameObject DemoData;
    public Floor Floors;
    public TemplateInfo template_info;
    public TitleTText GameTitle;
    public void UseDemoData(bool a)
    {
        isDemoData = a;
        if(!isDemoData)
        {
            JsonData(GetJsonData);
        }



    }
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        //Debug.LogErrorLogError("测试Json数据转换");
        //rootObject = AnylizeJsonData("{     \"warehouse\":     " + GetJsonData + "}");
    }
    public minimapMgr minimapMgr;
    // Update is called once per frame
    void Update()
    {
        if(newRootObject.warehouse.Length>0)
        {
            GameTitle.title_name.text = template_info.title_name;
            GameTitle.reset_button.text = template_info.reset_button;
            GameTitle.ShelfInfo_name_info.text = template_info.shelf_info_display.name_info;
            GameTitle.ShelfInfo_number_info.text = template_info.shelf_info_display.number_info;
            GameTitle.ShelfInfo_level_info.text = template_info.shelf_info_display.level_info;
            GameTitle.ShelfInfo_GoodsInfo_name.text = template_info.goods_info.name;
            GameTitle.ShelfInfo_GoodsInfo_initialization_info[0].text = template_info.goods_info.initialization_info[0];
            GameTitle.ShelfInfo_GoodsInfo_initialization_info[1].text = template_info.goods_info.initialization_info[1];
            GameTitle.ShelfInfo_GoodsInfo_initialization_info[2].text = template_info.goods_info.initialization_info[2];
            GameTitle.ShelfInfo_GoodsInfodisplay_name.text = template_info.goods_info_display.name;
            GameTitle.ShelfInfo_GoodsInfodisplay_close_button.text = template_info.goods_info_display.close_button;
            GameTitle.ShelfInfo_GoodsInfodisplay_name_info.text = template_info.goods_info_display.name_info;
            GameTitle.ShelfInfo_GoodsInfodisplay_num_info.text = template_info.goods_info_display.num_info;
            GameTitle.ShelfInfo_GoodsInfodisplay_type_info.text = template_info.goods_info_display.type_info;
            GameTitle.ShelfInfo_GoodsInfodisplay_number_info.text = template_info.goods_info_display.number_info;
            GameTitle.ShelfInfo_GoodsInfodisplay_shelf_info.text = template_info.goods_info_display.shelf_info;
            GameTitle.ShelfInfo_Event_info_display_name.text = template_info.event_info_display.name;
        }
        

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Debug.LogError("测试Json数据转换");
        //    GetJsonData = "[";
        //    for (int i = 0; i < newRootObject.warehouse.Length; i++)
        //    {
        //        GetJsonData += NewRootDataToJson(newRootObject.warehouse[i]);
        //        if (i < newRootObject.warehouse.Length - 1)
        //            GetJsonData += ",";
        //    }
        //    GetJsonData += "]";
        //}
        //if (isDemoData)
        //{ rootObject = DemoData.GetComponent<DemoJsonData>().rootObject; }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
        {
            JsonData(GetJsonData, "");
            CreateNewHouse.instance.GetHouseData();
            minimapMgr.WarehouseMapCreat();
            minimapMgr.WarehousesCreat();
        }
#endif

    }
    /// <summary>
    /// 通过调用该方法直接解析json数据
    /// </summary>
    /// <param name="jsonData"> 传入的json数据</param>
    /// <returns></returns>
    public static RootObject AnylizeJsonData(string jsonData)
    {
        Debug.Log(jsonData);
        //Debug.Log(JsonUtility.FromJson<RootObject>(jsonData).warehouses.Count);

        return JsonUtility.FromJson<RootObject>(jsonData);


    }

    /// <summary>
    /// 通过调用该方法直接解析json数据
    /// </summary>
    /// <param name="jsonData"> 传入的json数据</param>
    /// <returns></returns>
    public static NewRootObject NewAnylizeJsonData(string jsonData)
    {
        Debug.Log(jsonData);
        //Debug.Log(JsonUtility.FromJson<RootObject>(jsonData).warehouses.Count);

        return JsonUtility.FromJson<NewRootObject>(jsonData);


    }

    /// <summary>
    /// 传入
    /// </summary>
    /// <param name="jsonData"></param>
    public void JsonData(string jsonData)
    {
        if (jsonData == null || jsonData == "") return;
        GetJsonData = jsonData;
        rootObject = JsonUtility.FromJson<RootObject>("{     \"warehouse\":     " + GetJsonData + "}");
        for (int i = 0; i < rootObject.warehouse.Length; i++)
        {
            rootObject.warehouse[i].position = RestartPos(rootObject.warehouse[i].position);
            // Debug.LogError("仓库" + i + "的位置是" + rootObject.warehouse[i].position[0] + "    " + rootObject.warehouse[i].position[1] + "    " + rootObject.warehouse[i].position[2] + "    " + rootObject.warehouse[i].position[3]);
            for (int j = 0; j < rootObject.warehouse[i].barrier.Count; j++)
            {
                rootObject.warehouse[i].barrier[j].position = RestartPos(rootObject.warehouse[i].barrier[j].position);

            }
            for (int j = 0; j < rootObject.warehouse[i].bin.Count; j++)
            {
                rootObject.warehouse[i].bin[j].position = RestartPos(rootObject.warehouse[i].bin[j].position);
            }
            for (int j = 0; j < rootObject.warehouse[i].shelf.Count; j++)
            {
                rootObject.warehouse[i].shelf[j].position = RestartPos(rootObject.warehouse[i].shelf[j].position);
            }
        }
    }
    public List<Door> DoorRev(List<Door> doors)
    {
        List<Door> newdoor = new List<Door>();
        for(int i = 0;i<doors.Count;i++)
        {
            newdoor.Add(doors[doors.Count - 1 - i]);
        }
        return newdoor;
    }
    public void JsonData(string jsonData,string a)
    {
        if (jsonData == null || jsonData == "") return;
        GetJsonData = jsonData;
        //Debug.Log("{     \"warehouse\":     " + GetJsonData + "}");
        newRootObject = JsonUtility.FromJson<NewRootObject>("{     \"warehouse\":     " + GetJsonData + "}");
        template_info = newRootObject.warehouse[0].template_info;
        rootObject.warehouse = new Warehouse[newRootObject.warehouse.Length];
        for (int i = 0; i < rootObject.warehouse.Length; i++)
        {
            newRootObject.warehouse[i].door = DoorRev(newRootObject.warehouse[i].door);
            rootObject.warehouse[i] = new Warehouse();
            rootObject.warehouse[i].name = newRootObject.warehouse[i].name;
            rootObject.warehouse[i].position = newRootObject.warehouse[i].position;
            rootObject.warehouse[i].door = newRootObject.warehouse[i].door;
            rootObject.warehouse[i].barrier = newRootObject.warehouse[i].barrier;
            //rootObject.warehouse[i].bin = newRootObject.warehouse[i].bin;
            //rootObject.warehouse[i].shelf = newRootObject.warehouse[i].shelf;
            //rootObject.warehouse[i].shelf = new List<Shelf>()[newRootObject.warehouse[i].bin.Count];
            rootObject.warehouse[i].shelf = new List<Shelf>();
            rootObject.warehouse[i].bin = new List<Bin>();
            for (int j = 0; j < newRootObject.warehouse[i].bin.Count; j++)
            {
                if(newRootObject.warehouse[i].bin[j].floor!=0)
                {
                    Shelf newshelf = new Shelf();
                    newshelf.name = newRootObject.warehouse[i].bin[j].name;
                    newshelf.position = newRootObject.warehouse[i].bin[j].position;
                    newshelf.floor = new List<Floor>();
                    for (int k = 0; k < newRootObject.warehouse[i].bin[j].floor; k++)
                    {
                        Floor floor = new Floor();
                        floor.floor_num = k + 1;
                        floor.material = new List<material>();
                        newshelf.floor.Add(floor);
                    }
                    rootObject.warehouse[i].shelf.Add(newshelf);
                }
                else
                {
                    Bin newshelf = new Bin();
                    newshelf.name = newRootObject.warehouse[i].bin[j].name;
                    newshelf.position = newRootObject.warehouse[i].bin[j].position;
                    newshelf.material = new List<material>();
                    rootObject.warehouse[i].bin.Add(newshelf);
                }
                
            }
            rootObject.warehouse[i].scannerlog = newRootObject.warehouse[i].scannerlog;
            for (int j = 0; j < newRootObject.warehouse[i].material.Count; j++)
            {
                string Binnum = newRootObject.warehouse[i].material[j].bin.Split('-')[0];
                int Floornum = 1;
                if (newRootObject.warehouse[i].material[j].bin.Split('-').Length>1)
                {
                    Floornum = int.Parse(newRootObject.warehouse[i].material[j].bin.Split('-')[1]);

                    for (int k = 0; k < rootObject.warehouse[i].shelf.Count; k++)
                    {
                        if (Binnum == rootObject.warehouse[i].shelf[k].name)
                        {
                            rootObject.warehouse[i].shelf[k].floor[Floornum - 1].material.Add(newRootObject.warehouse[i].material[j]);
                        }
                    }
                }
                else
                {
                    Floornum = 1;//库位
                    for (int k = 0; k < rootObject.warehouse[i].bin.Count; k++)
                    {
                        if (Binnum == rootObject.warehouse[i].bin[k].name)
                        {
                            rootObject.warehouse[i].bin[k].material.Add(newRootObject.warehouse[i].material[j]);
                        }
                    }
                }

            }
           
        }
        for (int r = 0; r < JsonDataAnylize.instance.rootObject.warehouse.Length; r++)
        {
            for (int j = 0; j < JsonDataAnylize.instance.rootObject.warehouse[r].scannerlog.Count; j++)
            {
                ShowLog.instance. GetLog(template_info.event_info_display.title1 + " " + JsonDataAnylize.instance.rootObject.warehouse[r].scannerlog[j].address);    
                ShowLog.instance.GetLog(template_info.event_info_display.title2 + " " + JsonDataAnylize.instance.rootObject.warehouse[r].scannerlog[j].time);
                ShowLog.instance.GetLog(template_info.event_info_display.title3+" " + JsonDataAnylize.instance.rootObject.warehouse[r].scannerlog[j].code);
            }
        }
        //}
        for (int i = 0; i < rootObject.warehouse.Length; i++)
        {
            rootObject.warehouse[i].position = RestartPos(rootObject.warehouse[i].position);
            // Debug.LogError("仓库" + i + "的位置是" + rootObject.warehouse[i].position[0] + "    " + rootObject.warehouse[i].position[1] + "    " + rootObject.warehouse[i].position[2] + "    " + rootObject.warehouse[i].position[3]);
            for (int j = 0; j < rootObject.warehouse[i].barrier.Count; j++)
            {
                rootObject.warehouse[i].barrier[j].position = RestartPos(rootObject.warehouse[i].barrier[j].position);

            }
            for (int j = 0; j < rootObject.warehouse[i].bin.Count; j++)
            {
                rootObject.warehouse[i].bin[j].position = RestartPos(rootObject.warehouse[i].bin[j].position);
            }
            for (int j = 0; j < rootObject.warehouse[i].shelf.Count; j++)
            {
                rootObject.warehouse[i].shelf[j].position = RestartPos(rootObject.warehouse[i].shelf[j].position);
            }
        }
    }
    /// <summary>
    /// 将数据转换为json
    /// </summary>
    /// <param name="rootObject"></param>
    /// <returns></returns>
    public static string RootDataToJson(Warehouse warehouse)
    {
        return JsonUtility.ToJson(warehouse);
    }
    public static string NewRootDataToJson(NewWarehouse warehouse)
    {
        return JsonUtility.ToJson(warehouse);
    }
    public List<Vector2> pos;
    public List<string> RestartPos(List<string> position)
    {
        //Debug.Log("重置位置前  " + position[0] + "    " + position[1] + "    " + position[2] + "    " + position[3]);
        List<string> list = new List<string>();
        List<Vector2> postemp = new List<Vector2>();
        List<Vector2> postemp2 = new List<Vector2>();
        string[] temp = new string[4];
        for (int i = 0;i<4;i++)
        {
            postemp.Add(new Vector2(int.Parse(position[i].Split(',')[0]), int.Parse(position[i].Split(',')[1])));
        }
        for(int i = 0;i<4;i++)
        {
             if(postemp[i].x<= postemp[0].x && postemp[i].x <= postemp[1].x && postemp[i].x <= postemp[2].x && postemp[i].x <= postemp[3].x
                && postemp[i].y <= postemp[0].y && postemp[i].y <= postemp[1].y && postemp[i].y <= postemp[2].y && postemp[i].y <= postemp[3].y
                )
            {
                postemp2.Add(postemp[i]);
            }
        }
        for(int i = 0;i<4;i++)
        {
            if(postemp[i].y == postemp2[0].y && postemp[i].x != postemp2[0].x)
            {
                postemp2.Add(postemp[i]);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (postemp[i].y != postemp2[0].y && postemp[i].x != postemp2[0].x)
            {
                postemp2.Add(postemp[i]);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (postemp[i].y != postemp2[0].y && postemp[i].x == postemp2[0].x)
            {
                postemp2.Add(postemp[i]);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            list.Add(postemp2[i].x + "," + postemp2[i].y);
            //Debug.Log(list[i]);
        }       
        //Debug.Log("重置位置后  " + list[0] + "    " + list[1] + "    " + list[2] + "    " + list[3]);
        return list;
    }
}
public class PosXY
{

}

[System.Serializable]
public class Door
{
    public string name;
    public List<string> position;
}

[System.Serializable]
public class Barrier
{
    public string name;
    public List<string> position;
}

[System.Serializable]
public class material
{
    public string name;
    public int count;
    public string @class;
    public string code;
    public string unit;
    public string bin;
}

[System.Serializable]
public class Bin
{
    public string name;
    public int floor;
    public List<string> position;
    public List<material> material;
}
[System.Serializable]
public class NewBin
{
    public string name;
    public int floor;
    public List<string> position;
}

[System.Serializable]
public class Floor
{
    public int floor_num;
    public List<material> material;
}
[System.Serializable]
public class Shelf
{
    public string name;
    public List<string> position;
    public List<Floor> floor;
}
[System.Serializable]
public class NewShelf
{
    public string name;
    public List<string> position;
}
[System.Serializable]

public class Warehouse
{
    public string name;
    public List<string> position;
    public List<Door> door;
    public List<Barrier> barrier;
    public List<Bin> bin;
    public List<Shelf> shelf;
    public List<scannerlog> scannerlog;
    
}
[System.Serializable]
public class RootObject
{
    public Warehouse[] warehouse;
}
[System.Serializable]
public class NewRootObject
{
    public NewWarehouse[] warehouse;
}
[System.Serializable]

public class NewWarehouse
{
    public string name;
    public List<string> position;
    public List<Door> door;
    public List<Barrier> barrier;
    public List<NewBin> bin;
    //public List<Shelf> shelf;
    public List<material> material;
    public List<scannerlog> scannerlog;
    public TemplateInfo template_info;
}
[System.Serializable]
public class scannerlog
{
    public string address;
    public string time;
    public string code;
}
[System.Serializable]
public class ShelfInfo
{
    public string name;
    public string name_info;
    public string number_info;
    public string level_info;
}

[System.Serializable]
public class GoodsInfo
{
    public string name;
    public string unit;
    public List<string> initialization_info;
    public int unit_determine;
}

[System.Serializable]
public class GoodsInfoDisplay
{
    public string name;
    public string close_button;
    public string name_info;
    public string num_info;
    public string type_info;
    public string number_info;
    public string shelf_info;
}

[System.Serializable]
public class EventInfoDisplay
{
    public string name;
    public string title1;
    public string title2;
    public string title3;
}

[System.Serializable]
public class TemplateInfo
{
    public string title_name;
    public string reset_button;
    public ShelfInfo shelf_info_display;
    public GoodsInfo goods_info;
    public GoodsInfoDisplay goods_info_display;
    public EventInfoDisplay event_info_display;
}
[System.Serializable]
public class TitleTText
{
    public Text title_name;
    public Text reset_button;
    public Text ShelfInfo_name;
    public Text ShelfInfo_name_info;
    public Text ShelfInfo_number_info;
    public Text ShelfInfo_level_info;
    public Text ShelfInfo_GoodsInfo_name;
    public Text[] ShelfInfo_GoodsInfo_initialization_info;
    public Text ShelfInfo_GoodsInfodisplay_name;
    public Text ShelfInfo_GoodsInfodisplay_close_button;
    public Text ShelfInfo_GoodsInfodisplay_name_info;
    public Text ShelfInfo_GoodsInfodisplay_num_info;
    public Text ShelfInfo_GoodsInfodisplay_type_info;
    public Text ShelfInfo_GoodsInfodisplay_number_info;
    public Text ShelfInfo_GoodsInfodisplay_shelf_info;
    public Text ShelfInfo_Event_info_display_name;
}