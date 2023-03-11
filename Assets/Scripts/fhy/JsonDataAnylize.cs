using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogError("测试Json数据转换");
            GetJsonData = "[";
                for(int i = 0;i< rootObject.warehouse.Length;i++)
                {
                    GetJsonData += RootDataToJson(rootObject.warehouse[i]);
                if (i < rootObject.warehouse.Length - 1)
                    GetJsonData += ",";
                }
            GetJsonData +="]";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.LogError("测试Json数据转换");
            rootObject = AnylizeJsonData("{     \"warehouse\":     " +GetJsonData + "}");
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
    /// 将数据转换为json
    /// </summary>
    /// <param name="rootObject"></param>
    /// <returns></returns>
    public static string RootDataToJson(Warehouse warehouse)
    {
        return JsonUtility.ToJson(warehouse);
    }
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



