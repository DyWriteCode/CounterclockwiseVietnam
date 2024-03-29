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

    public void Register(float time, System.Action callback)
    {
        timer.Register(time, callback);
    }

    public void OnUpdate(float dt)
    {
        timer.OnUpdate(dt);
    }
}
