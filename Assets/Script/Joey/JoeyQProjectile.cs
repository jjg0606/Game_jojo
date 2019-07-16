using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeyQProjectile : MonoBehaviour
{
    private Transform tr;


    [HideInInspector]public  Vector3 direction   =   Vector3.zero;
    [HideInInspector]public  float   rangeLeft   =   0.0f;
    [HideInInspector]public  float   velocity;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
     
    }
}
