using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class minimapMgr : MonoBehaviour
{
    private Transform transWarehouses;
    Warehouse[] warehouse;
    private int wareCount;
    private WarehousesMgr[] warehousesMgrs;
    public static minimapMgr instance;
    private float[] housePosX;
    private float[] housePosY;

    public Button btnReset;
    public Button btnMiniMap;

    private float mapWidth;
    private float mapHeight;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        transWarehouses = transform.Find("Warehouses");
        WarehouseMapCreat();
        btnReset.onClick.AddListener(WarehouseMapCreat);
        btnReset.onClick.AddListener(WarehousesCreat);

        btnMiniMap.onClick.AddListener(WarehouseMapCreat);
        btnMiniMap.onClick.AddListener(WarehousesCreat);
    }

    public void WarehouseMapCreat()
    {
        if (JsonDataAnylize.instance.rootObject.warehouse.Length == 0) return;
        btnReset.onClick.RemoveAllListeners();
        btnReset.onClick.AddListener(WarehouseMapCreat);
        btnReset.onClick.AddListener(WarehousesCreat);

        btnMiniMap.onClick.RemoveAllListeners();
        btnMiniMap.onClick.AddListener(WarehouseMapCreat);
        btnMiniMap.onClick.AddListener(WarehousesCreat);
        for (int i = 0; i < transWarehouses.childCount; i++)
        {
            if (transWarehouses.GetChild(i).gameObject != null)
                Destroy(transWarehouses.GetChild(i).gameObject);
        }

        warehouse = JsonDataAnylize.instance.rootObject.warehouse;
        wareCount = warehouse.Length;
        GameObject[] gos = new GameObject[wareCount];

        List<float> mapX = new List<float>();//存储所有仓库位置X
        List<float> mapY = new List<float>();//存储所有仓库位置Y
        for (int i = 0; i < wareCount; i++)
        {
            for (int j = 0; j <4; j++)
            {
                mapX.Add(int.Parse(warehouse[i].position[j].Split(',')[0]));
                mapY.Add(int.Parse(warehouse[i].position[j].Split(',')[1]));
            }

            gos[i] = GameObject.Instantiate(Resources.Load<GameObject>("Warehouses/house"), transWarehouses);
            gos[i].name = "house" + i;
            gos[i].transform.Find("txtHouse").GetComponent<Text>().text = warehouse[i].name;
        }

        mapWidth = mapX.Max() - mapX.Min();
        mapHeight = mapY.Max() - mapY.Min();

        //小地图大小
        this.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(mapWidth / 10 + 200, mapHeight / 10 + 200);
        //this.transform.parent.GetComponent<RectTransform>().localPosition = new Vector3(-(mapWidth / 10 + 200) / 2, (mapHeight / 10 + 200) / 2);

        warehousesMgrs = new WarehousesMgr[wareCount];
        housePosX = new float[wareCount];
        housePosY = new float[wareCount];

        for (int i = 0; i < warehousesMgrs.Length; i++)
        {
            housePosX[i] = (float.Parse(warehouse[i].position[1].Split(',')[0]) + float.Parse(warehouse[i].position[0].Split(',')[0])) /2;
            housePosY[i] = (float.Parse(warehouse[i].position[2].Split(',')[1]) + float.Parse(warehouse[i].position[1].Split(',')[1])) /2;

            warehousesMgrs[i] = gos[i].GetComponent<WarehousesMgr>();
            warehousesMgrs[i].GetComponent<RectTransform>().localPosition =
                new Vector3((housePosX[i] / 10 - 100) - ((mapWidth / 10 / 200) - 1) * 100,
                (housePosY[i] / 10 - 100) - ((mapHeight / 10 / 200) - 1) * 100, 0);
        }

      
    }

    public void WarehousesCreat()
    {
        if (JsonDataAnylize.instance.rootObject.warehouse.Length == 0) return;
        for (int i = 0; i < warehousesMgrs.Length; i++)
        {
            warehousesMgrs[i].SetRectSize(i);
        }
    }
}
