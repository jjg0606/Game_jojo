using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillInterface
{
   bool isAvail{get;}

    bool isActive{get;}

    void ActiveSkill();

    void DeActiveSkill();

    void shotSkill();

}
