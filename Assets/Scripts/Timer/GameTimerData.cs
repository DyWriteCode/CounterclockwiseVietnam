using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏计时器数据
/// </summary>
public class GameTimerData
{
    /// <summary>
    /// 计时时长
    /// </summary>
    private float timer;
    /// <summary>
    /// 回调函数
    /// </summary>
    private System.Action callback;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="timer">计时时长</param>
    /// <param name="callback">回调函数</param>
    public GameTimerData(float timer, System.Action callback)
    {
        this.timer = timer;
        this.callback = callback;
    }

    /// <summary>
    /// 检测计时器是否运行完毕并触发回调
    /// </summary>
    /// <param name="dt">每针间隔时间</param>
    /// <returns>是否更新完成</returns>
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
