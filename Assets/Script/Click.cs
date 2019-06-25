using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public GameObject ClickEffect;
    public GameObject RClickEffect;

    private ParticleSystem unitParticle;
    private ParticleSystem RClickParticle;
    private Vector3 diffVector = new Vector3(0, 0.7f, 0);

    private Camera cam;

    private JoeyMove joeyMove;
    private JoeyBallCtrl joeyBctrl;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        unitParticle = ClickEffect.GetComponent<ParticleSystem>();
        RClickParticle = RClickEffect.GetComponent<ParticleSystem>();
        joeyMove = GetComponent<JoeyMove>();
        joeyBctrl = GetComponent<JoeyBallCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitinfo;

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
            {
                unitParticle.Stop();
                ClickEffect.transform.position = hitinfo.point + diffVector;
                unitParticle.Play();
                joeyMove.toMove = hitinfo.point;
            }

            
        }

        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hitinfo;

            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
            {
                RClickParticle.Stop();
                RClickEffect.transform.position = hitinfo.point + diffVector;
                RClickParticle.Play();
                joeyBctrl.toMove = hitinfo.point;
            }
        }
    }
}
