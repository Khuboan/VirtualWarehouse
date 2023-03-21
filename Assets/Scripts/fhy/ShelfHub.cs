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
    /// <summary>
    /// 每个箱子的占地长度，用于计算每层的数量，多余的放到上一层
    /// </summary>
    public float BoxLength;
    // Start is called before the first frame update
    void Start()
    {
        
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

