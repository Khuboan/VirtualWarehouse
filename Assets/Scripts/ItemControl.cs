using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    /// <summary>
    /// 总货物集合
    /// </summary>
    public List<Goods> goods;
    /// <summary>
    /// 射线选中的货物
    /// </summary>
    public int SelectGoodIndex;
    /// <summary>
    /// 货架
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
/// 货品类
/// </summary>
[System.Serializable]
public class Goods
{
    public string name;
    public GoodTypes goodTypes;
    /// <summary>
    /// 计量单位
    /// </summary>
    public string Units;
    /// <summary>
    /// 每单位数量
    /// </summary>
    public int UnitOfQuantity;
    /// <summary>
    /// 库存数量
    /// </summary>
    public int QuantityInStock;
    /// <summary>
    /// 所属仓库
    /// </summary>
    public string Warehouse;
    public GameObject GoodsModel;
}
/// <summary>
/// 物料类型
/// </summary>
public enum GoodTypes
{
    /// <summary>
    /// 劳保用品
    /// </summary>
    labour,
    /// <summary>
    /// 原料
    /// </summary>
    raw,
    /// <summary>
    /// 耗材
    /// </summary>
    consumable,
    /// <summary>
    /// 包材
    /// </summary>
    packing,
    /// <summary>
    /// 贵重物品
    /// </summary>
    valuable,
    /// <summary>
    /// 危险品
    /// </summary>
    hazardous,
    /// <summary>
    /// 产品
    /// </summary>
    product,

}
/// <summary>
/// 货架类
/// </summary>
[System.Serializable]
public class GoodsSelf
{
    public string name;
    public GoodTypes goodTypes;
    /// <summary>
    /// 总数量
    /// </summary>
    public int Gross;
    public List<Goods> goods;
    /// <summary>
    /// 货架模型
    /// </summary>
    public GameObject GoodsSelfModel;
}
