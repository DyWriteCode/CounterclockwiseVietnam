using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common;
using UnityEngine.SceneManagement;

/// <summary>
/// 加载游戏时的热更新面板
/// </summary>
public class UpgradeController : BaseController
{

    public UpgradeController() : base()
    {
        // 注册加载游戏视图
        GameApp.ViewManager.Reister(ViewType.UpgradeView, new ViewInfo()
        {
            PrefabName = "UpgradeView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });
        InitModuleEvent(); // 初始化模块事件(全局事件已在UIController初始化,所以只需初始化本模块事件)
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.UpgradeView, OpenUpgradeView); // 注册并打开加载场景面板
    }

    // 加载场景的回调函数
    private void OpenUpgradeView(System.Object[] args)
    {
        // 打开加载界面
        GameApp.ViewManager.Open(ViewType.UpgradeView, args);
    }

    public override void Init()
    {
        base.Init();
        ApplyControllerFunc(ControllerType.Upgrade, Defines.UpgradeView);
    }
}
