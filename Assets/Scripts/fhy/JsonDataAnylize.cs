using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
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
    public static JsonDataAnylize instance;
    public bool isDemoData;
    public GameObject DemoData;
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
        Debug.LogError("测试Json数据转换");
        //rootObject = AnylizeJsonData("{     \"warehouse\":     " + GetJsonData + "}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogError("测试Json数据转换");
            GetJsonData = "[";
            for (int i = 0; i < rootObject.warehouse.Length; i++)
            {
                GetJsonData += RootDataToJson(rootObject.warehouse[i]);
                if (i < rootObject.warehouse.Length - 1)
                    GetJsonData += ",";
            }
            GetJsonData += "]";
        }
        if (isDemoData)
        { rootObject = DemoData.GetComponent<DemoJsonData>().rootObject; }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.LogError("测试Json数据转换");
            rootObject = AnylizeJsonData("{     \"warehouse\":     " + GetJsonData + "}");
            for (int i = 0; i < rootObject.warehouse.Length; i++)
            {
                rootObject.warehouse[i].position = RestartPos(rootObject.warehouse[i].position);
                Debug.LogError("仓库" + i + "的位置是" + rootObject.warehouse[i].position[0] + "    " + rootObject.warehouse[i].position[1] + "    " + rootObject.warehouse[i].position[2] + "    " + rootObject.warehouse[i].position[3]);
                //for (int j = 0; j < rootObject.warehouse[i].barrier.Count; j++)
                //{
                //    rootObject.warehouse[i].barrier[j].position = RestartPos(rootObject.warehouse[i].barrier[j].position);

                //}
                //for (int j = 0; j < rootObject.warehouse[i].bin.Count; j++)
                //{
                //    rootObject.warehouse[i].bin[j].position = RestartPos(rootObject.warehouse[i].bin[j].position);
                //}
                //for (int j = 0; j < rootObject.warehouse[i].shelf.Count; j++)
                //{
                //    rootObject.warehouse[i].shelf[j].position = RestartPos(rootObject.warehouse[i].shelf[j].position);
                //}
            }
        }
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
    /// 传入
    /// </summary>
    /// <param name="jsonData"></param>
    public void JsonData(string jsonData)
    {
        if (jsonData == null || jsonData == "") return;
        GetJsonData = jsonData;
        rootObject = JsonUtility.FromJson<RootObject>("{     \"warehouse\":     " + GetJsonData + "}");
        
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
    public List<string> RestartPos(List<string> position)
    {
        Debug.Log("重置位置前  " + position[0] + "    " + position[1] + "    " + position[2] + "    " + position[3]);
        List<string> list = new List<string>();
        string[] temp = new string[4];
        for(int i = 0;i<4;i++)
        {
            if (int.Parse(position[i].Split(',')[0]) <= int.Parse(position[0].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[0].Split(',')[0])
                && int.Parse(position[i].Split(',')[0]) <= int.Parse(position[1].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[1].Split(',')[0])
            && int.Parse(position[i].Split(',')[0]) <= int.Parse(position[2].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[2].Split(',')[0])
             && int.Parse(position[i].Split(',')[0]) <= int.Parse(position[3].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[3].Split(',')[0]))
            {
                list.Add(position[i]);
                Debug.Log("重置后的第一个为" + position[i]);
            }



        }
        for(int i = 0;i<4;i++)
        {

            if (int.Parse(position[i].Split(',')[0]) >= int.Parse(position[0].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[0].Split(',')[0])
                && int.Parse(position[i].Split(',')[0]) >= int.Parse(position[1].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[1].Split(',')[0])
            && int.Parse(position[i].Split(',')[0]) >= int.Parse(position[2].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[2].Split(',')[0])
             && int.Parse(position[i].Split(',')[0]) >= int.Parse(position[3].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) <= int.Parse(position[3].Split(',')[0]))
            {
                list.Add(position[i]);
                Debug.Log("重置后的第2个为" + position[i]);

            }
        }


        for (int i = 0; i < 4; i++)
        {

            if (int.Parse(position[i].Split(',')[0]) >= int.Parse(position[0].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[0].Split(',')[0])
                && int.Parse(position[i].Split(',')[0]) >= int.Parse(position[1].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[1].Split(',')[0])
            && int.Parse(position[i].Split(',')[0]) >= int.Parse(position[2].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[2].Split(',')[0])
             && int.Parse(position[i].Split(',')[0]) >= int.Parse(position[3].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[3].Split(',')[0]))
            {
                list.Add(position[i]);
                Debug.Log("重置后的第3个为" + position[i]);

            }
        }
        for (int i = 0; i < 4; i++)
        {

            if (int.Parse(position[i].Split(',')[0]) <= int.Parse(position[0].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[0].Split(',')[0])
                && int.Parse(position[i].Split(',')[0]) <= int.Parse(position[1].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[1].Split(',')[0])
            && int.Parse(position[i].Split(',')[0]) <= int.Parse(position[0].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[2].Split(',')[0])
             && int.Parse(position[i].Split(',')[0]) <= int.Parse(position[0].Split(',')[0]) && int.Parse(position[0].Split(',')[0]) >= int.Parse(position[3].Split(',')[0]))
            {
                list.Add(position[i]);
                Debug.Log("重置后的第4个为" + position[i]);

            }
        }
        Debug.Log("重置位置后  " + list[0] + "    " + list[1] + "    " + list[2] + "    " + list[3]);
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
}

[System.Serializable]
public class Bin
{
    public string name;
    public List<string> position;
    public List<material> material;
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

public class Warehouse
{
    public string name;
    public List<string> position;
    public List<Door> door;
    public List<Barrier> barrier;
    public List<Bin> bin;
    public List<Shelf> shelf;
}
[System.Serializable]
public class RootObject
{
    public Warehouse[] warehouse;
}



