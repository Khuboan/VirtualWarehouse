using UnityEngine;
using System.Collections.Generic;

public class WarehouseBuilder : MonoBehaviour
{
    public GameObject leftModule;   // ���ģ��
    public GameObject centerModule; // �м�ģ��
    public GameObject rightModule;  // �Ҳ�ģ��

    private float moduleWidth;  // ģ����
    private float moduleHeight; // ģ��߶�

    // ���ݳ��������Ҫʹ�õ�ģ���������ܿ��
    private void CalculateModules(float length, float width, out int leftCount, out int centerCount, out int rightCount, out float totalWidth)
    {
        leftCount = Mathf.FloorToInt((width - moduleWidth) / moduleWidth) + 1;
        centerCount = Mathf.FloorToInt(length / moduleWidth);
        rightCount = Mathf.CeilToInt((width - moduleWidth * (leftCount + centerCount)) / moduleWidth);
        totalWidth = leftCount * moduleWidth + centerCount * moduleWidth + rightCount * moduleWidth;
    }

    // ����ģ�����ͺ�����������Ӧ��ģ���б�
    private List<GameObject> CreateModuleList(GameObject module, int count)
    {
        List<GameObject> moduleList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            moduleList.Add(Instantiate(module));
        }
        return moduleList;
    }

    // ����ģ���б��λ����Ϣ��ģ��ƴ�ӳɲֿ�
    private void BuildWarehouse(List<GameObject> leftModules, List<GameObject> centerModules, List<GameObject> rightModules, float totalWidth)
    {
        float x = -totalWidth / 2 + moduleWidth / 2;
        float y = 0;
        float z = 0;

        // ƴ�����ģ��
        foreach (GameObject module in leftModules)
        {
            module.transform.position = new Vector3(x, y, z);
            x += moduleWidth;
        }

        // ƴ���м�ģ��
        foreach (GameObject module in centerModules)
        {
            module.transform.position = new Vector3(x, y, z);
            x += moduleWidth;
        }

        // ƴ���Ҳ�ģ��
        foreach (GameObject module in rightModules)
        {
            module.transform.position = new Vector3(x, y, z);
            x += moduleWidth;
        }
    }

    // �����û����룬������Ҫʹ�õ�ģ���������ܿ�ȣ���������ƴ��ģ��
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

    // �ڳ����л��Ʋֿ�����
}
