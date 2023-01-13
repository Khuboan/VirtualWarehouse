using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    /// <summary>
    /// �ܻ��Ｏ��
    /// </summary>
    public List<Goods> goods;
    /// <summary>
    /// ����ѡ�еĻ���
    /// </summary>
    public int SelectGoodIndex;
    /// <summary>
    /// ����
    /// </summary>
    public List<GoodsSelf> GoodsSlefs;
    public int goodsnum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
/// <summary>
/// ��Ʒ��
/// </summary>
[System.Serializable]
public class Goods
{
    public string name;
    public GoodTypes goodTypes;
    /// <summary>
    /// ������λ
    /// </summary>
    public string Units;
    /// <summary>
    /// ÿ��λ����
    /// </summary>
    public int UnitOfQuantity;
    /// <summary>
    /// �������
    /// </summary>
    public int QuantityInStock;
    /// <summary>
    /// �����ֿ�
    /// </summary>
    public string Warehouse;
    public GameObject GoodsModel;
}
/// <summary>
/// ��������
/// </summary>
public enum GoodTypes
{
    /// <summary>
    /// �ͱ���Ʒ
    /// </summary>
    labour,
    /// <summary>
    /// ԭ��
    /// </summary>
    raw,
    /// <summary>
    /// �Ĳ�
    /// </summary>
    consumable,
    /// <summary>
    /// ����
    /// </summary>
    packing,
    /// <summary>
    /// ������Ʒ
    /// </summary>
    valuable,
    /// <summary>
    /// Σ��Ʒ
    /// </summary>
    hazardous,
    /// <summary>
    /// ��Ʒ
    /// </summary>
    product,

}
/// <summary>
/// ������
/// </summary>
[System.Serializable]
public class GoodsSelf
{
    public string name;
    public GoodTypes goodTypes;
    /// <summary>
    /// ������
    /// </summary>
    public int Gross;
    public List<Goods> goods;
    /// <summary>
    /// ����ģ��
    /// </summary>
    public GameObject GoodsSelfModel;
}
