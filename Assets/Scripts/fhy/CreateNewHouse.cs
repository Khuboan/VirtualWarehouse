using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewHouse : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject HousePre,WallPre, DoorPre, BarrierPre,BinPre, ShelfPre;
    /// <summary>
    /// 获取到的需要生成的墙的信息
    /// </summary>
   // public List<WallHubDetail> wallHubDetails;
    /// <summary>
    /// 房子的父物体
    /// </summary>
    public Transform HouseHub;
    //public List<Transform> Wall;
    //public List<Vector4> DoorL;
    //public List<Vector4> DoorR;
    //public List<Vector4> DoorU;
    //public List<Vector4> DoorD;
    public List<HouseHubDetail> houseHubDetails;
    public float housePosLength;
    public float WallHight;
    public float DoorHight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateHouse(int a)
    {
        GameObject house = GameObject.Instantiate(HousePre, transform);
        house.transform.localPosition = houseHubDetails[a].HousePostion;
        foreach (Transform child in house.transform)
        {
            houseHubDetails[a].Wall.Add(child);
        }

        //生成障碍
        for(int i = 0;i< houseHubDetails[a].barrierDetails.Count;i++)
        {
            GameObject newBarrier = GameObject.Instantiate(BarrierPre, house.transform);
            newBarrier.transform.localScale = houseHubDetails[a].barrierDetails[i].scale;
            newBarrier.transform.localPosition = houseHubDetails[a].barrierDetails[i].pos;
            houseHubDetails[a].barrierDetails[i].model = newBarrier;
        }
        //生成库位
        for (int i = 0; i < houseHubDetails[a].binDetails.Count; i++)
        {
            GameObject newBin = GameObject.Instantiate(BinPre, house.transform);
            newBin.transform.localScale = houseHubDetails[a].binDetails[i].scale;
            newBin.transform.localPosition = houseHubDetails[a].binDetails[i].pos;
            houseHubDetails[a].binDetails[i].model = newBin;
        }
        //生成货架
        for (int i = 0; i < houseHubDetails[a].shelfDetails.Count; i++)
        {
            GameObject newShelf = GameObject.Instantiate(ShelfPre, house.transform);
            newShelf.transform.GetChild(0).localScale = houseHubDetails[a].shelfDetails[i].scale;
            newShelf.transform.localPosition = houseHubDetails[a].shelfDetails[i].pos;
            houseHubDetails[a].shelfDetails[i].model = newShelf;
            newShelf.GetComponent<ShelfHub>().shelf = houseHubDetails[a].shelfDetails[i].shelf;
            newShelf.name = "Shelf" + i;
            CameraController.instance.targetPoint.Add(newShelf.GetComponent<ShelfHub>().CenterPos);
        }
        //生成仓库墙壁
        for (int i = 0; i < houseHubDetails[a].wallHubDetails.Count; i++)
        {

            houseHubDetails[a].Wall[i].localPosition = houseHubDetails[a].wallHubDetails[i].WallStartPos;
            houseHubDetails[a].Wall[i].localEulerAngles = houseHubDetails[a].wallHubDetails[i].WallDir;
            houseHubDetails[a].wallHubDetails[i].AllLength = 0;

            for (int j = 0; j < houseHubDetails[a].wallHubDetails[i].wallDetails.Count; j++)
            {
                if (houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallType == WallDetail.ModelType.Wall)
                {
                    GameObject newWall = GameObject.Instantiate(WallPre);
                    newWall.transform.parent = houseHubDetails[a].Wall[i];
                    newWall.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newWall.transform.localPosition = new Vector3(0, 0, houseHubDetails[a].wallHubDetails[i].AllLength);
                    newWall.transform.localScale = new Vector3(0.3f, houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallHeight, houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallLength);
                    houseHubDetails[a].wallHubDetails[i].AllLength += houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallLength;
                    houseHubDetails[a].wallHubDetails[i].wallDetails[j].Model = newWall;
                }
                if (houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallType == WallDetail.ModelType.Door)
                {
                    GameObject newDoor = GameObject.Instantiate(DoorPre);
                    newDoor.transform.parent = houseHubDetails[a].Wall[i];
                    newDoor.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newDoor.transform.localPosition = new Vector3(0, 0, houseHubDetails[a].wallHubDetails[i].AllLength);
                    newDoor.transform.localScale = new Vector3(0.1f, houseHubDetails[a].wallHubDetails[i].wallDetails[j].DoorHeight, houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallLength);
                    houseHubDetails[a].wallHubDetails[i].wallDetails[j].DoorModel = newDoor;

                    GameObject newWall = GameObject.Instantiate(WallPre);
                    newWall.transform.parent = houseHubDetails[a].Wall[i];
                    newWall.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newWall.transform.localPosition = new Vector3(0, houseHubDetails[a].wallHubDetails[i].wallDetails[j].DoorHeight, houseHubDetails[a].wallHubDetails[i].AllLength);
                    newWall.transform.localScale = new Vector3(0.3f, houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallHeight - houseHubDetails[a].wallHubDetails[i].wallDetails[j].DoorHeight, houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallLength);
                    houseHubDetails[a].wallHubDetails[i].wallDetails[j].Model = newWall;

                    houseHubDetails[a].wallHubDetails[i].AllLength += houseHubDetails[a].wallHubDetails[i].wallDetails[j].WallLength;

                }

            }
            //Wall[i].localPosition = new Vector3(wallHubDetails[i].AllLength, 0, 0);
        }
        //houseHubDetails[a].Wall[0].localPosition = houseHubDetails[a].wallHubDetails[0].WallStartPos;
        //houseHubDetails[a].Wall[1].localPosition = houseHubDetails[a].wallHubDetails[1].WallStartPos;
        //houseHubDetails[a].Wall[2].localPosition = houseHubDetails[a].wallHubDetails[2].WallStartPos;
        //houseHubDetails[a].Wall[3].localPosition = houseHubDetails[a].wallHubDetails[3].WallStartPos;
        //houseHubDetails[a].Wall[2].localPosition = new Vector3(-houseHubDetails[a].wallHubDetails[1].AllLength, 0, houseHubDetails[a].wallHubDetails[2].AllLength);
        //houseHubDetails[a].Wall[3].localPosition = new Vector3(0, 0, houseHubDetails[a].wallHubDetails[2].AllLength);
    }
    public void GetHouseData()
    {
        CameraController.instance.targetPoint.Clear();
        housePosLength = 0;
            for (int m = 0; m < transform.childCount; m++)
            {
                Destroy(transform.GetChild(m).gameObject);
            }
       
        houseHubDetails.Clear();
        int HouseNum = JsonDataAnylize.instance.rootObject.warehouse.Length;
        for(int i = 0;i< JsonDataAnylize.instance.rootObject.warehouse.Length;i++)
        {
            HouseHubDetail newhouseHubDetail = new HouseHubDetail();
            //获取地库数据
            Debug.Log("地库的数量 = " + JsonDataAnylize.instance.rootObject.warehouse[i].barrier.Count);
            for(int j = 0;j< JsonDataAnylize.instance.rootObject.warehouse[i].barrier.Count; j++)
            {
                Debug.Log(j);
                BarrierDetail barrierDetail = new BarrierDetail();
                barrierDetail.name = JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].name;
                float PosX1 = float.Parse (JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[0].Split(',')[0])/ 100;
                float PosY1 = float.Parse (JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[0].Split(',')[1])/ 100;
                float PosX2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[1].Split(',')[0]) / 100;
                float PosY2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[1].Split(',')[1]) / 100;
                float PosX3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[2].Split(',')[0]) / 100;
                float PosY3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[2].Split(',')[1]) / 100;
                float PosX4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[3].Split(',')[0]) / 100;
                float PosY4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].position[3].Split(',')[1]) / 100;
                barrierDetail.scale = new Vector3(PosX2 - PosX1, WallHight, PosY3 - PosY1);
                barrierDetail.pos = new Vector3(PosX1, 0, PosY1);
                newhouseHubDetail.barrierDetails.Add(barrierDetail);
            }
            //获取货架数据
            for (int j = 0; j < JsonDataAnylize.instance.rootObject.warehouse[i].shelf.Count; j++)
            {
                ShelfDetail shelfDetail = new ShelfDetail();
                shelfDetail.name = JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].name;
                float PosX1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[0].Split(',')[0]) / 100;
                float PosY1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[0].Split(',')[1]) / 100;
                float PosX2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[1].Split(',')[0]) / 100;
                float PosY2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[1].Split(',')[1]) / 100;
                float PosX3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[2].Split(',')[0]) / 100;
                float PosY3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[2].Split(',')[1]) / 100;
                float PosX4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[3].Split(',')[0]) / 100;
                float PosY4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j].position[3].Split(',')[1]) / 100;
                shelfDetail.scale = new Vector3(PosX2 - PosX1, 1, PosY3 - PosY1);
                shelfDetail.pos = new Vector3(PosX1, 0, PosY1);
                shelfDetail.shelf = JsonDataAnylize.instance.rootObject.warehouse[i].shelf[j];
                newhouseHubDetail.shelfDetails.Add(shelfDetail);
            }


            //获取障碍数据
            for (int j = 0; j < JsonDataAnylize.instance.rootObject.warehouse[i].bin.Count; j++)
            {
                BinDetail binDetail = new BinDetail();
                binDetail.name = JsonDataAnylize.instance.rootObject.warehouse[i].barrier[j].name;
                float PosX1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[0].Split(',')[0]) / 100;
                float PosY1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[0].Split(',')[1]) / 100;
                float PosX2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[1].Split(',')[0]) / 100;
                float PosY2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[1].Split(',')[1]) / 100;
                float PosX3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[2].Split(',')[0]) / 100;
                float PosY3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[2].Split(',')[1]) / 100;
                float PosX4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[3].Split(',')[0]) / 100;
                float PosY4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].bin[j].position[3].Split(',')[1]) / 100;
                binDetail.scale = new Vector3(PosX2 - PosX1, 1, PosY3 - PosY1);
                binDetail.pos = new Vector3(PosX1, 0, PosY1);
                newhouseHubDetail.binDetails.Add(binDetail);
            }



            //获取墙壁数据
            for (int j = 0;j< JsonDataAnylize.instance.rootObject.warehouse[i].door.Count;j++)
            {
                float doorx1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].door[j].position[0].Split(',')[0])/100;
                float doory1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].door[j].position[0].Split(',')[1]) / 100;
                float doorx2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].door[j].position[1].Split(',')[0]) / 100;
                float doory2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].door[j].position[1].Split(',')[1]) / 100;
                if (doory1 == 0 & doory2 == 0)
                {
                    newhouseHubDetail.DoorD.Add(new Vector4(doorx1, doory1, doorx2, doory2));
                }
                if (doorx1 == 0 & doorx2 == 0)
                {
                    newhouseHubDetail.DoorL.Add(new Vector4(doorx1, doory1, doorx2, doory2));
                }
                if (doorx1 != 0 & doorx2 == doorx1)
                {
                    newhouseHubDetail.DoorR.Add(new Vector4(doorx1, doory1, doorx2, doory2));
                }
                if (doory1 != 0 & doory2 == doory1)
                {
                    newhouseHubDetail.DoorU.Add(new Vector4(doorx1, doory1, doorx2, doory2));
                }
            }
            
                float wallx1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[0].Split(',')[0])/100;
                float wally1 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[0].Split(',')[1]) / 100;
                float wallx2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[1].Split(',')[0]) / 100;
                float wally2 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[1].Split(',')[1]) / 100;
                float wallx3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[2].Split(',')[0]) / 100;
                float wally3 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[2].Split(',')[1]) / 100;
                float wallx4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[3].Split(',')[0]) / 100;
                float wally4 = float.Parse(JsonDataAnylize.instance.rootObject.warehouse[i].position[3].Split(',')[1]) / 100;
                newhouseHubDetail.HousePostion = new Vector3(wallx1, 0, wally1);
                newhouseHubDetail.WallD = Mathf.Abs(wallx2 - wallx1);
                newhouseHubDetail.WallL = Mathf.Abs(wally4 - wally1);
                newhouseHubDetail.WallR = Mathf.Abs(wally3 - wally2);
                newhouseHubDetail.WallU = Mathf.Abs(wallx3 - wallx4);
            if (newhouseHubDetail.DoorL.Count==0)
            {
                WallHubDetail wallHubDetail = new WallHubDetail();
                WallDetail wallDetail = new WallDetail();
                wallDetail.WallLength = newhouseHubDetail.WallL;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                
                wallHubDetail.AllLength = newhouseHubDetail.WallL;
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            else
            {
                float alllength = 0;

                WallHubDetail wallHubDetail = new WallHubDetail();///首先生成第一面墙，墙的长度等于第一个门的x1
                WallDetail wallDetail = new WallDetail();
                wallHubDetail.AllLength = newhouseHubDetail.WallL;
                Debug.LogError("下方的门的数量为" + newhouseHubDetail.DoorL.Count);
                Debug.LogError("第一个门的坐标为" + newhouseHubDetail.DoorL[0]);
                wallDetail.WallLength = newhouseHubDetail.DoorL[0].y;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                alllength += wallDetail.WallLength;
                for (int k = 0; k < newhouseHubDetail.DoorL.Count; k++)
                {
                    //Debug.LogError("正在生成第" + k  + newhouseHubDetail.DoorD[0]);

                    WallDetail doorDetail = new WallDetail();///生成一个门，门的长度根据门的x2-x1
                    doorDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorL[k].w - newhouseHubDetail.DoorL[k].y);
                    doorDetail.WallType = WallDetail.ModelType.Door;
                    doorDetail.WallHeight = WallHight;
                    doorDetail.DoorHeight = DoorHight;
                    wallHubDetail.wallDetails.Add(doorDetail);
                    alllength += doorDetail.WallLength;
                    if (k < newhouseHubDetail.DoorL.Count - 1)
                    {
                        WallDetail dwallDetail = new WallDetail();//生成门与门之间的墙，长度等于后面的门的x减去前面的门的x
                        dwallDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorL[k + 1].y - newhouseHubDetail.DoorL[k].y);
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                        alllength += dwallDetail.WallLength;
                    }
                    else
                    {
                        WallDetail dwallDetail = new WallDetail();//生成最后一面墙，长度等于墙的总长度减去前面生成的长度
                        dwallDetail.WallLength = wallHubDetail.AllLength - alllength;
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                    }

                }
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            if (newhouseHubDetail.DoorR.Count == 0)
            {
                WallHubDetail wallHubDetail = new WallHubDetail();
                WallDetail wallDetail = new WallDetail();
                wallDetail.WallLength = newhouseHubDetail.WallR;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                wallHubDetail.WallStartPos = new Vector3(newhouseHubDetail.WallD + 0.3f, 0, 0);
                wallHubDetail.AllLength = newhouseHubDetail.WallR;
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            else
            {
                float alllength = 0;

                WallHubDetail wallHubDetail = new WallHubDetail();///首先生成第一面墙，墙的长度等于第一个门的x1
                WallDetail wallDetail = new WallDetail();
                wallHubDetail.AllLength = newhouseHubDetail.WallR;
                Debug.LogError("下方的门的数量为" + newhouseHubDetail.DoorR.Count);
                Debug.LogError("第一个门的坐标为" + newhouseHubDetail.DoorR[0]);
                wallDetail.WallLength = newhouseHubDetail.DoorR[0].y;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                alllength += wallDetail.WallLength;
                for (int k = 0; k < newhouseHubDetail.DoorR.Count; k++)
                {
                    //Debug.LogError("正在生成第" + k  + newhouseHubDetail.DoorD[0]);

                    WallDetail doorDetail = new WallDetail();///生成一个门，门的长度根据门的x2-x1
                    doorDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorR[k].w - newhouseHubDetail.DoorR[k].y);
                    doorDetail.WallType = WallDetail.ModelType.Door;
                    doorDetail.WallHeight = WallHight;
                    doorDetail.DoorHeight = DoorHight;
                    wallHubDetail.wallDetails.Add(doorDetail);
                    alllength += doorDetail.WallLength;
                    if (k < newhouseHubDetail.DoorR.Count - 1)
                    {
                        WallDetail dwallDetail = new WallDetail();//生成门与门之间的墙，长度等于后面的门的x减去前面的门的x
                        dwallDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorR[k + 1].y - newhouseHubDetail.DoorR[k].y);
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                        alllength += dwallDetail.WallLength;
                    }
                    else
                    {
                        WallDetail dwallDetail = new WallDetail();//生成最后一面墙，长度等于墙的总长度减去前面生成的长度
                        dwallDetail.WallLength = wallHubDetail.AllLength - alllength;
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                    }

                }
                wallHubDetail.WallStartPos = new Vector3(newhouseHubDetail.WallD + 0.3f, 0, 0);
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            if (newhouseHubDetail.DoorU.Count == 0)
            {
                WallHubDetail wallHubDetail = new WallHubDetail();
                WallDetail wallDetail = new WallDetail();
                wallDetail.WallLength = newhouseHubDetail.WallU;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                wallHubDetail.WallStartPos = new Vector3(0.3f, 0, newhouseHubDetail.WallR + 0.3f);
                wallHubDetail.WallDir = new Vector3(0, 90, 0);
                wallHubDetail.AllLength = newhouseHubDetail.WallU;
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            else
            {
                float alllength = 0;

                WallHubDetail wallHubDetail = new WallHubDetail();///首先生成第一面墙，墙的长度等于第一个门的x1
                WallDetail wallDetail = new WallDetail();
                wallHubDetail.AllLength = newhouseHubDetail.WallU;
                Debug.LogError("下方的门的数量为" + newhouseHubDetail.DoorU.Count);
                Debug.LogError("第一个门的坐标为" + newhouseHubDetail.DoorU[0]);
                wallDetail.WallLength = newhouseHubDetail.DoorU[0].x;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                alllength += wallDetail.WallLength;
                for (int k = 0; k < newhouseHubDetail.DoorU.Count; k++)
                {
                    //Debug.LogError("正在生成第" + k  + newhouseHubDetail.DoorD[0]);

                    WallDetail doorDetail = new WallDetail();///生成一个门，门的长度根据门的x2-x1
                    doorDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorU[k].z - newhouseHubDetail.DoorU[k].x);
                    doorDetail.WallType = WallDetail.ModelType.Door;
                    doorDetail.WallHeight = WallHight;
                    doorDetail.DoorHeight = DoorHight;
                    wallHubDetail.wallDetails.Add(doorDetail);
                    alllength += doorDetail.WallLength;
                    if (k < newhouseHubDetail.DoorU.Count - 1)
                    {
                        WallDetail dwallDetail = new WallDetail();//生成门与门之间的墙，长度等于后面的门的x减去前面的门的x
                        dwallDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorU[k + 1].x - newhouseHubDetail.DoorU[k].x);
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                        alllength += dwallDetail.WallLength;
                    }
                    else
                    {
                        WallDetail dwallDetail = new WallDetail();//生成最后一面墙，长度等于墙的总长度减去前面生成的长度
                        dwallDetail.WallLength = wallHubDetail.AllLength - alllength;
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                    }

                }
                wallHubDetail.WallStartPos = new Vector3(0.3f, 0, newhouseHubDetail.WallR + 0.3f);
                wallHubDetail.WallDir = new Vector3(0, 90, 0);
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            if (newhouseHubDetail.DoorD.Count == 0)
            {
                WallHubDetail wallHubDetail = new WallHubDetail();
                WallDetail wallDetail = new WallDetail();
                wallDetail.WallLength = newhouseHubDetail.WallD;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                wallHubDetail.WallStartPos = new Vector3(0.3f, 0, 0);
                wallHubDetail.WallDir = new Vector3(0, 90, 0);
                wallHubDetail.AllLength = newhouseHubDetail.WallD;
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            else
            {
                float alllength = 0;

                WallHubDetail wallHubDetail = new WallHubDetail();///首先生成第一面墙，墙的长度等于第一个门的x1
                WallDetail wallDetail = new WallDetail();
                wallHubDetail.AllLength = newhouseHubDetail.WallD;
                Debug.LogError("下方的门的数量为" + newhouseHubDetail.DoorD.Count);
                Debug.LogError("第一个门的坐标为" + newhouseHubDetail.DoorD[0]);
                wallDetail.WallLength = newhouseHubDetail.DoorD[0].x;
                wallDetail.WallHeight = WallHight;
                wallHubDetail.wallDetails.Add(wallDetail);
                alllength += wallDetail.WallLength;
                for (int k = 0; k < newhouseHubDetail.DoorD.Count; k++)
                {
                    //Debug.LogError("正在生成第" + k  + newhouseHubDetail.DoorD[0]);

                    WallDetail doorDetail = new WallDetail();///生成一个门，门的长度根据门的x2-x1
                    doorDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorD[k].z - newhouseHubDetail.DoorD[k].x);
                    doorDetail.WallType = WallDetail.ModelType.Door;
                    doorDetail.WallHeight = WallHight;
                    doorDetail.DoorHeight = DoorHight;
                    wallHubDetail.wallDetails.Add(doorDetail);
                    alllength += doorDetail.WallLength;
                    if (k < newhouseHubDetail.DoorD.Count - 1)
                    {
                        WallDetail dwallDetail = new WallDetail();//生成门与门之间的墙，长度等于后面的门的x减去前面的门的x
                        dwallDetail.WallLength = Mathf.Abs(newhouseHubDetail.DoorD[k + 1].x - newhouseHubDetail.DoorD[k].x);
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                        alllength += dwallDetail.WallLength;
                    }
                    else
                    {
                        WallDetail dwallDetail = new WallDetail();//生成最后一面墙，长度等于墙的总长度减去前面生成的长度
                        dwallDetail.WallLength = wallHubDetail.AllLength - alllength;
                        dwallDetail.WallHeight = WallHight;
                        wallHubDetail.wallDetails.Add(dwallDetail);
                    }

                }
                wallHubDetail.WallStartPos = new Vector3(0.3f, 0, 0);
                wallHubDetail.WallDir = new Vector3(0, 90, 0);
                newhouseHubDetail.wallHubDetails.Add(wallHubDetail);
            }
            houseHubDetails.Add(newhouseHubDetail);
            // if (JsonDataAnylize.instance.rootObject.warehouse[i].door)
            CreateHouse(i);
            housePosLength += newhouseHubDetail.WallD + 1;
        }

    }
}
[System.Serializable]
public class WallHubDetail
{
    public List<WallDetail> wallDetails = new List<WallDetail>();
    public Vector3 WallDir;
    public Vector3 WallStartPos;
    public float AllLength;

}
[System.Serializable]
public class HouseHubDetail
{
    public List<WallHubDetail> wallHubDetails = new List<WallHubDetail>();
    public List<BarrierDetail> barrierDetails = new List<BarrierDetail>();
    public List<ShelfDetail> shelfDetails = new List<ShelfDetail>();
    public List<BinDetail> binDetails = new List<BinDetail>();
    public List<Transform> Wall = new List<Transform>();
    public Vector3 HousePostion;
    public List<Vector4> DoorL = new List<Vector4>();
    public List<Vector4> DoorR = new List<Vector4>();
    public List<Vector4> DoorU = new List<Vector4>();
    public List<Vector4> DoorD = new List<Vector4>();
    public float WallL;
    public float WallR;
    public float WallU;
    public float WallD;
}
[System.Serializable]
public class BarrierDetail
{
    public string name;
    public Vector3 pos, scale;
    public GameObject model;
}
public class ShelfDetail
{
    public string name;
    public Vector3 pos, scale;
    public GameObject model;
    public Shelf shelf;
}
public class BinDetail
{
    public string name;
    public Vector3 pos, scale;
    public GameObject model;
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

