using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimapMgr : MonoBehaviour
{
    [HideInInspector]
    public Image rectLine;

    public float rectHeight;
    public float rectWidth;


    public void SetRectSize()
    {
        rectLine.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
    }
}
