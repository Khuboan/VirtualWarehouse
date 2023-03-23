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
    /// 点击按钮返回至货架信息窗口
    /// </summary>
    public void ClickBackButton()
    {
        WindowAnimator.SetBool("isClose", false);
    }
}
