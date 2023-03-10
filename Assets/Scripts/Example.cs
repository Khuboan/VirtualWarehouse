using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Example : MonoBehaviour
{
    public WarehouseGenerator warehouseGenerator;
    public WarehouseBuilder warehouseBuilder;

    private void Start()
    {
        warehouseGenerator.GenerateWarehouse(5.0f, 10.0f);
        warehouseBuilder.BuildWarehouseFromInput(5.0f, 10.0f);
        Debug.Log("Example");
    }
}
