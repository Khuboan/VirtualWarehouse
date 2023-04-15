using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialInfoGet : MonoBehaviour
{
    public static MaterialInfoGet materialInfoGet;

    public Text MaterialName, MaterialIndex, MaterialClass, MaterialCount, MaterialBin;
    public GameObject ShelfWin;
    public GameObject MaterialWin;

    private void Start()
    {
        ShelfWin.SetActive(true);
        MaterialWin.SetActive(true);
    }
    private void Awake()
    {
        materialInfoGet = this;
    }
    public void GetInfo(int nowFloor,int nowMaterial)
    {
        //��ʾ�������֣���ţ����࣬����
        material targetMaterial = SelfInfoGet.shelfHub.shelf.floor[nowFloor - 1].material[nowMaterial-1];
        MaterialName.text = targetMaterial.name.ToString();
        MaterialIndex.text = targetMaterial.code.ToString();
        MaterialClass.text = targetMaterial.@class.ToString();
        MaterialCount.text = targetMaterial.count.ToString() + targetMaterial.unit.ToString();
        MaterialBin.text = targetMaterial.bin.ToString();
    }
    public void GetInfo( int nowMaterial)
    {
        //��ʾ�������֣���ţ����࣬����
        SelfInfoGet.binHub = CameraController.instance.targetPoint[CameraController.instance.camposIndex].parent.GetComponent<BinHub>();
        Debug.Log(SelfInfoGet.binHub.gameObject.name);
        Debug.Log("����Ϊ" + SelfInfoGet.binHub.bin.material.Count + "  ��ǰѡ�� " + (nowMaterial - 1));

        material targetMaterial = SelfInfoGet.binHub.bin.material[nowMaterial - 1];
        MaterialName.text = targetMaterial.name.ToString();
        MaterialIndex.text = targetMaterial.code.ToString();
        MaterialClass.text = targetMaterial.@class.ToString();
        MaterialCount.text = targetMaterial.count.ToString() + targetMaterial.unit.ToString();
        MaterialBin.text = targetMaterial.bin.ToString();
    }
}