using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeyMove : MonoBehaviour
{
    [HideInInspector]
    public Vector3 toMove;
    public float velocity;

    private string ap_speed = "speed";

    private Animator anim;
    private Transform tr;

    private Camera cam;

    public GameObject clickEffect;
    private ParticleSystem clickParticle;
    private Vector3 EffectDiff = new Vector3(0, 0.7f, 0);


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        toMove = tr.position;

        cam = Camera.main;

        clickParticle = clickEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hitinfo;

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
            {
                clickParticle.Stop();
                clickEffect.transform.position = hitinfo.point + EffectDiff;
                clickParticle.Play();
                this.toMove = hitinfo.point;
            }
        }


        if(tr.position == toMove)
        {
            anim.SetFloat(ap_speed, 0.0f);
            return;
        }

        Vector3 direction = toMove - tr.position;
        tr.rotation = Quaternion.LookRotation(Vector3.RotateTowards(tr.forward, direction, 0.1f, 0.0f));

        if (direction.magnitude < velocity*Time.deltaTime)
        {
            tr.position = toMove;
        }
        else
        {
            tr.position += direction.normalized * velocity * Time.deltaTime;
        }        
        anim.SetFloat(ap_speed, 1.0f);
    }
}
