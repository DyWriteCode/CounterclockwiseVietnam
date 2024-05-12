using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 网络管理器
/// </summary>
public class NetManager
{
    // 创建一个工厂实例
    public BufferFactory bufferFactory;

    public NetManager()
    {
        bufferFactory = new BufferFactory();
    }

    public void Update(float dt)
    {
        
    }
}
