using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes m_axes = RotationAxes.MouseXAndY;
    [Header("鼠标灵敏度")]
    //XY轴灵敏度
    public float m_sensitivityX = 10f;
    public float m_sensitivityY = 10f;

    public bool CanMove = false;

    // 水平方向的 镜头转向
    [Header("水平转向限制")]
    public float m_minimumX = -360f;
    public float m_maximumX = 360f;

    // 垂直方向的 镜头转向 (这里给个限度 最大仰角为45°)
    [Header("高低转向限制")]
    public float m_minimumY = -45f;
    public float m_maximumY = 45f;

    // 摄像机缩放灵敏度 最大最小缩放
    //public float MouseWheelSensitivity = 5;
    [Header("镜头移动幅度")]
    public float minView = 4f;
    public float maxView = 7.5f;

    //记录鼠标移动数据
    float m_rotationY = 0f;
    float m_rotationX = 0f;
    [Header("镜头移动速率")]
    public float distance;
    //控制摄像机移动的速率
    public float speed = 1f;


    public List<Transform> targetPoint;

    [Header("摄像机平移速度")]
    public float moveSpeed = 10f;
    void Start()
    {
        // 防止 刚体影响 镜头旋转
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {
        //按下鼠标左键
        if (Input.GetMouseButton(0))
        {
            CanMove = !CanMove;
        }
        else
        {
            CanMove = false;
        }

        CameraZoom();

        CameraRotation();

        //CameraMove();
    }
    /// <summary>
    /// 摄像机旋转视角
    /// </summary>
    private void CameraRotation()
    {
        if (CanMove)
        {
            if (m_axes == RotationAxes.MouseXAndY)
            {
                m_rotationX += Input.GetAxis("Mouse X") * m_sensitivityX;
                m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
                //旋转幅度限制
                m_rotationX = Mathf.Clamp(m_rotationX, m_minimumX, m_maximumX);
                m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

                transform.localEulerAngles = new Vector3(-m_rotationY, m_rotationX, 0);
            }
            else if (m_axes == RotationAxes.MouseX)
            {
                m_rotationX += Input.GetAxis("Mouse X") * m_sensitivityX;
                m_rotationX = Mathf.Clamp(m_rotationX, m_minimumX, m_maximumX);

                transform.localEulerAngles = new Vector3(0, m_rotationX, 0);
            }
            else
            {
                m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
                m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);
                transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
    /// <summary>
    /// 摄像机滚轮先后移动
    /// </summary>
    private void CameraZoom()
    {
        float mouseCenter = Input.GetAxis("Mouse ScrollWheel");

        //鼠标滑动中键滚轮,实现摄像机的镜头前后滑动
        //mouseCenter < 0 = 负数 往后滑动
        if (mouseCenter < 0)
        {
            //滑动限制
            if (transform.position.x <= maxView)
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            //mouseCenter >0 = 正数 往前滑动
        }
        else if (mouseCenter > 0)
        {
            //滑动限制
            if (transform.position.x >= minView)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }

    IEnumerator Ien;
    /// <summary>
    /// 点击左键按钮
    /// </summary>
    public void CameraLeft()
    {
        if (Ien != null)
        {
            StopCoroutine(Ien);
        }
        float camZ = transform.position.z;
        for (int i = targetPoint.Count - 1; i >= 0; i--)
        {
            Ien = toLeft(i);
            StartCoroutine(Ien);
            if (transform.position.z != camZ)
            {
                break;
            }
        }
    }
    /// <summary>
    /// 点击右键按钮
    /// </summary>
    public void CameraRight()
    {
        if (Ien != null)
        {
            StopCoroutine(Ien);
        }
        float camZ = transform.position.z;
        for (int i = 0; i < targetPoint.Count; i++)
        {
            Ien = toRight(i);
            StartCoroutine(Ien);
            if (transform.position.z != camZ)
            {
                break;
            }
        }
    }


    /// <summary>
    /// 右键控制摄像机滑动
    /// </summary>
    private void CameraMove()
    {
        if (Input.GetMouseButton(1))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -24, 23),
                Mathf.Clamp(transform.position.y, -24, 23), Mathf.Clamp(transform.position.z, -24, 23));
            transform.Translate(Vector3.back * Input.GetAxis("Mouse X") * speed * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime, Space.World);
        }
    }
    /// <summary>
    /// 向右平滑过度
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    IEnumerator toRight(int x)
    {
        while (targetPoint[x].transform.position.z > transform.position.z)
        {
            transform.position = Vector3.Lerp(transform.position, targetPoint[x].position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    /// <summary>
    /// 向左平滑过度
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    IEnumerator toLeft(int x)
    {
        while (targetPoint[x].transform.position.z < transform.position.z)
        {
            transform.position = Vector3.Lerp(transform.position, targetPoint[x].position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
