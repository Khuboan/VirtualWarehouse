using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject LightButton;

    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        LightButton.SetActive(true);
    }
    public void OnMouseUp()
    {
        LightButton.SetActive(false);
    }
}
