using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimapMgr : MonoBehaviour
{
    [HideInInspector]
    public Image rectLine;
    [HideInInspector]
    public Image fDoor, bDoor, lDoor, rDoor;

    public float rectHeight;
    public float rectWidth;


    public void SetRectSize()
    {
        rectLine.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
        SetPosDoor();
    }

    public void SetPosDoor()
    {
        bDoor.GetComponent<RectTransform>().localPosition = new Vector3(0, -rectHeight / 2, 0);
        fDoor.GetComponent<RectTransform>().localPosition = new Vector3(0, rectHeight / 2, 0);
        lDoor.GetComponent<RectTransform>().localPosition = new Vector3(-rectWidth / 2, 0, 0);
        rDoor.GetComponent<RectTransform>().localPosition = new Vector3(rectWidth / 2, 0, 0);
    }
}
