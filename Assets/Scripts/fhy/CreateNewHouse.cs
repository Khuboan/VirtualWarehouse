using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewHouse : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject WallPre, DoorPre;
    /// <summary>
    /// 获取到的需要生成的墙的信息
    /// </summary>
    public List<WallHubDetail> wallHubDetails;
    /// <summary>
    /// 房子的父物体
    /// </summary>
    public Transform HouseHub;
    public List<Transform> Wall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateHouse()
    {
        for (int i = 0; i < wallHubDetails.Count; i++)
        {
            Wall[i].localPosition = wallHubDetails[i].WallStartPos;
            Wall[i].localEulerAngles = wallHubDetails[i].WallDir;
            wallHubDetails[i].AllLength = 0;
            for (int m = 0; m < Wall[i].childCount;m++)
            {

                Destroy(Wall[i].GetChild(m).gameObject);
            }
            for (int j = 0; j < wallHubDetails[i].wallDetails.Count; j++)
            {
                if (wallHubDetails[i].wallDetails[j].WallType == WallDetail.ModelType.Wall)
                {
                    GameObject newWall = GameObject.Instantiate(WallPre);
                    newWall.transform.parent = Wall[i];
                    newWall.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newWall.transform.localPosition = new Vector3(0, 0, wallHubDetails[i].AllLength);
                    newWall.transform.localScale = new Vector3(1, wallHubDetails[i].wallDetails[j].WallHeight, wallHubDetails[i].wallDetails[j].WallLength);
                    wallHubDetails[i].AllLength += wallHubDetails[i].wallDetails[j].WallLength;
                    wallHubDetails[i].wallDetails[j].Model = newWall;
                }
                if (wallHubDetails[i].wallDetails[j].WallType == WallDetail.ModelType.Door)
                {
                    GameObject newDoor = GameObject.Instantiate(DoorPre);
                    newDoor.transform.parent = Wall[i];
                    newDoor.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newDoor.transform.localPosition = new Vector3(0, 0, wallHubDetails[i].AllLength);
                    newDoor.transform.localScale = new Vector3(1, wallHubDetails[i].wallDetails[j].DoorHeight, wallHubDetails[i].wallDetails[j].WallLength);
                    wallHubDetails[i].wallDetails[j].DoorModel = newDoor;

                    GameObject newWall = GameObject.Instantiate(WallPre);
                    newWall.transform.parent = Wall[i];
                    newWall.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newWall.transform.localPosition = new Vector3(0, wallHubDetails[i].wallDetails[j].DoorHeight, wallHubDetails[i].AllLength);
                    newWall.transform.localScale = new Vector3(1, wallHubDetails[i].wallDetails[j].WallHeight - wallHubDetails[i].wallDetails[j].DoorHeight, wallHubDetails[i].wallDetails[j].WallLength);
                    wallHubDetails[i].wallDetails[j].Model = newWall;

                    wallHubDetails[i].AllLength += wallHubDetails[i].wallDetails[j].WallLength;

                }

            }
            //Wall[i].localPosition = new Vector3(wallHubDetails[i].AllLength, 0, 0);
        }
        Wall[0].localPosition = new Vector3(0, 0, 0);
        Wall[1].localPosition = new Vector3(-wallHubDetails[1].AllLength, 0, 0);
        Wall[2].localPosition = new Vector3(-wallHubDetails[1].AllLength, 0, wallHubDetails[2].AllLength);
        Wall[3].localPosition = new Vector3(0, 0, wallHubDetails[2].AllLength);
    }

}
[System.Serializable]
public class WallHubDetail
{
    public List<WallDetail> wallDetails;
    public Vector3 WallDir;
    public Vector3 WallStartPos;
    public float AllLength;

}
[System.Serializable]
public class WallDetail
{ 
    public ModelType WallType;
    public float WallPos;
    public float WallLength;
    public float WallHeight;
    public float DoorHeight;
    public GameObject Model, DoorModel;
    public enum ModelType
    {
        Wall, Door
    }
}

