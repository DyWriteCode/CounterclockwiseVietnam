using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常量类
/// </summary>
public static class Defines
{
    // 控制器相关事件的字符串
    public static readonly string OpenStartView = "OpenStartView"; // 打开开始面板
    public static readonly string OpenSetView = "OpenSetView"; // 打开设置面板
    public static readonly string OpenMessageView = "OpenMessageView"; // 打开消息面板
    public static readonly string OpenSelectLevelView = "OpenSelectLevelView"; // 打开关卡选择面板
    public static readonly string LoadingScene = "LoadingScene"; // 加载场景面板                                                     
    public static readonly string UpgradeView = "UpgradeView"; // 更新场景面板
    public static readonly string BeginFight = "onBeginFight"; // 开始战斗

    // 全局事件相关字符串
    public static readonly string ShowLevelDesEvent = "ShowLevelDesEvent"; // 展示关卡详情事件
    public static readonly string HideLevelDesEvent = "HideLevelDesEvent"; // 隐藏关卡详情事件
    public static readonly string OnSelectEvent = "OnSelectEvent"; // 选中事件
    public static readonly string OnUnSelectEvent = "OnUnSelectEvent"; // 未选中事件
}
