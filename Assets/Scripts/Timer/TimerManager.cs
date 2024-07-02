using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏计时器管理器
/// </summary>
public class TimerManager
{
    private GameTimer timer;

    public TimerManager() 
    { 
        timer = new GameTimer();
    }

    // 注册计时器
    public void Register(float time, System.Action callback)
    {
        timer.Register(time, callback);
    }

    // 检测计时器是否运行完毕并触发回调
    public void OnUpdate(float dt)
    {
        timer.OnUpdate(dt);
    }
}
