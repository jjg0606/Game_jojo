using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CEnum.Skill;

/// <summary>
/// 스킬과 관련된 입력과, 스킬간의 통신을 담당하는 클래스
/// </summary>
public class joeySkillMgr : MonoBehaviour
{

    private KeyCode[] keyBoardCodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.D, KeyCode.F };
    private int[] mouseCodes = { 0, 1 }; // 0 = left click, 1 = righ click
    private SkillInterface[] skillArray;
    private State[] statesArray;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: remove annotation after making new joey q skill script
        skillArray = new SkillInterface[1];
        skillArray[Index.Q] = GetComponent<JoeySkillQ>();
        foreach(SkillInterface skill in skillArray)
        {
            skill.BroadCast += this.BroadcastMessage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 키보드 입력을 받아 스킬클래스에게 전달
        foreach(KeyCode key in keyBoardCodes)
        {
            if(Input.GetKeyDown(key))
            {
                foreach(var SkillScript in skillArray)
                {
                    SkillScript.InputKeyBoardBtn(key);
                }
            }
        }

        // 마우스 입력을 받아 스킬클래스에게 전달
        foreach(int key in mouseCodes)
        {
            if(Input.GetMouseButtonDown(key))
            {
                foreach(var SkillScript in skillArray)
                {
                    SkillScript.InputMouseBtn(key);
                }
            }
        }
        
    }

    void BroadCastMsg(string msg)
    {
        foreach(SkillInterface skill in skillArray)
        {
            skill.GetBroadCast(msg);
        }
    }
}
