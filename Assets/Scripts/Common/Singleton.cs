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
    private static readonly object locker = new object();

    /// <summary>
    /// 单例
    /// </summary>
    public static T Instance
    { 
        get 
        { 
            lock (locker) 
            {
                return instance; 
            }
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

/// <summary>
/// mono单例基础类
/// </summary>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour, new()
{
    private static readonly T instance = Activator.CreateInstance<T>();
    private static readonly object locker = new object();

    /// <summary>
    /// 单例
    /// </summary>
    public static T Instance
    {
        get
        {
            lock (locker)
            {
                return instance;
            }

        }
        set
        {
            return;
        }
    }

    // 初始化
    public virtual void Awake()
    {

    }

    // 初始化
    public virtual void Start()
    {

    }

    // 每帧运行
    public virtual void Update()
    {

    }

    // 释放
    public virtual void OnDestroy()
    {

    }
}
