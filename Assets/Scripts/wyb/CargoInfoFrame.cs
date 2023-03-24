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



    private void Update()
    {
        //if (HitObj != null)
        //{
        //    //ʵʱ����
        //    DrawLine(HitObj.transform.position, TipUI.transform.position + new Vector3(0, 0, 1.8f));
        //}

        ////��ʾ��Ŵ�
        //if (isOpenTip == true)
        //{
        //    Vector3 endScale = new Vector3(7, 7, 7);
        //    AdjustTip(TipUI.transform.localScale, endScale, OneExecutePer);
        //    if (TipUI.transform.localScale == endScale)
        //    {
        //        isOpenTip = false;
        //    }
        //}
        ////��ʾ����С
        //if (isCloseTip == true)
        //{
        //    Vector3 endScale = new Vector3(7, 0, 0);
        //    AdjustTip(TipUI.transform.localScale, endScale, OneExecutePer);
        //    if (TipUI.transform.localScale == endScale)
        //    {
        //        isCloseTip = false;
        //        TipUI.SetActive(false);
        //    }
        //}


        if (Input.GetMouseButtonDown(0))
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
                        //����ʾ��
                       // TipUI.SetActive(true);
                        if (HitObj != null)
                        {
                            //�رո���
                            HitObj.GetComponent<Outline>().enabled = false;
                        }
                        //��ʾ����
                        Debug.Log("�����������");
                        hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                        Debug.Log("���ӵ��� = " + hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex + "   ������� = " + CameraController.instance.camposIndex);
                        if(hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex!=CameraController.instance.camposIndex)
                        {
                            CameraController.instance.camposIndex = CameraController.instance.camposIndex = hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex;
                            CameraController.instance.CameraMoveTime = 0;
                            CameraController.instance.isMoveDone = false;
                            CameraController.instance.campos = CameraController.instance.transform.position;
                        }
                        //��ʾ��ͻ���仮��
                        // Line.enabled = true;
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
                        //ɾ����
                       // Line.enabled = false;
                    }
                    HitObj = null;
                }
            }
        }


        
    }
    //���߹���
    private void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        //�Ƿ�ʹ����������
        //Line.useWorldSpace = true;
        //���ÿ�ʼ�ͽ���λ��0�����һ���㣬1����ڶ�����
       // Line.SetPosition(0, startPos);
       // Line.SetPosition(1, endPos);
        //��ʼ�ͽ���λ�õ���ɫ
        //Line.startColor = new Color(1, 0.8F, 0.1F, 0);
        //Line.endColor = Color.red;
        //��ʼ�ͽ�����ϸ��С
        //Line.startWidth = 0.02f;
        //Line.endWidth = 0.015f;
    }

    /// <summary>
    /// ��̬�ı���ʾ���С
    /// </summary>
    /// <param name="startScale"></param>
    /// <param name="endScale"></param>
    /// <param name="oneExecutePer"></param>
    /// <returns></returns>
    private void AdjustTip(Vector3 startScale, Vector3 endScale, float oneExecutePer)
    {
       // TipUI.transform.localScale = Vector3.Lerp(startScale, endScale, oneExecutePer);
        //yield return null;
    }
}