using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 单例基础类
/// </summary>
public class Singleton<T> where T : class, new()
{
    private static readonly T instance = Activator.CreateInstance<T>();
    public static T Instance
    { 
        get 
        { 
            return instance; 
        } 
        set
        {
            return;
        }
    }

    // 初始化
    public virtual void Init()
    {

    }

    // 每帧运行
    public virtual void Update(float dt)
    {

    }

    // 释放
    public virtual void OnDestroy()
    {

    }
}
