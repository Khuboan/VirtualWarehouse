using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimapMgr : MonoBehaviour
{
    [HideInInspector]
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



    public void SetRectSize()
    {
        warehouse = JsonDataAnylize.instance.rootObject.warehouse;
        rectWidth = (float.Parse(warehouse[0].position[1].Split(',')[0]) - float.Parse(warehouse[0].position[0].Split(',')[0])) / 10;
        rectHeight = (float.Parse(warehouse[0].position[2].Split(',')[1]) - float.Parse(warehouse[0].position[1].Split(',')[1])) / 10;

        rectLine.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
        SetPosDoor();
        SetPosBarrier();
        SetPosBin();
        SetPosShelfs();
    }

    public void SetPosDoor()
    {
        // rootObject.warehouse[i].door[j].position[k]
        float[] PosX, PosY;
        PosX = new float[doors.Length];
        PosY = new float[doors.Length];
        for (int i = 0; i < doors.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[0].door[i].position[1].Split(',')[0]) + float.Parse(warehouse[0].door[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            //PosY[i] = (float.Parse(warehouse[0].door[i].position[1].Split(',')[1]) + float.Parse(warehouse[0].door[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            doors[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], -rectHeight / 2,0);
        }
        
        //fDoor.GetComponent<RectTransform>().localPosition = new Vector3(0, rectHeight / 2, 0); //ÉÏ
        //lDoor.GetComponent<RectTransform>().localPosition = new Vector3(-rectWidth / 2, 0, 0); //×ó
        //rDoor.GetComponent<RectTransform>().localPosition = new Vector3(rectWidth / 2, 0, 0); //ÓÒ
    }

    public void SetPosBarrier()
    {
        float[] PosX,PosY;
        PosX = new float[barrier.Length];
        PosY = new float[barrier.Length];
        for (int i = 0; i < barrier.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[0].barrier[i].position[1].Split(',')[0]) + float.Parse(warehouse[0].barrier[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[0].barrier[i].position[1].Split(',')[1]) + float.Parse(warehouse[0].barrier[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            barrier[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

    public void SetPosBin()
    {
        float[] PosX, PosY;
        PosX = new float[bin.Length];
        PosY = new float[bin.Length];
        for (int i = 0; i < bin.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[0].bin[i].position[1].Split(',')[0]) + float.Parse(warehouse[0].bin[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[0].bin[i].position[1].Split(',')[1]) + float.Parse(warehouse[0].bin[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            //Debug.Log(PosX[i]);
            //Debug.Log(PosY[i]);
            bin[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

    public void SetPosShelfs()
    {
        float[] PosX, PosY;
        PosX = new float[shelfs.Length];
        PosY = new float[shelfs.Length];
        for (int i = 0; i < shelfs.Length; i++)
        {
            PosX[i] = (float.Parse(warehouse[0].shelf[i].position[1].Split(',')[0]) + float.Parse(warehouse[0].shelf[i].position[0].Split(',')[0])) / 2 / 10 - 100;
            PosY[i] = (float.Parse(warehouse[0].shelf[i].position[1].Split(',')[1]) + float.Parse(warehouse[0].shelf[i].position[2].Split(',')[1])) / 2 / 10 - 50;
            shelfs[i].GetComponent<RectTransform>().localPosition = new Vector3(PosX[i], PosY[i]);
        }
    }

}
