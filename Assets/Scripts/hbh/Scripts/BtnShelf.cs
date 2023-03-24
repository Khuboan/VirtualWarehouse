using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShelf : MonoBehaviour
{
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }
    public void ShelfPosCam()
    {
        int index = ToolExtractData(this.name);

        int houseIndex = ToolExtractData(this.transform.parent.parent.name);

        GameObject HouseHub = GameObject.Find("HouseHub");
        Transform shelfPosCam = HouseHub.transform.Find("House" + houseIndex).Find("Shelf"+index).GetComponent<ShelfHub>().CenterPos;
        Debug.Log(shelfPosCam.position);
        for (int i = 0; i < CameraController.instance.targetPoint.Count; i++)
        {
            if(shelfPosCam== CameraController.instance.targetPoint[i])
            {
                CameraController.instance.camposIndex = i;
                CameraController.instance.isMoveDone = false;
                CameraController.instance.CameraMoveTime = 0;
                CameraController.instance.campos = CameraController.instance.transform.position;
            }
        }
    }

    public void BinPosCam()
    {
        int index = ToolExtractData(this.name);

        int houseIndex = ToolExtractData(this.transform.parent.parent.name);

        GameObject HouseHub = GameObject.Find("HouseHub");
        Transform shelfPosCam = HouseHub.transform.Find("House" + houseIndex).Find("Bin" + index).GetComponent<BinHub>().CenterPos;
        Debug.Log(shelfPosCam.position);
        for (int i = 0; i < CameraController.instance.targetPoint.Count; i++)
        {
            if (shelfPosCam == CameraController.instance.targetPoint[i])
            {
                CameraController.instance.camposIndex = i;
                CameraController.instance.isMoveDone = false;
                CameraController.instance.CameraMoveTime = 0;
                CameraController.instance.campos = CameraController.instance.transform.position;
            }
        }
    }    


    public static int ToolExtractData(string str)
    {
        string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
        return int.Parse(result);
    }
}
