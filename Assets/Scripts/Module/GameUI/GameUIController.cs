using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 处理一些游戏通用UI的控制器(比如开始面板,通知面板等)
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        // 注册视图
        // 开始游戏视图
        GameApp.ViewManager.Reister(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });
        // 游戏设置视图
        GameApp.ViewManager.Reister(ViewType.SetView, new ViewInfo()
        {
            PrefabName = "SetView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 1 // 挡住开始面板层级更高一级
        });
        // 游戏通知视图
        GameApp.ViewManager.Reister(ViewType.MessageView, new ViewInfo()
        {
            PrefabName = "MessageView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 10 // 挡住任何面板层级更高一级
        });
        InitModuleEvent(); // 初始化模块事件
        InitGlobalEvent(); // 初始化全局事件
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView); // 注册并打开开始面板
        RegisterFunc(Defines.OpenSetView, openSetView); // 注册并打开设置面板
        RegisterFunc(Defines.OpenMessageView, openMessageView); // 注册并打开设置面板
    }

    // 测试模板注册事件 例子
    private void openStartView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.StartView, args);
    }

    // 打开设置面板
    private void openSetView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SetView, args);
    }

    // 打开消息面板
    private void openMessageView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.MessageView, args);
    }

    public override void Init()
    {
        // 调用GameController开发面板事件
        // ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
    }
}
