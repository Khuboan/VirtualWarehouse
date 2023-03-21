using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehousesMgr : MonoBehaviour
{
    private Image[] doors;
    private Image[] barriers;
    private Image[] bins;
    private Image[] shelfs;

    private float rectHeight;
    private float rectWidth;

    Warehouse[] warehouse;

    public Transform transDoors;
    public Transform transBarrier;
    public Transform transBins;
    public Transform transShelfs;


    void Start()
    {
        transDoors = this.transform.Find("Doors").GetComponent<Transform>();
        transBarrier = this.transform.Find("Barriers").GetComponent<Transform>();
        transShelfs = this.transform.Find("Shelfs").GetComponent<Transform>();
        transBins = this.transform.Find("Bins").GetComponent<Transform>();
        Debug.Log(transDoors);
    }

    public void SetRectSize(int index)
    {
        warehouse = JsonDataAnylize.instance.rootObject.warehouse;
        rectWidth = (float.Parse(warehouse[index].position[1].Split(',')[0]) - float.Parse(warehouse[index].position[0].Split(',')[0])) / 10;
        rectHeight = (float.Parse(warehouse[index].position[2].Split(',')[1]) - float.Parse(warehouse[index].position[1].Split(',')[1])) / 10;

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
        SetPosDoor(index);
        SetPosBarrier(index);
        SetPosBin(index);
        SetPosShelfs(index);
    }

    #region Warehouses
    public void SetPosDoor(int index)
    {
        for (int i = 0; i < transDoors.childCount; i++)
        {
            if(transDoors.GetChild(i).gameObject!=null)
            Destroy(transDoors.GetChild(i).gameObject);
        }
        int initCount = warehouse[index].door.Count;

        doors = new Image[initCount];
        float[] PosX, PosY, sizeX, sizeY;

        PosX = new float[doors.Length];
        PosY = new float[doors.Length];
        sizeX = new float[doors.Length];
        sizeY = new float[doors.Length];
        for (int i = 0; i < initCount; i++)
        {
            GameObject[] gos=new GameObject[initCount];
            gos[i] = GameObject.Instantiate(Resources.Load<GameObject>("Warehouses/door"), transDoors);
            doors[i] = gos[i].GetComponent<Image>();
            PosX[i] = (float.Parse(warehouse[index].door[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].door[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            sizeX[i] = (float.Parse(warehouse[index].door[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].door[i].position[0].Split(',')[0])) / 10;
            doors[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i], 7f);
            doors[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], -rectHeight / 2, 0);
        }

        //fDoor.GetComponent<RectTransform>().localPosition = new Vector3(0, rectHeight / 2, 0); //ÉÏ
        //lDoor.GetComponent<RectTransform>().localPosition = new Vector3(-rectWidth / 2, 0, 0); //×ó
        //rDoor.GetComponent<RectTransform>().localPosition = new Vector3(rectWidth / 2, 0, 0); //ÓÒ
    }

    public void SetPosBarrier(int index)
    {
        for (int i = 0; i < transBarrier.childCount; i++)
        {
            if (transBarrier.GetChild(i).gameObject != null)
                Destroy(transBarrier.GetChild(i).gameObject);
        }
        int initCount = warehouse[index].barrier.Count;
        barriers = new Image[initCount];
        float[] PosX, PosY, sizeX, sizeY;

        PosX = new float[barriers.Length];
        PosY = new float[barriers.Length];
        sizeX = new float[barriers.Length];
        sizeY = new float[barriers.Length];
        for (int i = 0; i < initCount; i++)
        {
            GameObject[] gos = new GameObject[initCount];
            gos[i] = GameObject.Instantiate(Resources.Load<GameObject>("Warehouses/barrier"), transBarrier);
            barriers[i] = gos[i].GetComponent<Image>();
            PosX[i] = (float.Parse(warehouse[index].barrier[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].barrier[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[index].barrier[i].position[1].Split(',')[1]) + float.Parse(warehouse[index].barrier[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            sizeX[i] = (float.Parse(warehouse[index].barrier[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].barrier[i].position[0].Split(',')[0])) / 10;
            sizeY[i] = (float.Parse(warehouse[index].barrier[i].position[2].Split(',')[1]) - float.Parse(warehouse[index].barrier[i].position[1].Split(',')[1])) / 10;
            barriers[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i], sizeY[i]);
            barriers[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

    public void SetPosBin(int index)
    {
        for (int i = 0; i < transBins.childCount; i++)
        {
            if (transBins.GetChild(i).gameObject != null)
                Destroy(transBins.GetChild(i).gameObject);
        }
        int initCount = warehouse[index].bin.Count;
        bins = new Image[initCount];
        float[] PosX, PosY, sizeX, sizeY;

        PosX = new float[bins.Length];
        PosY = new float[bins.Length];
        sizeX = new float[bins.Length];
        sizeY = new float[bins.Length];
        for (int i = 0; i < initCount; i++)
        {
            GameObject[] gos = new GameObject[initCount];
            gos[i] = GameObject.Instantiate(Resources.Load<GameObject>("Warehouses/bin"), transBins);
            bins[i] = gos[i].GetComponent<Image>();
            PosX[i] = (float.Parse(warehouse[index].bin[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].bin[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[index].bin[i].position[1].Split(',')[1]) + float.Parse(warehouse[index].bin[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            sizeX[i] = (float.Parse(warehouse[index].bin[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].bin[i].position[0].Split(',')[0])) / 10;
            sizeY[i] = (float.Parse(warehouse[index].bin[i].position[2].Split(',')[1]) - float.Parse(warehouse[index].bin[i].position[1].Split(',')[1])) / 10;
            bins[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i], sizeY[i]);
            bins[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

    public void SetPosShelfs(int index)
    {
        for (int i = 0; i < transShelfs.childCount; i++)
        {
            if (transShelfs.GetChild(i).gameObject != null)
                Destroy(transShelfs.GetChild(i).gameObject);
        }
        int initCount = warehouse[index].shelf.Count;
        shelfs = new Image[initCount];
        float[] PosX, PosY, sizeX, sizeY;

        PosX = new float[shelfs.Length];
        PosY = new float[shelfs.Length];
        sizeX = new float[shelfs.Length];
        sizeY = new float[shelfs.Length];
        for (int i = 0; i < initCount; i++)
        {
            GameObject[] gos = new GameObject[initCount];
            gos[i] = GameObject.Instantiate(Resources.Load<GameObject>("Warehouses/shelf"), transShelfs);
            shelfs[i] = gos[i].GetComponent<Image>();
            PosX[i] = (float.Parse(warehouse[index].shelf[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].shelf[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[index].shelf[i].position[1].Split(',')[1]) + float.Parse(warehouse[index].shelf[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            sizeX[i] = (float.Parse(warehouse[index].shelf[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].shelf[i].position[0].Split(',')[0])) / 10;
            sizeY[i] = (float.Parse(warehouse[index].shelf[i].position[2].Split(',')[1]) - float.Parse(warehouse[index].shelf[i].position[1].Split(',')[1])) / 10;
            shelfs[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i], sizeY[i]);
            shelfs[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }     
    }

    #endregion

}
