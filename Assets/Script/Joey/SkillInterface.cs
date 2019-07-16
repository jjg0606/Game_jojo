using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillInterface
{
    #region Legacy
    //bool isAvail {get;}

    //bool isActive{get;}

    //void ActiveSkill();

    //void DeActiveSkill();

    //void shotSkill();

    //GDele.vv endSkill { set; }
    #endregion

    /// <summary>
    /// Skill mgr 에서 각각 Skill의 State를 얻기 위함
    /// </summary>
    CEnum.Skill.State skillState { get; }

    /// <summary>
    /// Skill Class에게 마우스 입력 전달
    /// </summary>
    /// <param name="buttonnum"></param>
    void InputMouseBtn(int buttonnum);

    /// <summary>
    /// Skill Class에게 키보드 입력 전달
    /// </summary>
    /// <param name="keycode"></param>
    void InputKeyBoardBtn(KeyCode key);

    /// <summary>
    /// 현재 스킬클래스에서 다른 스킬클래스에 통신하기 위함
    /// </summary>
    GDele.vs BroadCast { get; set; }

    /// <summary>
    /// 다른 스킬 클래스에서 보내는 메세지를 수신하기 위함
    /// </summary>
    /// <param name="msg"></param>
    void GetBroadCast(string msg);


}
