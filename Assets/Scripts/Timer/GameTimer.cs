using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏计时器
/// </summary>
public class GameTimer
{
    /// <summary>
    /// 存储时间数据的集合
    /// </summary>
    private List<GameTimerData> timers; 

    /// <summary>
    /// 初始化
    /// </summary>
    public GameTimer()
    {
        timers = new List<GameTimerData>();
    }

    /// <summary>
    /// 注册计时器
    /// </summary>
    /// <param name="timer">计时时长</param>
    /// <param name="callback">回调函数</param>
    public void Register(float timer, System.Action callback)
    {
        GameTimerData data = new GameTimerData(timer, callback);
        timers.Add(data);
    }

    /// <summary>
    /// 同update一起运行 检测每个计时器是否运行完毕
    /// </summary>
    /// <param name="dt">每个计时器是否运行完毕</param>
    public void OnUpdate(float dt)
    {
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            if (timers[i].OnUpdate(dt) == true)
            {
                timers.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 打断所有计时器
    /// </summary>
    public void Break()
    {
        timers.Clear();
    }

    /// <summary>
    /// 获取计时器数量
    /// </summary>
    /// <returns></returns>
    public int Count()
    {
        return timers.Count;
    }
}
