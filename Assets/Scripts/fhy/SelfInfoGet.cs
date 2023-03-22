using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfInfoGet : MonoBehaviour
{
    public Text ShelfName, ShelfIndex, ShelfFloor;
    ///public string ShelfNameValue;
    //public int ShelfIndexValue, ShelfFloorValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShelfHub shelfHub = CameraController.instance.targetPoint[CameraController.instance.camposIndex].parent.parent.GetComponent<ShelfHub>();
        ShelfName.text = shelfHub.shelf.name;
        ShelfFloor.text = shelfHub.shelf.floor.Count.ToString();
        ShelfIndex.text = CameraController.instance.camposIndex.ToString();
    }
}
