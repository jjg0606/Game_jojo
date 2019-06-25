using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeyQSkill : MonoBehaviour,SkillInterface
{
    public GameObject skillRangeObject;
    public GameObject projectile;
    public float projectileVelocity;

    public int SkillRange;
    private readonly int defaultSkillRange = 15;

    private Transform joeyTr;
    private Transform srTr;
    private Transform prTr;

    private Camera cam;
    private Vector3 offsetVector = new Vector3(0, 8.0f, 0);

    private bool isAvailable = true;
    private bool isActiveNow = false;


    private Vector3 toMove = Vector3.zero;
    enum QState
    {
        notborn,
        firstmove,
        secondmove
    }
    private QState qState = QState.notborn;

    public bool isAvail
    {
        get { return isAvailable; }
    }
    public bool isActive
    {
        get { return isActiveNow; }
    }

    void Start()
    {
        cam = Camera.main;
        joeyTr = GetComponent<Transform>();
        srTr = skillRangeObject.GetComponent<Transform>();
        skillRangeObject.SetActive(false);
        prTr = projectile.GetComponent<Transform>();
        projectile.SetActive(false);

        srTr.localScale = new Vector3(SkillRange / defaultSkillRange, 1, SkillRange / defaultSkillRange);
    }

    private void LateUpdate()
    {
        if (!isActiveNow)
        {
            return;
        }

        switch(qState)
        {
            case QState.notborn:
                IndicateRange();
                break;
            case QState.firstmove:
                break;
            case QState.secondmove:
                break;
        }

        Vector3 direction = toMove - prTr.position;

        if (direction.magnitude < projectileVelocity * Time.deltaTime)
        {
            prTr.position = toMove;
        }
        else
        {
            prTr.position += direction.normalized * projectileVelocity * Time.deltaTime;
        }

    }

    private void ControlShotOne()
    {

    }

    private void ControlShotTwo()
    {

    }

    private void IndicateRange()
    {
        srTr.position = joeyTr.position;
        RaycastHit hitinfo;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
        {
            Vector3 direction = hitinfo.point - joeyTr.position;
            direction.y = 0;
            direction = direction == Vector3.zero ? Vector3.forward : direction;

            srTr.rotation = Quaternion.LookRotation(Vector3.RotateTowards(srTr.forward, direction, 1.0f, 0.0f));
        }
    }

    public void ActiveSkill()
    {
        isActiveNow = true;
        qState = QState.notborn;
        skillRangeObject.SetActive(true);
    }

    public void DeActiveSkill()
    {
        isActiveNow = false;
        skillRangeObject.SetActive(false);
        projectile.SetActive(false);
    }

    public void shotSkill()
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
        {
            switch(this.qState)
            {
                case QState.notborn:
                    SetShotOne(hitinfo.point);
                    break;
                case QState.firstmove:
                    SetShotTwo(hitinfo.point);
                    break;

            }
        }
    }

    private void SetShotOne(Vector3 point)
    {
        toMove = point - joeyTr.position;
        toMove.y = 0;
        toMove = toMove.normalized;
        toMove = toMove == Vector3.zero ? Vector3.forward : toMove;


        skillRangeObject.SetActive(false);
        projectile.SetActive(true);
        prTr.position = new Vector3(joeyTr.position.x, offsetVector.y, joeyTr.position.z);

        this.qState = QState.firstmove;
        toMove = SkillRange * toMove + offsetVector + new Vector3(joeyTr.position.x, 0, joeyTr.position.z);
    }

    private void SetShotTwo(Vector3 point)
    {
        Vector3 direction = point - joeyTr.position;
        direction.y = 0;
        direction = direction.normalized;

        this.qState = QState.secondmove;
        toMove = SkillRange * direction + offsetVector;
    }
}
