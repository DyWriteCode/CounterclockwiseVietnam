using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗控制器(战斗相关的界面以及事件等)
/// </summary>
public class FightController : BaseController
{
    public FightController() : base()
    {
        SetModel(new FightModel(this)); // 设置战斗数据模型
        GameApp.ViewManager.Reister(ViewType.FightView, new ViewInfo()
        {
            PrefabName = "FightView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
        });
        GameApp.ViewManager.Reister(ViewType.FightSelectHeroView, new ViewInfo()
        {
            PrefabName = "FightSelectHeroView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 1,
        });
        GameApp.ViewManager.Reister(ViewType.DragHeroView, new ViewInfo()
        {
            PrefabName = "DragHeroView",
            controller = this,
            parentTf = GameApp.ViewManager.worldCanvasTf, // 设置到世界画布
            Sorting_Order = 2,
        });
        GameApp.ViewManager.Reister(ViewType.TipView, new ViewInfo()
        {
            PrefabName = "TipView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });
        GameApp.ViewManager.Reister(ViewType.HeroDesView, new ViewInfo()
        {
            PrefabName = "HeroDesView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });
        GameApp.ViewManager.Reister(ViewType.EnemyDesView, new ViewInfo()
        {
            PrefabName = "EnemyDesView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });
        GameApp.ViewManager.Reister(ViewType.MassifDesView, new ViewInfo()
        {
            PrefabName = "MassifDesView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });
        GameApp.ViewManager.Reister(ViewType.SupplieDesView, new ViewInfo()
        {
            PrefabName = "SupplieDesView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 2,
        });
        GameApp.ViewManager.Reister(ViewType.SelectOptionView, new ViewInfo()
        {
            PrefabName = "SelectOptionView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
        });
        GameApp.ViewManager.Reister(ViewType.FightOptionDesView, new ViewInfo()
        {
            PrefabName = "FightOptionDesView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 3,
        });
        GameApp.ViewManager.Reister(ViewType.WinView, new ViewInfo()
        {
            PrefabName = "WinView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 3,
        });
        GameApp.ViewManager.Reister(ViewType.LossView, new ViewInfo()
        {
            PrefabName = "LossView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
            Sorting_Order = 3,
        });
        InitModuleEvent();
    }

    public override void Init()
    {
        model.Init();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.BeginFight, onBeginFightCallback);
    }

    private void onBeginFightCallback(System.Object[] args)
    {
        // 进入战斗
        GameApp.FightManager.ChangeState(GameState.Enter);
        GameApp.ViewManager.Open(ViewType.FightView);
        GameApp.ViewManager.Open(ViewType.FightSelectHeroView);
    }
}
