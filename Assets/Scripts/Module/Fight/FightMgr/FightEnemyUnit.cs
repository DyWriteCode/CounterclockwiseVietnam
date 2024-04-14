using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人回合
/// </summary>
public class FightEnemyUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        // test
        // Debug.Log("enemy");
        GameApp.FightManager.ResetHeros(); // 重置英雄行动
        GameApp.ViewManager.Open(ViewType.TipView, "敌人回合");
        GameApp.CommandManager.AddCommand(new WaitCommand(1.25f));
    }
}
