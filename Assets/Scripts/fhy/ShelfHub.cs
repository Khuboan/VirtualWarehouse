using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfHub : MonoBehaviour
{
    public Transform StartPos,EndPos,CenterPos;
    public GameObject[] Floor;
    public Transform[] FloorStart,FloorEnd;
    /// <summary>
    /// 该货架存储的总的货物
    /// </summary>
    public List<ShelfObject> shelfObjects;
    /// <summary>
    /// 分层存储的货物
    /// </summary>
    public List<ShelfList> shelfLists;
    public Shelf shelf;
    /// <summary>
    /// 每个箱子的占地长度，用于计算每层的数量，多余的放到上一层
    /// </summary>
    public float BoxLength;
    public float floorlength;
    // Start is called before the first frame update
    void Start()
    {
        floorlength = Vector3.Distance(FloorStart[0].position, FloorEnd[0].position);
        for (int i = 0; i < Floor.Length; i++)
        {
            Floor[i].SetActive((float)shelf.floor.Count / 2 > i);
        }
        for (int i = 0; i < shelf.floor.Count; i++)
        {
            //   shelfLists[i].
            float boxleng = BoxLength;
            float scaleValue = 1;
            if(BoxLength * shelf.floor[i].material.Count> floorlength)
            {
                Debug.Log("floorlength = " + floorlength + "  数量 = " + (float)BoxLength * shelf.floor[i].material.Count);
                scaleValue = floorlength/((float)(BoxLength * shelf.floor[i].material.Count)) * 0.95f;
                boxleng = BoxLength * scaleValue*0.95f;
                Debug.Log("scaleValue=" + scaleValue);
                Debug.Log("boxleng=" + boxleng);
            }
            for (int j = 0; j < shelf.floor[i].material.Count; j++)
            {
                GameObject newObject = GameObject.Instantiate(shelfLists[i].MetaModel, this.transform);
                if (BoxLength * shelf.floor[i].material.Count > floorlength)
                    newObject.transform.localScale = new Vector3(shelfLists[i].MetaModel.transform.localScale.x * scaleValue, shelfLists[i].MetaModel.transform.localScale.y * scaleValue, shelfLists[i].MetaModel.transform.localScale.z * scaleValue);
                newObject.transform.position = shelfLists[i].MetaModel.transform.position + new Vector3(0, 0, j * boxleng);
                newObject.GetComponent<ShelfObject>().material = shelf.floor[i].material[j];
                newObject.SetActive(true);
                shelfObjects.Add(newObject.GetComponent<ShelfObject>());
                shelfLists[i].ShelfModel.Add(newObject);
                shelfLists[i].shelfObjects.Add(newObject.GetComponent<ShelfObject>());

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class ShelfList
{
    public string name;
    public GameObject MetaModel;
    public List<GameObject> ShelfModel;
    public List<ShelfObject> shelfObjects;
}

