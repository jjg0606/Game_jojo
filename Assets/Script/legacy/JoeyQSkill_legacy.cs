//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

// legacy
//public class JoeyQSkill : MonoBehaviour
//{
//    #region Variables
//    private Transform joeyTr;

//    public GameObject skillRangeObject;
//    public int SkillRange;
//    private readonly int defaultSkillRange = 15;
//    private Transform srTr;

//    //public GameObject projectile;
//    public GameObject projectilePrefab;
//    public float projectileVelocity;
//    private GameObject projectileObject;
//    private Transform prTr;
//    private JoeyQProjectile qScript;
    
//    private Camera cam;
//    private Vector3 offsetVector = new Vector3(0, 8.0f, 0); // offset from ground

//    private bool isAvailable = true;
//    public bool isAvail
//    {
//        get { return isAvailable; }
//    }
//    private bool isActiveNow = false;
//    public bool isActive
//    {
//        get { return isActiveNow; }
//    }
       
//    /// <summary>
//    /// Delegate to call skills end
//    /// </summary>
//    private GDele.vv endskill;
//    public GDele.vv endSkill
//    {
//        set
//        {
//            endskill = value;
//        }
//    }
    
//    private Vector3 toMove = Vector3.zero;
//    private Vector3 toMoveDirection;
//    private float lengthleft;
    
    
//    enum QState
//    {
//        notborn,
//        firstmove,
//        secondmove
//    }
//    /// <summary>
//    /// state of this skill
//    /// </summary>
//    private QState qState = QState.notborn;
//    #endregion

//    void Start()
//    {
//        cam = Camera.main;

//        joeyTr = GetComponent<Transform>();

//        srTr = skillRangeObject.GetComponent<Transform>();
//        srTr.localScale = new Vector3(SkillRange / defaultSkillRange, 1, SkillRange / defaultSkillRange);
//        skillRangeObject.SetActive(false);

//        projectileObject = Instantiate<GameObject>(projectilePrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
//        prTr = projectileObject.GetComponent<Transform>();
//        qScript = projectileObject.GetComponent<JoeyQProjectile>();
//        projectileObject.SetActive(false);        
//    }

//    public void ActiveSkill()
//    {
//        isActiveNow = true;
//        qState = QState.notborn;
//        skillRangeObject.SetActive(true);
//    }

//    public void DeActiveSkill()
//    {
//        isActiveNow = false;
//        skillRangeObject.SetActive(false);
//        projectileObject.SetActive(false);
//    }

//    private void LateUpdate()
//    {
//        if (!isActiveNow)
//        {
//            return;
//        }

//        switch(qState)
//        {
//            case QState.notborn:
//                IndicateRange();
//                break;
//            case QState.firstmove:
//                ControlShotOne();
//                break;
//            case QState.secondmove:
//                ControlShotTwo();
//                break;
//        }
//    }

//    private void IndicateRange()
//    {
//        srTr.position = joeyTr.position;
//        RaycastHit hitinfo;
//        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
//        {
//            Vector3 direction = hitinfo.point - joeyTr.position;
//            direction.y = 0;
//            direction = direction == Vector3.zero ? Vector3.forward : direction;

//            srTr.rotation = Quaternion.LookRotation(Vector3.RotateTowards(srTr.forward, direction, 1.0f, 0.0f));
//        }
//    }

//    private void ControlShotOne()
//    {
//        Vector3 direction = toMove - prTr.position;

//        if (direction.magnitude < projectileVelocity * Time.deltaTime)
//        {
//            prTr.position = toMove;
//        }
//        else
//        {
//            prTr.position += direction.normalized * projectileVelocity * Time.deltaTime;
//        }
//    }

//    private void ControlShotTwo()
//    {
//        Vector3 direction = toMove - prTr.position;
//        if(direction == Vector3.zero)
//        {
//            DeActiveSkill();
//            endskill();
//        }
//        else if (direction.magnitude < projectileVelocity * Time.deltaTime)
//        {
//            prTr.position = toMove;            
//        }
//        else
//        {
//            prTr.position += direction.normalized * projectileVelocity * Time.deltaTime;
//        }
//    }





//    public void shotSkill()
//    {
//        RaycastHit hitinfo;
//        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
//        {
//            switch(this.qState)
//            {
//                case QState.notborn:
//                    SetShotOne(hitinfo.point);
//                    break;
//                case QState.firstmove:
//                    SetShotTwo(hitinfo.point);
//                    break;

//            }
//        }
//    }

//    private void SetShotOne(Vector3 point)
//    {
//        toMove = point - joeyTr.position;
//        toMove.y = 0;
//        toMove = toMove.normalized;
//        toMove = toMove == Vector3.zero ? Vector3.forward : toMove;


//        skillRangeObject.SetActive(false);
//        projectileObject.SetActive(true);
//        prTr.position = new Vector3(joeyTr.position.x, offsetVector.y, joeyTr.position.z);

//        this.qState = QState.firstmove;
//        toMove = SkillRange * toMove + offsetVector + new Vector3(joeyTr.position.x, 0, joeyTr.position.z);
//    }

//    private void SetShotTwo(Vector3 point)
//    {
//        if((point - joeyTr.position).magnitude < SkillRange)
//        {
//            toMove = point;
//            toMove.y = 0;
//            toMove += offsetVector;
//        }
//        else
//        {
//            toMove = point - joeyTr.position;
//            toMove.y = 0;
//            toMove = toMove.normalized;
//            toMove = toMove == Vector3.zero ? Vector3.forward : toMove;
//            toMove = SkillRange * toMove + offsetVector + new Vector3(joeyTr.position.x, 0, joeyTr.position.z);
//        }      

//        this.qState = QState.secondmove;
        
//    }
//}
