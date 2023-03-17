using UnityEngine;

public class HouseBuilder : MonoBehaviour
{
    public GameObject[] modules;  // 存储模块化模型的数组
    public Vector3 warehouseSize; // 仓库的大小

    private void Start()
    {
        
    }
    private void Update()
    {
        GenerateWarehouse();
        
    }

    private void GenerateWarehouse()
    {
       
        Vector3 moduleSize = Vector3.zero;

        // 计算每个模块的大小
        if (modules.Length > 0)
        {
            MeshFilter filter = modules[0].GetComponent<MeshFilter>();
            if (filter != null)
            {
                moduleSize = filter.sharedMesh.bounds.size;
                Debug.Log("???");
            }
        }

        // 计算每行/列/层可以放置多少个模块
        int rowCount = Mathf.FloorToInt(warehouseSize.x / moduleSize.x);
        int colCount = Mathf.FloorToInt(warehouseSize.y / moduleSize.y);
        int layerCount = Mathf.FloorToInt(warehouseSize.z / moduleSize.z);

        // 计算仓库中每个模块的位置
        Vector3 modulePosition = Vector3.zero;
        for (int l = 0; l < layerCount; l++)
        {
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    // 随机选择一个模块
                    GameObject module = modules[Random.Range(0, modules.Length)];

                    // 实例化模块
                    GameObject instance = Instantiate(module, transform);

                    // 设置模块的位置
                    modulePosition.x = r * moduleSize.x - warehouseSize.x / 2f + moduleSize.x / 2f;
                    modulePosition.y = c * moduleSize.y - warehouseSize.y / 2f + moduleSize.y / 2f;
                    modulePosition.z = l * moduleSize.z - warehouseSize.z / 2f + moduleSize.z / 2f;
                    instance.transform.localPosition = modulePosition;
                    Debug.Log("zhixing");
                }
            }
        }
    }
}
