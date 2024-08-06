using System.Collections;
using System.Collections.Generic;
using Game.Common;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 加载更新界面
/// </summary>
public class UpgradeView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("startGameBtn").onClick.AddListener(onStartGameBtn);
    }

    private void onStartGameBtn()
    {
        // 游戏开始页面已预加载, 更新途中大概率不会对游戏起始页面更改
        // 关闭界面
        GameApp.ViewManager.Close(ViewId);
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
    }
}
