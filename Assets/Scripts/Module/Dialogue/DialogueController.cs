using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common;

/// <summary>
/// 用来处理游戏剧情的对话的类
/// </summary>
public class DialogueController : BaseController
{
    public DialogueController() : base()
    {
        // 游戏剧情对话视图
        GameApp.ViewManager.Reister(ViewType.DialogueView, new ViewInfo()
        {
            PrefabName = "DialogueView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 3,
        });
        // 浮窗对话视图
        GameApp.ViewManager.Reister(ViewType.SpeakView, new ViewInfo()
        {
            PrefabName = "SpeakView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 3,
        });
        InitModuleEvent(); // 初始化模块事件
        InitGlobalEvent(); // 初始化全局事件
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenDialogueView, openDialogueView); // 注册并打开剧情对话面板
        RegisterFunc(Defines.OpenSpeakView, openSpeakView); // 注册并打开浮窗对话面板
    }

    private void openDialogueView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.DialogueView, args);
    }

    private void openSpeakView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SpeakView, args);
    }
}
