using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 交互指令
/// </summary>
public class InteractCommand : BaseCommand
{
    private System.Action callback;
    bool isInteract;

    public InteractCommand(System.Action callback, ModelBase model) : base(model)
    {
        this.callback = callback;
        // 判断是否可交互
        List<ModelBase> results = (model as ISkill).GetTarget();
        if (results.Count > 0)
        {
            if (int.Parse(results[0].data["Interact"]) == 0)
            {
                isInteract = false;
            }
            else
            {
                isInteract = true;
            }
        }
    }

    public override void Do()
    {
        base.Do();
        if (isInteract == false)
        {
            GameApp.ViewManager.Open(ViewType.TipView, "此人物或物品不可交互");
        }
        else
        {
            callback?.Invoke();
        }
    }

    public override void UnDo()
    {
        base.UnDo();
    }

    public override bool Update(float dt)
    {
        if (GameApp.SkillManager.IsRunningSkill() == false)
        {
            model.IsStop = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
