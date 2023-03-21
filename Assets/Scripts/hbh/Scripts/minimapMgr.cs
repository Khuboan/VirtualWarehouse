using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMgr : MonoBehaviour
{
    private Transform transWarehouses;
    Warehouse[] warehouse;
    private int wareCount;
    private WarehousesMgr[] warehousesMgrs;
    private float defaultPosY = -51;
    void Start()
    {
        transWarehouses = transform.Find("Warehouses");
        WarehouseMapCreat();
    }

    public void WarehouseMapCreat()
    {
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
        }

        warehousesMgrs = new WarehousesMgr[wareCount];
        for (int i = 0; i < warehousesMgrs.Length; i++)
        {
            warehousesMgrs[i] = gos[i].GetComponent<WarehousesMgr>();
            warehousesMgrs[i].GetComponent<RectTransform>().localPosition = new Vector3(0, defaultPosY + i * 100, 0);
        }
        defaultPosY = -51;
    }

    void Update()
    {
        
    }
}
