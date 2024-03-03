using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏计时器数据
/// </summary>
public class GameTimerData
{
    private float timer; // 计时时长
    private System.Action callback; // 回调函数

    public GameTimerData(float timer, System.Action callback)
    {
        this.timer = timer;
        this.callback = callback;
    }

    public bool OnUpdate(float dt)
    {
        timer -= dt;
        if (timer <= 0)
        {
            callback.Invoke();
            return true;
        }
        return false;
    }
}
