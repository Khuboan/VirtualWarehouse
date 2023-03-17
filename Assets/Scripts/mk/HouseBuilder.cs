using UnityEngine;

public class HouseBuilder : MonoBehaviour
{
    public GameObject[] modules;  // �洢ģ�黯ģ�͵�����
    public Vector3 warehouseSize; // �ֿ�Ĵ�С

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

        // ����ÿ��ģ��Ĵ�С
        if (modules.Length > 0)
        {
            MeshFilter filter = modules[0].GetComponent<MeshFilter>();
            if (filter != null)
            {
                moduleSize = filter.sharedMesh.bounds.size;
                Debug.Log("???");
            }
        }

        // ����ÿ��/��/����Է��ö��ٸ�ģ��
        int rowCount = Mathf.FloorToInt(warehouseSize.x / moduleSize.x);
        int colCount = Mathf.FloorToInt(warehouseSize.y / moduleSize.y);
        int layerCount = Mathf.FloorToInt(warehouseSize.z / moduleSize.z);

        // ����ֿ���ÿ��ģ���λ��
        Vector3 modulePosition = Vector3.zero;
        for (int l = 0; l < layerCount; l++)
        {
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    // ���ѡ��һ��ģ��
                    GameObject module = modules[Random.Range(0, modules.Length)];

                    // ʵ����ģ��
                    GameObject instance = Instantiate(module, transform);

                    // ����ģ���λ��
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
