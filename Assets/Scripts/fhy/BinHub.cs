using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class BinHub : MonoBehaviour
{
    public Transform CenterPos;
    public int CenterPosNum;
    public bool isY;
    public bool isL;
    public List<BinObject> binObjects = new List<BinObject>();
    public List<BinObject> binObjects2 = new List<BinObject>();
    public List<GameObject> gameObjects = new List<GameObject>();
    public List<GameObject> gameObjects2 = new List<GameObject>();
    public Transform[] CameraDir;
    public ColliderTest[] colliderTests;
    public Bin bin;
    public BinDetail binDetail = new BinDetail();
    public Text BinName;
    public GameObject binmodel,binmodel2;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i< bin.material.Count; i++)
        {
            gameObjects[i].SetActive(true);
            binObjects[i].material = bin.material[i];
            gameObjects2[i].SetActive(true);
            binObjects2[i].material = bin.material[i];
            binObjects[i].CamPosIndex = CenterPosNum;
            binObjects2[i].CamPosIndex = CenterPosNum;
            binObjects[i].Num = i;
            binObjects2[i].Num = i;
        }
        float PosX1 = float.Parse(bin.position[0].Split(',')[0]) / 100;
        float PosY1 = float.Parse(bin.position[0].Split(',')[1]) / 100;
        float PosX2 = float.Parse(bin.position[1].Split(',')[0]) / 100;
        float PosY2 = float.Parse(bin.position[1].Split(',')[1]) / 100;
        float PosX3 = float.Parse(bin.position[2].Split(',')[0]) / 100;
        float PosY3 = float.Parse(bin.position[2].Split(',')[1]) / 100;
        float PosX4 = float.Parse(bin.position[3].Split(',')[0]) / 100;
        float PosY4 = float.Parse(bin.position[3].Split(',')[1]) / 100;
        binDetail.scale = new Vector3(PosX2 - PosX1, 1, PosY3 - PosY1);
        if (PosY4 - PosY1 >= PosX2 - PosX1)//竖向排列
        {
            transform.GetChild(0).localScale = binDetail.scale;
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else//横向排列
        {
            isY = true;
            transform.GetChild(1).localScale = binDetail.scale;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (!isY)
        {
            if (colliderTests[0].isObstacle == true)
            {
                CenterPos.position = CameraDir[1].position;
                isL = true;
            }
            else
            {
                CenterPos.position = CameraDir[0].position;
                isL = false;
            }
            }
        else
        {
            if (colliderTests[2].isObstacle == true)
            {
                CenterPos.position = CameraDir[3].position;
                isL = true;
            }
            else
            {
                CenterPos.position = CameraDir[2].position;
                isL = false;
            }
        }
        BinName.text = bin.name;
        }
        // Update is called once per frame
        void Update()
    {
        if (CameraController.instance.targetPoint[CameraController.instance.camposIndex] == CenterPos)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                binmodel.GetComponent<Outline>().enabled = true;
                binmodel2.GetComponent<Outline>().enabled = true;
            }

        }
        else
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                binmodel.GetComponent<Outline>().enabled = false;
                binmodel2.GetComponent<Outline>().enabled = false;
            }
        }
        if (!isY)
        {
            if (colliderTests[0].isObstacle == true)
            {
                BinName.gameObject.transform.parent.localEulerAngles = new Vector3(0, 180, 0);
                BinName.gameObject.transform.parent.position = binmodel.transform.position + new Vector3(0, ((float)bin.material.Count * 0.5f) * 0.8f, 0);
                CenterPos.position = CameraDir[1].position;
                isL = true;
            }
            else
            {
                BinName.gameObject.transform.parent.localEulerAngles = new Vector3(0, 0, 0);
                BinName.gameObject.transform.parent.position = binmodel2.transform.position + new Vector3(0, ((float)bin.material.Count * 0.5f) * 0.8f, 0);
                CenterPos.position = CameraDir[0].position;
                isL = false;
            }
        }
        else
        {
            if (colliderTests[2].isObstacle == true)
            {
                BinName.gameObject.transform.parent.localEulerAngles = new Vector3(0, 90, 0);
                BinName.gameObject.transform.parent.position = binmodel.transform.position + new Vector3(0, ((float)bin.material.Count * 0.5f) * 0.8f, 0);
                CenterPos.position = CameraDir[3].position;
                isL = true;
            }
            else
            {
                BinName.gameObject.transform.parent.localEulerAngles = new Vector3(0, -90, 0);
                BinName.gameObject.transform.parent.position = binmodel2.transform.position + new Vector3(0, ((float)bin.material.Count * 0.5f) * 0.8f, 0);
                CenterPos.position = CameraDir[2].position;
                isL = false;
            }
        }
    }
}
