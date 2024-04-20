using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗结束类别
/// </summary>
public class FightGameOverUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        GameApp.CommandManager.Clear(); // 清除掉所有指令
        // 胜利界面 失败界面在一起
        if (GameApp.FightManager.heros.Count == 0)
        {
            // 延迟一点时间才出现界面
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, delegate ()
            {
                GameApp.ViewManager.Open(ViewType.LossView);
            }));
        }
        else if (GameApp.FightManager.enemys.Count == 0)
        {
            // 延迟一点时间才出现界面
            GameApp.CommandManager.AddCommand(new WaitCommand(1.25f, delegate ()
            {
                GameApp.ViewManager.Open(ViewType.WinView);
            }));
        }
        else
        {
            return;
        }
    }

    public override bool Update(float dt)
    {
        return true;
    }
}

