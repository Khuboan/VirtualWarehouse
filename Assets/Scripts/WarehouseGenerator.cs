using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseGenerator : MonoBehaviour
{
    public GameObject leftModule;
    public GameObject middleModule;
    public GameObject rightModule;

    private float moduleWidth;
    private float moduleLength;

    public void GenerateWarehouse(float width, float length)
    {
        // 计算每个模块的宽度
        moduleWidth = (width - moduleLength) / 2.0f;

        // 生成左侧模块
        GameObject left = Instantiate(leftModule, transform);
        left.transform.localPosition = Vector3.zero;

        // 生成中间模块
        GameObject middle = Instantiate(middleModule, transform);
        middle.transform.localPosition = new Vector3(moduleWidth, 0, 0);
        middle.transform.localScale = new Vector3(moduleWidth, 1, length);

        // 生成右侧模块
        GameObject right = Instantiate(rightModule, transform);
        right.transform.localPosition = new Vector3(moduleWidth + moduleLength, 0, 0);

        // 调整仓库的大小
        transform.localScale = new Vector3(width, 1, length);
    }
}
