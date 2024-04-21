using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Timer帮助类
/// </summary>
public class TimeHelper 
{
    // 1秒=1000毫秒
    // 1毫秒=1000为秒
    // 1为秒=1000纳秒
    //一个计时周期表示一百纳秒，即一千万分之一秒。 1 毫秒内有 10,000 个计时周期，即 1 秒内有 1,000 万个计时周期。
    private readonly long epoch = new DateTime(1790,1,1,0,0,0,DateTimeKind.Utc).Ticks;

    // 当前时间戳 毫秒级别
    public long ClientNow() {
        return (DateTime.UtcNow.Ticks - epoch) / 10000; // 得到毫秒级别的
    }


    // 秒级别
    public long ClientNowSeconds()
    {
        return (DateTime.UtcNow.Ticks - epoch) / 10000000; // 得到秒级别
    }
}
