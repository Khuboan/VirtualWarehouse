using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public bool isObstacle;
    public string collname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Åö×²µ½ÁË" + other.gameObject.name);
        if (other.gameObject.tag == "Wall")
        {
            collname = other.gameObject.transform.parent.gameObject.name;
            isObstacle = true;
        }
    }
}
