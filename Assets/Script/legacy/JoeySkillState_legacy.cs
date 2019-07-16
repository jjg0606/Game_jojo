using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeySkillState : MonoBehaviour
{
    //private enum SkillState
    //{
    //    idle=-1,
    //    qisWorking
    //}

    //private SkillState skillState = SkillState.idle;
    //private SkillInterface[] skillArray;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    skillArray = new SkillInterface[1];
    //    skillArray[0] = GetComponent<JoeyQSkill>();
    //    skillArray[0].endSkill = this.setIdle;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(0)&& (int)skillState >= 0 && (int)skillState < skillArray.Length)
    //    {
    //        skillArray[(int)skillState].shotSkill();
    //    }

    //    if(Input.GetKeyDown(KeyCode.Q))
    //    {
    //        if(skillState == SkillState.idle)
    //        {
    //            if(skillArray[0].isAvail)
    //            {
    //                skillState = SkillState.qisWorking;
    //                skillArray[0].ActiveSkill();
    //            }
    //        }
    //        else if(skillState == SkillState.qisWorking)
    //        {
    //            skillState = SkillState.idle;
    //            skillArray[0].DeActiveSkill();
    //        }
    //    }
    //}

    //public void setIdle()
    //{
    //    this.skillState = SkillState.idle;
    //}
}
