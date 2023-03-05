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
    [Header("���������")]
    //XY��������
    public float m_sensitivityX = 10f;
    public float m_sensitivityY = 10f;

    public bool CanMove = false;

    // ˮƽ����� ��ͷת��
    [Header("ˮƽת������")]
    public float m_minimumX = -360f;
    public float m_maximumX = 360f;

    // ��ֱ����� ��ͷת�� (��������޶� �������Ϊ45��)
    [Header("�ߵ�ת������")]
    public float m_minimumY = -45f;
    public float m_maximumY = 45f;

    // ��������������� �����С����
    //public float MouseWheelSensitivity = 5;
    [Header("��ͷ�ƶ�����")]
    public float minView = 4f;
    public float maxView = 7.5f;

    //��¼����ƶ�����
    float m_rotationY = 0f;
    float m_rotationX = 0f;
    [Header("��ͷ�ƶ�����")]
    public float distance;
    //����������ƶ�������
    public float speed = 1f;


    public List<Transform> targetPoint;

    [Header("�����ƽ���ٶ�")]
    public float moveSpeed = 10f;
    void Start()
    {
        // ��ֹ ����Ӱ�� ��ͷ��ת
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {
        //����������
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
    /// �������ת�ӽ�
    /// </summary>
    private void CameraRotation()
    {
        if (CanMove)
        {
            if (m_axes == RotationAxes.MouseXAndY)
            {
                m_rotationX += Input.GetAxis("Mouse X") * m_sensitivityX;
                m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
                //��ת��������
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
    /// ����������Ⱥ��ƶ�
    /// </summary>
    private void CameraZoom()
    {
        float mouseCenter = Input.GetAxis("Mouse ScrollWheel");

        //��껬���м�����,ʵ��������ľ�ͷǰ�󻬶�
        //mouseCenter < 0 = ���� ���󻬶�
        if (mouseCenter < 0)
        {
            //��������
            if (transform.position.x <= maxView)
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            //mouseCenter >0 = ���� ��ǰ����
        }
        else if (mouseCenter > 0)
        {
            //��������
            if (transform.position.x >= minView)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }

    IEnumerator Ien;
    /// <summary>
    /// ��������ť
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
    /// ����Ҽ���ť
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
    /// �Ҽ��������������
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
    /// ����ƽ������
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
    /// ����ƽ������
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
