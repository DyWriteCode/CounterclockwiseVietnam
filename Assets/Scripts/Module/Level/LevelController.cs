using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡控制器
/// </summary>
public class LevelController : BaseController
{
    public LevelController() : base()
    {
        SetModel(new LevelModel()); // 设置数据模型
        // 注册视图面板
        GameApp.ViewManager.Reister(ViewType.SelectLevelView, new ViewInfo()
        {
            PrefabName = "SelectLevelView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });
        InitModuleEvent(); // 初始化本模块事件
        InitGlobalEvent(); // 初始化全局事件
    }

    public override void Init()
    {
        model.Init(); // 初始化数据
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenSelectLevelView, onOpenSelectLevelView);
    }

    // 注册全局事件
    public override void InitGlobalEvent()
    {
        GameApp.MessageManager.AddEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallback);
        GameApp.MessageManager.AddEvent(Defines.HideLevelDesEvent, onHideLevelDesCallback);
    }

    // 移除全局事件
    public override void RemoveGlobalEvent()
    {
        GameApp.MessageManager.RemoveEvent(Defines.ShowLevelDesEvent, onShowLevelDesCallback);
        GameApp.MessageManager.RemoveEvent(Defines.HideLevelDesEvent, onHideLevelDesCallback);
    }

    private void onShowLevelDesCallback(System.Object args)
    {
        // test
        // Debug.Log("level id : " + args.ToString()); 
        LevelModel levelModel = GetModel<LevelModel>();
        levelModel.current = levelModel.GetLevel(int.Parse(args.ToString()));
        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).ShowLevelDes();
    }

    private void onHideLevelDesCallback(System.Object args)
    {
        GameApp.ViewManager.GetView<SelectLevelView>((int)ViewType.SelectLevelView).HideLevelDes();
    }

    private void onOpenSelectLevelView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SelectLevelView, args);
    }
}
