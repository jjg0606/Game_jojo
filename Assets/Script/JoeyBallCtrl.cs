using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeyBallCtrl : MonoBehaviour
{
    public GameObject ball;
    public float velocity;

    private float ballYoffset = 12.8f;

    private Vector3 toMoveVec;
    [HideInInspector]
    public Vector3 toMove
    {
        get
        {
            return toMoveVec;
        }
        set
        {
            toMoveVec = value;
        }
    }

    private Transform tr;
    private Transform btr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        btr = ball.GetComponent<Transform>();
        toMoveVec = tr.position;
        btr.position = tr.position;
        btr.position = new Vector3(btr.position.x, ballYoffset, btr.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (btr.position == toMoveVec)
        {
            return;
        }

        Vector3 direction = toMoveVec - btr.position;
        direction.y = 0;
        if (direction.magnitude < velocity * Time.deltaTime)
        {
            btr.position = toMoveVec;
        }
        else
        {
            btr.position += direction.normalized * velocity * Time.deltaTime;
        }

        btr.position = new Vector3(btr.position.x, ballYoffset, btr.position.z);
    }
}
