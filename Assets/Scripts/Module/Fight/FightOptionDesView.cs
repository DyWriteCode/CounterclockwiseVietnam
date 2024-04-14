using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 战斗描述视图
/// </summary>
public class FightOptionDesView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        Find<Button>("bg/gameOverBtn").onClick.AddListener(onGameOverBtn);
        Find<Button>("bg/turnBtn").onClick.AddListener(onChangeEnemyTurn);
        Find<Button>("bg/cancelBtn").onClick.AddListener(onCancelBtn);
    }

    // 游戏结束按钮回调 结束本局游戏
    private void onGameOverBtn()
    {
        GameApp.ViewManager.Close((int)ViewType.FightOptionDesView);

    }

    // 回合结束切换到敌人回合
    private void onChangeEnemyTurn()
    {
        GameApp.ViewManager.Close((int)ViewType.FightOptionDesView);

    }

    // 取消
    private void onCancelBtn()
    {
        GameApp.ViewManager.Close((int)ViewType.FightOptionDesView);
    }
}
