using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视图接口
/// </summary>
public interface IBaseView
{
    bool IsInit(); // 视图是否已经初始化

    bool IsShow(); // 视图是否显示

    void InitUI(); // 初始化视图面板

    void InitData(); // 初始化数据

    void Open(params object[] args); // 打开面板

    void Close(params object[] args); // 关闭面板

    void DestroyView(); // 删除面板

    void ApplyFunc(string eventName, params object[] args); // 触发本模块事件

    void ApplyControllerFunc(int controllerKey, string eventName, params object[] args); // 触发其他控制器模块事件

    void SetVisible(bool value); // 设置显示隐藏

    int ViewId { get; set; } // 面板Id

    BaseController Controller { get; set; } // 面板所属控制器
}
