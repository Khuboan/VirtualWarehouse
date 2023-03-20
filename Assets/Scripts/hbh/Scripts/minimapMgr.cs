using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimapMgr : MonoBehaviour
{
    //[HideInInspector]
    public Image rectLine;
    //[HideInInspector]
    //public Image[] fDoor, bDoor, lDoor, rDoor;

    public Image[] doors;
    public Image[] barrier;
    public Image[] bin;
    public Image[] shelfs;

    private float rectHeight;
    private float rectWidth;

    Warehouse[] warehouse;



    public void SetRectSize(int index)
    {
        warehouse = JsonDataAnylize.instance.rootObject.warehouse;
        rectWidth = (float.Parse(warehouse[index].position[1].Split(',')[0]) - float.Parse(warehouse[index].position[0].Split(',')[0])) / 10;
        rectHeight = (float.Parse(warehouse[index].position[2].Split(',')[1]) - float.Parse(warehouse[index].position[1].Split(',')[1])) / 10;

        rectLine.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
        SetPosDoor(index);
        SetPosBarrier(index);
        SetPosBin(index);
        SetPosShelfs(index);
    }

    public void SetPosDoor(int index)
    {
        // rootObject.warehouse[i].door[j].position[k]
        float[] PosX, PosY, sizeX, sizeY;

        PosX = new float[doors.Length];
        PosY = new float[doors.Length];
        sizeX = new float[doors.Length];
        sizeY = new float[doors.Length];
        for (int i = 0; i < doors.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[index].door[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].door[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            sizeX[i] = (float.Parse(warehouse[index].door[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].door[i].position[0].Split(',')[0])) / 10;
            doors[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i], 7f);
            //PosY[i] = (float.Parse(warehouse[0].door[i].position[1].Split(',')[1]) + float.Parse(warehouse[0].door[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            doors[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], -rectHeight / 2,0);
        }
        
        //fDoor.GetComponent<RectTransform>().localPosition = new Vector3(0, rectHeight / 2, 0); //ÉÏ
        //lDoor.GetComponent<RectTransform>().localPosition = new Vector3(-rectWidth / 2, 0, 0); //×ó
        //rDoor.GetComponent<RectTransform>().localPosition = new Vector3(rectWidth / 2, 0, 0); //ÓÒ
    }

    public void SetPosBarrier(int index)
    {
        float[] PosX,PosY, sizeX, sizeY;
        PosX = new float[barrier.Length];
        PosY = new float[barrier.Length];
        sizeX = new float[barrier.Length];
        sizeY = new float[barrier.Length];
        for (int i = 0; i < barrier.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[index].barrier[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].barrier[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[index].barrier[i].position[1].Split(',')[1]) + float.Parse(warehouse[index].barrier[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            sizeX[i] = (float.Parse(warehouse[index].barrier[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].barrier[i].position[0].Split(',')[0])) / 10;
            sizeY[i] = (float.Parse(warehouse[index].barrier[i].position[2].Split(',')[1]) - float.Parse(warehouse[index].barrier[i].position[1].Split(',')[1])) / 10;
            barrier[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i],sizeY[i]);
            barrier[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

    public void SetPosBin(int index)
    {
        float[] PosX, PosY, sizeX, sizeY;
        PosX = new float[bin.Length];
        PosY = new float[bin.Length];
        sizeX = new float[bin.Length];
        sizeY = new float[bin.Length];
        for (int i = 0; i < bin.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[index].bin[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].bin[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[index].bin[i].position[1].Split(',')[1]) + float.Parse(warehouse[index].bin[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            sizeX[i] = (float.Parse(warehouse[index].bin[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].bin[i].position[0].Split(',')[0])) / 10;
            sizeY[i] = (float.Parse(warehouse[index].bin[i].position[2].Split(',')[1]) - float.Parse(warehouse[index].bin[i].position[1].Split(',')[1])) / 10;
            bin[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i], sizeY[i]);
            bin[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

    public void SetPosShelfs(int index)
    {
        float[] PosX, PosY, sizeX, sizeY;
        PosX = new float[shelfs.Length];
        PosY = new float[shelfs.Length];
        sizeX = new float[shelfs.Length];
        sizeY = new float[shelfs.Length];
        for (int i = 0; i < shelfs.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[index].shelf[i].position[1].Split(',')[0]) + float.Parse(warehouse[index].shelf[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[index].shelf[i].position[1].Split(',')[1]) + float.Parse(warehouse[index].shelf[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            sizeX[i] = (float.Parse(warehouse[index].shelf[i].position[1].Split(',')[0]) - float.Parse(warehouse[index].shelf[i].position[0].Split(',')[0])) / 10;
            sizeY[i] = (float.Parse(warehouse[index].shelf[i].position[2].Split(',')[1]) - float.Parse(warehouse[index].shelf[i].position[1].Split(',')[1])) / 10;
            shelfs[i].GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX[i], sizeY[i]);
            shelfs[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

}
