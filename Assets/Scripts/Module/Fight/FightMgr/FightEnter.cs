using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 进入战斗后需要处理的逻辑
/// </summary>
public class FightEnter : FightUnitBase
{
    public override void Init()
    {
        // 地图初始化
        GameApp.MapManager.Init();
        // 进入战斗
        GameApp.FightManager.EnterFight();
    }
}
