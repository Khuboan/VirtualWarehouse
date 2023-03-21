using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxUVOffset : MonoBehaviour
{
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentScale = transform.localScale;
        Vector2 textureScale = new Vector2(currentScale.x / initialScale.x, currentScale.y / initialScale.y);
        GetComponent<Renderer>().material.mainTextureScale = textureScale;
    }
}
