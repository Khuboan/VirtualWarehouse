using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfHub : MonoBehaviour
{
    public Transform CenterPos;
    public int CenterPosNum;
    public GameObject[] Floor;
    public GameObject[] Floor2;
    public Transform[] FloorStart, FloorStart2, FloorEnd, FloorEnd2;
    public bool isY;
    public bool isL;
    /// <summary>
    /// 该货架存储的总的货物
    /// </summary>
    public List<ShelfObject> shelfObjects;
    /// <summary>
    /// 分层存储的货物
    /// </summary>
    public List<ShelfList> shelfLists;
    public Shelf shelf;
    public ShelfDetail shelfDetail = new ShelfDetail();
    /// <summary>
    /// 每个箱子的占地长度，用于计算每层的数量，多余的放到上一层
    /// </summary>
    public float BoxLength;
    public float floorlength;
    public Text Shelfname;
    public Vector3 WallPos;
    public Transform[] CameraDir;
    public ColliderTest[] colliderTests;
    // Start is called before the first frame update
    void Start()
    {
        
        //for(int i = 0;i<CreateNewHouse.in)
        //{

        //}
        float PosX1 = float.Parse(shelf.position[0].Split(',')[0]) / 100;
        float PosY1 = float.Parse(shelf.position[0].Split(',')[1]) / 100;
        float PosX2 = float.Parse(shelf.position[1].Split(',')[0]) / 100;
        float PosY2 = float.Parse(shelf.position[1].Split(',')[1]) / 100;
        float PosX3 = float.Parse(shelf.position[2].Split(',')[0]) / 100;
        float PosY3 = float.Parse(shelf.position[2].Split(',')[1]) / 100;
        float PosX4 = float.Parse(shelf.position[3].Split(',')[0]) / 100;
        float PosY4 = float.Parse(shelf.position[3].Split(',')[1]) / 100;
        shelfDetail.scale = new Vector3(PosX2 - PosX1, 1, PosY3 - PosY1);
        if(PosY4 - PosY1 >= PosX2 - PosX1)//竖向排列
        {
            transform.GetChild(0).localScale = shelfDetail.scale;
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else//横向排列
        {
            isY = true;
            transform.GetChild(1).localScale = shelfDetail.scale;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if(!isY)
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
                if (BoxLength * shelf.floor[i].material.Count > floorlength)
                {
                    //Debug.Log("floorlength = " + floorlength + "  数量 = " + (float)BoxLength * shelf.floor[i].material.Count);
                    scaleValue = floorlength / ((float)(BoxLength * shelf.floor[i].material.Count)) * 0.95f;
                    boxleng = BoxLength * scaleValue * 0.95f;
                    //Debug.Log("scaleValue=" + scaleValue);
                   // Debug.Log("boxleng=" + boxleng);
                }
                for (int j = 0; j < shelf.floor[i].material.Count; j++)
                {
                    GameObject newObject = GameObject.Instantiate(shelfLists[i].MetaModel, this.transform);
                    if (BoxLength * shelf.floor[i].material.Count > floorlength)
                        newObject.transform.localScale = new Vector3(shelfLists[i].MetaModel.transform.localScale.x * scaleValue, shelfLists[i].MetaModel.transform.localScale.y * scaleValue, shelfLists[i].MetaModel.transform.localScale.z * scaleValue );
                    //Debug.Log("调整前Scale = " + newObject.transform.localScale);
                    newObject.transform.localScale = new Vector3(shelfLists[i].MetaModel.transform.localScale.x * scaleValue, shelfLists[i].MetaModel.transform.localScale.y * scaleValue, shelfLists[i].MetaModel.transform.localScale.z * scaleValue * shelfDetail.scale.x*2);
                   // Debug.Log("调整后Scale = " + newObject.transform.localScale);
                    if (!isL)
                    {
                       // Debug.Log(gameObject.name+"  " + shelf.name +"   " + "!isY && !isL");
                        newObject.transform.position = shelfLists[i].MetaModel.transform.position + new Vector3(shelfDetail.scale.x/2-0.3f, 0, j * boxleng);
                    }
                    else
                    {
                        //Debug.Log(gameObject.name + "  " + shelf.name + "   " + "!isY && isL");
                             newObject.transform.position = shelfLists[i].MetaModel.transform.position + new Vector3(-shelfDetail.scale.x, 0, j * boxleng);
                    }
                    newObject.GetComponent<ShelfObject>().material = shelf.floor[i].material[j];
                    newObject.GetComponent<ShelfObject>().CamPosIndex = CenterPosNum;
                    newObject.GetComponent<ShelfObject>().Floor = i;
                    newObject.GetComponent<ShelfObject>().Num = j;
                    newObject.SetActive(true);
                    shelfObjects.Add(newObject.GetComponent<ShelfObject>());
                    shelfLists[i].ShelfModel.Add(newObject);
                    shelfLists[i].shelfObjects.Add(newObject.GetComponent<ShelfObject>());

                }
                Shelfname.text = shelf.name;
                
                Shelfname.gameObject.transform.parent.position = 
                    new Vector3(Floor[(shelf.floor.Count+1) / 2].transform.position.x, 
                    Floor[(shelf.floor.Count+1) / 2].transform.position.y+1, 
                    Floor[(shelf.floor.Count + 1) / 2].transform.position.z);
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


            floorlength = Vector3.Distance(FloorStart2[0].position, FloorEnd2[0].position);
            for (int i = 0; i < Floor.Length; i++)
            {
                Floor2[i].SetActive((float)shelf.floor.Count / 2 > i);
            }



            for (int i = 0; i < shelf.floor.Count; i++)
            {
                //   shelfLists[i].
                float boxleng = BoxLength;
                float scaleValue = 1;
                if (BoxLength * shelf.floor[i].material.Count > floorlength)
                {
                    //Debug.Log("floorlength = " + floorlength + "  数量 = " + (float)BoxLength * shelf.floor[i].material.Count);
                    scaleValue = floorlength / ((float)(BoxLength * shelf.floor[i].material.Count)) * 0.95f;
                    boxleng = BoxLength * scaleValue * 0.95f;
                    //Debug.Log("scaleValue=" + scaleValue);
                    //Debug.Log("boxleng=" + boxleng);
                }
                for (int j = 0; j < shelf.floor[i].material.Count; j++)
                {
                    GameObject newObject = GameObject.Instantiate(shelfLists[i].MetaModel, this.transform);
                    if (BoxLength * shelf.floor[i].material.Count > floorlength)
                        newObject.transform.localScale = new Vector3(shelfLists[i].MetaModel.transform.localScale.x * scaleValue, shelfLists[i].MetaModel.transform.localScale.y * scaleValue, shelfLists[i].MetaModel.transform.localScale.z * scaleValue);
                    newObject.transform.localEulerAngles = Vector3.zero;
                    //Debug.Log("竖向");
                    //Debug.Log(this.gameObject.name+ "isY = " + isY);
                    if(!isL)
                    {
                        // Debug.Log("竖向!isL");
                        //Debug.Log(gameObject.name + "  " + shelf.name + "   " + "isY && !isL");
                        newObject.transform.localScale = new Vector3(shelfLists[i].MetaModel.transform.localScale.x * scaleValue, shelfLists[i].MetaModel.transform.localScale.y * scaleValue, shelfLists[i].MetaModel.transform.localScale.z * scaleValue * shelfDetail.scale.z *2);
                        newObject.transform.position = shelfLists[i].MetaModel.transform.position + new Vector3(j * boxleng + 0.3f, 0, shelfDetail.scale.z/2-0.45f);

                    }
                    else
                    {
                        //Debug.Log("竖向isL");
                        //Debug.Log(gameObject.name + "  " + shelf.name + "   " + "isY && isL");
                        newObject.transform.position = shelfLists[i].MetaModel.transform.position + new Vector3(j * boxleng + 0.3f, 0, 0);
                    }
                    newObject.GetComponent<ShelfObject>().material = shelf.floor[i].material[j];
                    newObject.SetActive(true);
                    newObject.GetComponent<ShelfObject>().CamPosIndex = CenterPosNum;
                    newObject.GetComponent<ShelfObject>().Floor = i;
                    newObject.GetComponent<ShelfObject>().Num = j;
                    shelfObjects.Add(newObject.GetComponent<ShelfObject>());
                    shelfLists[i].ShelfModel.Add(newObject);
                    shelfLists[i].shelfObjects.Add(newObject.GetComponent<ShelfObject>());

                }
                Shelfname.text = shelf.name;
                //Shelfname.gameObject.transform.parent.localEulerAngles = Vector3.zero;
                Shelfname.gameObject.transform.parent.position =
                    new Vector3(Floor2[(shelf.floor.Count + 1) / 2].transform.position.x,
                    Floor2[(shelf.floor.Count + 1) / 2].transform.position.y + 1,
                    Floor2[(shelf.floor.Count + 1) / 2].transform.position.z);
            }


        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        //if (CameraController.instance.targetPoint[CameraController.instance.camposIndex]==CenterPos)
        //{
        //    for(int i = 0;i<Floor.Length;i++)
        //    {
        //        Floor[i].gameObject.GetComponent<Outline>().enabled = true;
        //        Floor2[i].gameObject.GetComponent<Outline>().enabled = true;
        //    }
            
        //}
        //else
        //{
        //    for (int i = 0; i < Floor.Length; i++)
        //    {
        //        Floor[i].gameObject.GetComponent<Outline>().enabled = false;
        //        Floor2[i].gameObject.GetComponent<Outline>().enabled = false;
        //    }
        //}


        if (!isY)
        {
            if (colliderTests[0].isObstacle == true && colliderTests[1].isObstacle == false)
            {
                Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, -90, 0);

                CenterPos.position = CameraDir[1].position;
                isL = true;
            }
            if (colliderTests[0].isObstacle == false && colliderTests[1].isObstacle == true || colliderTests[0].isObstacle == false && colliderTests[1].isObstacle == false)
            {
                Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, 90, 0);

                CenterPos.position = CameraDir[0].position;
                isL = false;
            }
            if(colliderTests[0].isObstacle == true && colliderTests[1].isObstacle == true)
            {
                if (colliderTests[0].collname.Contains("Wall"))
                {
                    Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, -90, 0);

                    CenterPos.position = CameraDir[1].position;
                    isL = true;
                }
                if (colliderTests[1].collname.Contains("Wall"))
                {
                    Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, 90, 0);

                    CenterPos.position = CameraDir[0].position;
                    isL = false;
                }
            }
        }
        else
        {
            if (colliderTests[2].isObstacle == true&& colliderTests[3].isObstacle == false)
            {
                Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, 180, 0);
                CenterPos.position = CameraDir[3].position;
                isL = true;
            }
            if (colliderTests[2].isObstacle == false && colliderTests[3].isObstacle == true || colliderTests[2].isObstacle == false && colliderTests[3].isObstacle == false)
            {
                Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, 0, 0);

                CenterPos.position = CameraDir[2].position;
                isL = false;
            }
            if (colliderTests[2].isObstacle == true && colliderTests[3].isObstacle == true)
            {
                if (colliderTests[2].collname.Contains("Wall"))
                {
                    Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, 180, 0);
                    CenterPos.position = CameraDir[3].position;
                    isL = true;
                }
                if (colliderTests[3].collname.Contains("Wall"))
                {
                    Shelfname.gameObject.transform.parent.localEulerAngles = new Vector3(0, 0, 0);

                    CenterPos.position = CameraDir[2].position;
                    isL = false;
                }
            }
        }
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

