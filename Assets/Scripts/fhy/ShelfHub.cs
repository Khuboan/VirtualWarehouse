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
    /// <summary>
    /// ÿ�����ӵ�ռ�س��ȣ����ڼ���ÿ�������������ķŵ���һ��
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

