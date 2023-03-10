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
        // ����ÿ��ģ��Ŀ��
        moduleWidth = (width - moduleLength) / 2.0f;

        // �������ģ��
        GameObject left = Instantiate(leftModule, transform);
        left.transform.localPosition = Vector3.zero;

        // �����м�ģ��
        GameObject middle = Instantiate(middleModule, transform);
        middle.transform.localPosition = new Vector3(moduleWidth, 0, 0);
        middle.transform.localScale = new Vector3(moduleWidth, 1, length);

        // �����Ҳ�ģ��
        GameObject right = Instantiate(rightModule, transform);
        right.transform.localPosition = new Vector3(moduleWidth + moduleLength, 0, 0);

        // �����ֿ�Ĵ�С
        transform.localScale = new Vector3(width, 1, length);
    }
}
