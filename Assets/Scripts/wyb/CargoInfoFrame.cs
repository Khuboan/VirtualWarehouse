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



    private void Update()
    {
        //if (HitObj != null)
        //{
        //    //实时划线
        //    DrawLine(HitObj.transform.position, TipUI.transform.position + new Vector3(0, 0, 1.8f));
        //}

        ////提示框放大
        //if (isOpenTip == true)
        //{
        //    Vector3 endScale = new Vector3(7, 7, 7);
        //    AdjustTip(TipUI.transform.localScale, endScale, OneExecutePer);
        //    if (TipUI.transform.localScale == endScale)
        //    {
        //        isOpenTip = false;
        //    }
        //}
        ////提示框缩小
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
                        //打开提示牌
                       // TipUI.SetActive(true);
                        if (HitObj != null)
                        {
                            //关闭高亮
                            HitObj.GetComponent<Outline>().enabled = false;
                        }
                        //显示高亮
                        Debug.Log("点击到箱子了");
                        hitInfo.collider.gameObject.GetComponent<Outline>().enabled = true;
                        Debug.Log("箱子的数 = " + hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex + "   相机的数 = " + CameraController.instance.camposIndex);
                        if(hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex!=CameraController.instance.camposIndex)
                        {
                            CameraController.instance.camposIndex = CameraController.instance.camposIndex = hitInfo.collider.gameObject.GetComponent<ShelfObject>().CamPosIndex;
                            CameraController.instance.CameraMoveTime = 0;
                            CameraController.instance.isMoveDone = false;
                            CameraController.instance.campos = CameraController.instance.transform.position;
                        }
                        //提示板和货箱间划线
                        // Line.enabled = true;
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
                        //删除线
                       // Line.enabled = false;
                    }
                    HitObj = null;
                }
            }
        }


        
    }
    //划线功能
    private void DrawLine(Vector3 startPos, Vector3 endPos)
    {
        //是否使用世界坐标
        //Line.useWorldSpace = true;
        //设置开始和结束位置0代表第一个点，1代表第二个点
       // Line.SetPosition(0, startPos);
       // Line.SetPosition(1, endPos);
        //开始和结束位置的颜色
        //Line.startColor = new Color(1, 0.8F, 0.1F, 0);
        //Line.endColor = Color.red;
        //开始和结束粗细大小
        //Line.startWidth = 0.02f;
        //Line.endWidth = 0.015f;
    }

    /// <summary>
    /// 动态改变提示框大小
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