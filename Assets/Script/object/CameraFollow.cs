using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject followObject;

    private Transform followTr;
    private Transform camTr;

    private Vector3 offsetVector=new Vector3(0,34,-34);
    // Start is called before the first frame update
    void Start()
    {
        camTr = GetComponent<Transform>();
        followTr = followObject.GetComponent<Transform>();

    }

    private void LateUpdate()
    {
        camTr.position = Vector3.Lerp(camTr.position,followTr.position+offsetVector,0.5f);
    }
}
