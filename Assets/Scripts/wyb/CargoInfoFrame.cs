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
    public bool isCloseTip = false;  //�Ƿ�Ҫ�ر���ʾ��
    public bool isOpenTip = false;  //�Ƿ�Ҫ����ʾ��
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
            //�������������������������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Vector3 TipPos = hitInfo.transform.position + new Vector3(0.3f, 0.8f, 0);
                Quaternion hitRot = hitInfo.transform.rotation;

                //��������ײĿ��ΪBox���͵���Ʒ
                if (hitInfo.collider.tag == "Box")
                {
                    
                    //�ٴε����Box
                    if (HitObj == hitInfo.transform.gameObject)
                    {

                    }
                    else
                    {
                        isOpenTip = true;
                        if (HitObj != null)
                        {
                            //�رո���
                            HitObj.GetComponent<Outline>().enabled = false;
                        }
                        //��ʾ����
                       // Debug.Log("�����������");
                        hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                       // Debug.Log("���ӵ��� = " + hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex + "   ������� = " + CameraController.instance.camposIndex);
                        
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
                    
                    //�洢�����Box
                    HitObj = hitInfo.transform.gameObject;
                }
                else
                {
                    if (HitObj != null)
                    {
                        //�رո���
                        HitObj.GetComponent<Outline>().enabled = false;
                        //�ر���ʾ��
                        isCloseTip = true;
                    }
                    HitObj = null;
                }
            }
        }
    }
}