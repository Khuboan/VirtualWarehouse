using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CargoInfoFrame : MonoBehaviour
{
    //public GameObject TipUI;
    //public LineRenderer Line;
    public float OneExecutePer = 0;

    private GameObject HitObj;
    public bool isCloseTip = false;  //是否要关闭提示板
    public bool isOpenTip = false;  //是否要打开提示板
    private Animator WindowAnimator;
    public static string goButtonText;
    private void Start()
    {
        WindowAnimator = GameObject.Find("UI").GetComponent<Animator>();
    }

    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            //从摄像机发出到点击坐标的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Vector3 TipPos = hitInfo.transform.position + new Vector3(0.3f, 0.8f, 0);
                Quaternion hitRot = hitInfo.transform.rotation;

                //当射线碰撞目标为Box类型的物品
                if (hitInfo.collider.tag == "Box")
                {
                    
                    //再次点击此Box
                    if (HitObj == hitInfo.transform.gameObject)
                    {

                    }
                    else
                    {
                        isOpenTip = true;
                        if (HitObj != null)
                        {
                            //关闭高亮
                            HitObj.GetComponent<Outline>().enabled = false;
                        }
                        //显示高亮
                       // Debug.Log("点击到箱子了");
                        hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                       // Debug.Log("箱子的数 = " + hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex + "   相机的数 = " + CameraController.instance.camposIndex);
                        
                        try
                        {

                            if (hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex != CameraController.instance.camposIndex)
                            {
                                CameraController.instance.camposIndex = CameraController.instance.camposIndex = hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex;
                                CameraController.instance.CameraMoveTime = 0;
                                CameraController.instance.isMoveDone = false;
                                CameraController.instance.campos = CameraController.instance.transform.position;
                            }
                            MaterialInfoGet.materialInfoGet.GetInfo(hitInfo.collider.gameObject.GetComponent<ShelfObject>().Floor + 1, hitInfo.collider.gameObject.GetComponent<ShelfObject>().Num + 1);

                        }
                        catch
                        {
                            if (hitInfo.collider.gameObject.GetComponent<BinObject>().CamPosIndex != CameraController.instance.camposIndex)
                            {
                                CameraController.instance.camposIndex = CameraController.instance.camposIndex = hitInfo.collider.gameObject.GetComponent<BinObject>().CamPosIndex;
                                CameraController.instance.CameraMoveTime = 0;
                                CameraController.instance.isMoveDone = false;
                                CameraController.instance.campos = CameraController.instance.transform.position;
                            }
                            MaterialInfoGet.materialInfoGet.GetInfo(hitInfo.collider.gameObject.GetComponent<BinObject>().Num + 1);

                        }



                        WindowAnimator.SetBool("isClose", true);
                    }
                    
                    //存储点击的Box
                    HitObj = hitInfo.transform.gameObject;
                }
                else
                {
                    if (HitObj != null)
                    {
                        //关闭高亮
                        HitObj.GetComponent<Outline>().enabled = false;
                        //关闭提示牌
                        isCloseTip = true;
                    }
                    HitObj = null;
                }
            }
        }
    }
}