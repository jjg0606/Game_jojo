using System.Collections;
using System.Collections.Generic;
using GDele;
using CEnum.Skill;
using UnityEngine;

public class JoeySkillQ : MonoBehaviour,SkillInterface
{
    #region Enum
    /// <summary>
    /// Q스킬 내부적인 상태머신
    /// </summary>
    private enum Seq
    {
        notActive,      // 스킬이 발동 되기 전
        beforeFirst,    // 스킬 발동 ~ 첫번째 샷 전
        beforeSecond,   // 첫번째 샷 이후 두 번째 샷 전
        secondShotFollowing,  // 두번째 샷 이후 투사체가 조이의 몸을 따라 갈 때 
        secondShotFollowingEnd,   // 두번째 샷 이후, 투사체가 조이를 따라가지 않을 때
    }
    #endregion

    #region Variables
    private State state=State.idle;
    private Seq sequence = Seq.notActive;
    private GDele.vs broadCast;

    private Transform masterTr;

    private Camera maincam;

    public   float              skillRange;
    public   GameObject         skillRangePrefab;    
    private  GameObject         skillRangeObj;
    private  Transform          skillRangeTr;

    public   GameObject         ProjectilePrefab;
    public   float              ProjectileVelocity_first;
    public   float              ProjectileVelocity_second;
    private  GameObject         projectileObj;
    private  Transform          projectileTr;

    private GDele.vv FrameFunction; // Update 에서 수행 할 Function
    private GDele.vv LateFrameFunction;
    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        // initial this
        masterTr = GetComponent<Transform>();
        FrameFunction = Nothing;
        LateFrameFunction = Nothing;
        maincam = Camera.main;
        // instantiate prefab
        skillRangeObj = Instantiate<GameObject>(skillRangePrefab);
        skillRangeObj.SetActive(false);
        projectileObj = Instantiate<GameObject>(ProjectilePrefab);
        projectileObj.SetActive(false);
        // initialize sub object
        skillRangeTr = skillRangeObj.GetComponent<Transform>();
        projectileTr = projectileObj.GetComponent<Transform>();

    }

    void Update()
    {
        FrameFunction();
        GetDebug();
    }

    private void LateUpdate()
    {
        LateFrameFunction();
    }

    #endregion

    #region SkillInterface
    State SkillInterface.skillState
    {
        get { return this.state; }
    }

    GDele.vs SkillInterface.BroadCast
    {
        get { return this.broadCast; }
        set { this.broadCast = value; }
    }


    #endregion

    #region Input Control
    void SkillInterface.InputKeyBoardBtn(KeyCode key)
    {
        // Q키가 아니면 Cancel
        if (key != KeyCode.Q)
        {
            CancelSkill();
            return;
        }

        switch (this.sequence)
        {
            case Seq.notActive:
                //스킬 시작 전 상태 Q를 누름으로써 스킬 활성화
                ActiveSkill();
                break;
            case Seq.beforeFirst:
                //샷 발사 전 범위가 떠 있는 상태, Q를 한번 더 누르면 스킬 취소
                CancelSkill();
                break;
            case Seq.beforeSecond:
                ShotSecond();
                //2번째 샷 발사
                break;
            case Seq.secondShotFollowing:
                //입력과 무관
                break;
            case Seq.secondShotFollowingEnd:
                //입력과 무관
                break;
            default:
                break;
        }
    }

    void SkillInterface.InputMouseBtn(int buttonnum)
    {
        // 좌클릭이 아니면 ignore
        if (buttonnum != 0)
        {
            return;
        }

        switch (this.sequence)
        {
            case Seq.notActive:
                //스킬 시작전 좌클릭 무시
                break;
            case Seq.beforeFirst:
                //스킬 범위가 뜬 상태에서 좌클릭 -> 첫번째 샷 발사
                ShotFirst();
                break;
            case Seq.beforeSecond:
                //첫번째 샷 이후 두번째 샷 발사
                ShotSecond();
                break;
            case Seq.secondShotFollowing:
                //입력과 무관
                break;
            case Seq.secondShotFollowingEnd:
                //입력과 무관
                break;
            default:
                break;
        }
    }

    void SkillInterface.GetBroadCast(string msg)
    {
        //현재로선 상호작용 할 것 없음
    }


    #endregion

    #region TempVariables
    //at first shot
    private Vector3 shotDestination = Vector3.zero;
    //at second shot
    private Vector3 shotDirection = Vector3.zero;
    //at second shot following
    private float leftDistance = 0.0f;
    #endregion

    #region SkillFunction

    /// <summary>
    /// Skill notActive -> beforeFirst
    /// </summary>
    void ActiveSkill()
    {
        // state change
        this.state = State.ActiveNow;
        this.sequence = Seq.beforeFirst;
        // frame function update
        this.FrameFunction = Nothing;
        this.LateFrameFunction = SkillRangeFollow;
        // function
        skillRangeObj.SetActive(true);
        skillRangeObj.transform.position = masterTr.position;
    }

    /// <summary>
    /// beforeFirst -> beforeSecond
    /// </summary>
    void ShotFirst()
    {
        // check mouse position
        RaycastHit hitinfo;
        if (!Physics.Raycast(maincam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
        {
            // mouse clicked wrong thing
            return;
        }
        // function
        projectileObj.SetActive(true);
        projectileTr.position = masterTr.position;
        Vector3 direction = hitinfo.point - masterTr.position;
        direction.y = 0;
        direction = (direction == Vector3.zero) ? Vector3.forward : direction;
        this.shotDestination = direction.normalized * this.skillRange + masterTr.position;

        // state change
        this.sequence = Seq.beforeSecond;
        // frame function update
        this.FrameFunction = ControlFirstShot;
        this.LateFrameFunction = Nothing;
        // 
        this.skillRangeObj.SetActive(false);
    }

    void ShotSecond()
    {
        // check mouse position
        RaycastHit hitinfo;
        if (!Physics.Raycast(maincam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
        {
            // mouse clicked wrong thing
            return;
        }
        // function
        Vector3 direction = hitinfo.point - projectileTr.position;
        direction.y = 0;
        direction = (direction == Vector3.zero) ? Vector3.forward : direction;
        this.shotDirection = direction.normalized;
        // state change
        this.sequence = Seq.secondShotFollowing;
        // frame function update
        this.FrameFunction = ControlSecondShot;
        this.LateFrameFunction = Nothing;
    }
    #endregion

    #region FrameFunction
    void Nothing()
    {
        // Nothing to do
    }

    void SkillRangeFollow()
    {
        skillRangeTr.position = masterTr.position;

        RaycastHit hitinfo;
        if (!Physics.Raycast(maincam.ScreenPointToRay(Input.mousePosition), out hitinfo, 200.0f, Constants.groundLayerMask))
        {
            // mouse clicked wrong thing
            return;
        }

        Vector3 direction = hitinfo.point - masterTr.position;
        direction.y = 0;
        direction = (direction == Vector3.zero) ? Vector3.forward : direction;
        skillRangeTr.rotation = Quaternion.LookRotation(Vector3.RotateTowards(skillRangeTr.forward,direction,1.0f,0.0f));

    }

    void ControlFirstShot()
    {
        Vector3 direction = this.shotDestination - projectileTr.position;
        if(direction.magnitude <= this.ProjectileVelocity_first*Time.deltaTime)
        {
            // get destination
            projectileTr.position = this.shotDestination;
            // first shot end state change
            this.sequence = Seq.beforeSecond;
            return;
        }
        
        // following
        projectileTr.position += direction.normalized * this.ProjectileVelocity_first * Time.deltaTime;

    }

    void ControlSecondShot()
    {
        Vector3 joeyVector = masterTr.position - projectileTr.position;
        if(Vector3.Dot(joeyVector,this.shotDirection)<=0)
        {
            // following end
            // set variable
            this.leftDistance = skillRange;
            // state change
            this.sequence = Seq.secondShotFollowingEnd;
            // frame function update
            this.FrameFunction = ControlShotNotFollowing;
            this.LateFrameFunction = Nothing;
            return;
        }

        projectileTr.position += this.shotDirection * this.ProjectileVelocity_second * Time.deltaTime;
    }

    void ControlShotNotFollowing()
    {
        Vector3 projectileMoving = this.shotDirection * this.ProjectileVelocity_second * Time.deltaTime;
        if(this.leftDistance <0.01f)
        {
            // all Q skill End
            // state change
            this.sequence = Seq.notActive;
            this.state = State.idle;    // have to modify as waiting
            // frame function update
            this.FrameFunction = Nothing;
            this.LateFrameFunction = Nothing;
            // disable object
            this.projectileObj.SetActive(false);
        }
        if(this.leftDistance < projectileMoving.magnitude)
        {
            projectileMoving = this.shotDirection * leftDistance;
        }

        projectileTr.position += projectileMoving;
        this.leftDistance -= projectileMoving.magnitude;
    }
    #endregion

    #region Cancel function
    /// <summary>
    /// Skill Cancel 명령이 나왔을때
    /// </summary>
    void CancelSkill()
    {
        switch (this.sequence)
        {
            case Seq.notActive:
                //할게없음
                break;
            case Seq.beforeFirst:
                CancelSkillRange();
                break;
            case Seq.beforeSecond:
                // 취소불가 낙장불입
                break;
            default:
                break;
        }
    }

    void CancelSkillRange()
    {
        // state change
        this.state = State.idle;
        this.sequence = Seq.notActive;
        // frame function update
        this.FrameFunction = Nothing;
        // function
        skillRangeObj.SetActive(false);
    }
    #endregion

    #region Utilfunction
    
    void GetDebug()
    {
        Debug.Log("Seq = " + this.sequence);
    }
    #endregion
}
