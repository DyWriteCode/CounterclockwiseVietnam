using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkillAreaCommand : BaseCommand
{
    ISkill skill;

    public ShowSkillAreaCommand(ModelBase model) : base(model)
    {
        skill = model as ISkill;
    }

    public override void Do()
    {
        base.Do();
        skill.ShowSkillArea();
    }

    public override bool Update(float dt)
    {
        if (Input.GetMouseButtonDown(0))
        {
            skill.HideSkillArea();
            // Debug.Log("use attack");
            return true;
        }
        return false;
    }
}