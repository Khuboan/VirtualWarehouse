using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialInfoGet : MonoBehaviour
{
    public static MaterialInfoGet materialInfoGet;

    public Text MaterialName, MaterialIndex, MaterialClass, MaterialCount;
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
        //显示货物名字，编号，种类，数量
        material targetMaterial = SelfInfoGet.shelfHub.shelf.floor[nowFloor - 1].material[nowMaterial-1];
        MaterialName.text = targetMaterial.name.ToString();
        MaterialIndex.text = targetMaterial.code.ToString();
        MaterialClass.text = targetMaterial.@class.ToString();
        MaterialCount.text = targetMaterial.count.ToString() + targetMaterial.unit.ToString();
    }
}