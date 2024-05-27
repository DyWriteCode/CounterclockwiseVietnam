using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkillAreaCommand : BaseCommand
{
    ISkill skill;
    bool IsInteract;
    private System.Action callback;


    public ShowSkillAreaCommand(ModelBase model, bool isInteract = false, System.Action callback = null) : base(model)
    {
        skill = model as ISkill;
        IsInteract = isInteract;
        this.callback = callback;
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
            if (IsInteract == true)
            {
                // Debug.Log("use interact method");
                GameApp.CommandManager.AddCommand(new InteractCommand(callback, model));
                return true;
            }
            else
            {
                // Debug.Log("use attack method");
                GameApp.CommandManager.AddCommand(new SkillCommand(model));
                return true;
            }
        }
        return false;
    }
}
