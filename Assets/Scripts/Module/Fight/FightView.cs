using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 战斗基本面板
/// </summary>
public class FightView : BaseView
{
    // 缓存回和数组件
    private Text roundTxt;
    // 缓存金币组件
    private Text moneyTxt;

    protected override void OnStart()
    {
        base.OnStart();
        roundTxt = Find<Text>("TurnTxt");
        moneyTxt = Find<Text>("icon.txt");
    }

    private void Update()
    {
        // 更新回合数
        roundTxt.text = $"第 {GameApp.FightManager.RoundCount} 回合";
    }
}
