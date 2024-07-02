using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏计时器
/// </summary>
public class GameTimer
{
    private List<GameTimerData> timers; // 存储时间数据的集合

    public GameTimer()
    {
        timers = new List<GameTimerData>();
    }

    // 注册计时器
    public void Register(float timer, System.Action callback)
    {
        GameTimerData data = new GameTimerData(timer, callback);
        timers.Add(data);
    }

    // 同update一起运行 检测每个计时器是否运行完毕
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

    // 打断所有计时器
    public void Break()
    {
        timers.Clear();
    }

    // 获取计时器数量
    public int Count()
    {
        return timers.Count;
    }
}
