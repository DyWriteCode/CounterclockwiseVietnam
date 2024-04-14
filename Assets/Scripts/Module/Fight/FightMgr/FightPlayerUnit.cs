using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家的回合
/// </summary>
public class FightPlayerUnit : FightUnitBase
{
    public override void Init()
    {
        base.Init();
        // test
        // Debug.Log("player");
        GameApp.FightManager.ResetEnemys(); // 重置敌人行动
        GameApp.ViewManager.Open(ViewType.TipView, "玩家回合");
    }
}
