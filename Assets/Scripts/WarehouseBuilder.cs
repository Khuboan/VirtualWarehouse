using UnityEngine;
using System.Collections.Generic;

public class WarehouseBuilder : MonoBehaviour
{
    public GameObject leftModule;   // 左侧模块
    public GameObject centerModule; // 中间模块
    public GameObject rightModule;  // 右侧模块

    private float moduleWidth;  // 模块宽度
    private float moduleHeight; // 模块高度

    // 根据长宽计算需要使用的模块数量和总宽度
    private void CalculateModules(float length, float width, out int leftCount, out int centerCount, out int rightCount, out float totalWidth)
    {
        leftCount = Mathf.FloorToInt((width - moduleWidth) / moduleWidth) + 1;
        centerCount = Mathf.FloorToInt(length / moduleWidth);
        rightCount = Mathf.CeilToInt((width - moduleWidth * (leftCount + centerCount)) / moduleWidth);
        totalWidth = leftCount * moduleWidth + centerCount * moduleWidth + rightCount * moduleWidth;
    }

    // 根据模块类型和数量创建对应的模块列表
    private List<GameObject> CreateModuleList(GameObject module, int count)
    {
        List<GameObject> moduleList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            moduleList.Add(Instantiate(module));
        }
        return moduleList;
    }

    // 根据模块列表和位置信息将模块拼接成仓库
    private void BuildWarehouse(List<GameObject> leftModules, List<GameObject> centerModules, List<GameObject> rightModules, float totalWidth)
    {
        float x = -totalWidth / 2 + moduleWidth / 2;
        float y = 0;
        float z = 0;

        // 拼接左侧模块
        foreach (GameObject module in leftModules)
        {
            module.transform.position = new Vector3(x, y, z);
            x += moduleWidth;
        }

        // 拼接中间模块
        foreach (GameObject module in centerModules)
        {
            module.transform.position = new Vector3(x, y, z);
            x += moduleWidth;
        }

        // 拼接右侧模块
        foreach (GameObject module in rightModules)
        {
            module.transform.position = new Vector3(x, y, z);
            x += moduleWidth;
        }
    }

    // 处理用户输入，计算需要使用的模块数量和总宽度，并创建并拼接模块
    public void BuildWarehouseFromInput(float length, float width)
    {
        int leftCount, centerCount, rightCount;
        float totalWidth;
        CalculateModules(length, width, out leftCount, out centerCount, out rightCount, out totalWidth);

        List<GameObject> leftModules = CreateModuleList(leftModule, leftCount);
        List<GameObject> centerModules = CreateModuleList(centerModule, centerCount);
        List<GameObject> rightModules = CreateModuleList(rightModule, rightCount);

        BuildWarehouse(leftModules, centerModules, rightModules, totalWidth);
        Debug.Log("Builder");
    }

    // 在场景中绘制仓库区域，
}
