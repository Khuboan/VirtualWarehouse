using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimapMgr : MonoBehaviour
{
    private Transform transWarehouses;
    Warehouse[] warehouse;
    private int wareCount;
    private WarehousesMgr[] warehousesMgrs;

    private float[] housePosX;
    private float[] housePosY;

    public Button btnReset;
    public Button btnMiniMap;
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

        for (int i = 0; i < wareCount; i++)
        {
            gos[i] = GameObject.Instantiate(Resources.Load<GameObject>("Warehouses/house"), transWarehouses);
            gos[i].name = "house" + i;
            gos[i].transform.Find("txtHouse").GetComponent<Text>().text = warehouse[i].name;
        }

        warehousesMgrs = new WarehousesMgr[wareCount];
        housePosX = new float[wareCount];
        housePosY = new float[wareCount];

        for (int i = 0; i < warehousesMgrs.Length; i++)
        {
            housePosX[i] = (float.Parse(warehouse[i].position[1].Split(',')[0]) + float.Parse(warehouse[i].position[0].Split(',')[0])) /2;
            housePosY[i] = (float.Parse(warehouse[i].position[2].Split(',')[1]) + float.Parse(warehouse[i].position[1].Split(',')[1])) /2;

            warehousesMgrs[i] = gos[i].GetComponent<WarehousesMgr>();
            warehousesMgrs[i].GetComponent<RectTransform>().localPosition = new Vector3(housePosX[i]/10-100, housePosY[i] / 10 - 100, 0);
        }
    }

    public void WarehousesCreat()
    {
        for (int i = 0; i < warehousesMgrs.Length; i++)
        {
            warehousesMgrs[i].SetRectSize(i);
        }
    }
}
