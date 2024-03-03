using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏主控制器(处理，开始游戏保存，退出等操作)
/// </summary>
public class GameController : BaseController
{
    public GameController() : base()
    {
        // 目前没有视图
        InitModuleEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        // 调用GameController开发面板事件
        // ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
    }
}
