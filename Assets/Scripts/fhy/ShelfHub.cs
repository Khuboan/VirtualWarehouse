using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfHub : MonoBehaviour
{
    public Transform StartPos,EndPos,CenterPos;
    public GameObject[] Floor;
    public Transform[] FloorStart,FloorEnd;
    /// <summary>
    /// �û��ܴ洢���ܵĻ���
    /// </summary>
    public List<ShelfObject> shelfObjects;
    /// <summary>
    /// �ֲ�洢�Ļ���
    /// </summary>
    public List<ShelfList> shelfLists;
    public Shelf shelf;
    /// <summary>
    /// ÿ�����ӵ�ռ�س��ȣ����ڼ���ÿ�������������ķŵ���һ��
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
                Debug.Log("floorlength = " + floorlength + "  ���� = " + (float)BoxLength * shelf.floor[i].material.Count);
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

