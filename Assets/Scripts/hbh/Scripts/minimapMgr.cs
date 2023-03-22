using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapMgr : MonoBehaviour
{
    private Transform transWarehouses;
    Warehouse[] warehouse;
    private int wareCount;
    private WarehousesMgr[] warehousesMgrs;
    private float defaultPosY = -51;

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
            gos[i] = GameObject.Instantiate(Resources.Load<GameObject>("Warehouses/rectline"), transWarehouses);
            gos[i].name = "house" + i;
        }

        warehousesMgrs = new WarehousesMgr[wareCount];
        for (int i = 0; i < warehousesMgrs.Length; i++)
        {
            warehousesMgrs[i] = gos[i].GetComponent<WarehousesMgr>();
            warehousesMgrs[i].GetComponent<RectTransform>().localPosition = new Vector3(0, defaultPosY + i * 100, 0);
        }
        defaultPosY = -51;
    }

    public void WarehousesCreat()
    {
        for (int i = 0; i < warehousesMgrs.Length; i++)
        {
            warehousesMgrs[i].SetRectSize(i);
        }
    }
}
