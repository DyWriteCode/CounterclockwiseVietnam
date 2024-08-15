using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏计时器管理器
/// </summary>
public class TimerManager
{
    /// <summary>
    /// 计时器对象
    /// </summary>
    private GameTimer timer;

    public TimerManager() 
    { 
        timer = new GameTimer();
    }

    /// <summary>
    /// 注册计时器
    /// </summary>
    /// <param name="time"></param>
    /// <param name="callback"></param>
    public void Register(float time, System.Action callback)
    {
        timer.Register(time, callback);
    }

    /// <summary>
    /// 检测计时器是否运行完毕并触发回调
    /// </summary>
    /// <param name="dt">每针间隔时间</param>
    public void OnUpdate(float dt)
    {
        timer.OnUpdate(dt);
    }
}
