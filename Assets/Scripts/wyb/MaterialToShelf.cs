using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialToShelf : MonoBehaviour
{
    private Animator WindowAnimator;
    
    private void Start()
    {
        WindowAnimator = GameObject.Find("UI").GetComponent<Animator>();
    }
    /// <summary>
    /// �����ť������������Ϣ����
    /// </summary>
    public void ClickBackButton()
    {
        WindowAnimator.SetBool("isClose", false);
    }
}
