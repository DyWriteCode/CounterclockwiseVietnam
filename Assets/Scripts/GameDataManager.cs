using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理器(存储玩家基本的游戏信息)
/// </summary>
public class GameDataManager
{
    /// <summary>
    /// 英雄列表
    /// </summary>
    public List<int> heros;
    /// <summary>
    /// 金币数量
    /// </summary>
    public int Money; 

    /// <summary>
    /// 初始化
    /// </summary>
    public GameDataManager() 
    {
        // 默认三个英雄的ID 提前保存
        heros = new List<int>
        {
            10001,
            10002,
            10003,
            10001,
            10002,
            10003,
        };
        // 这个一样效果
        // heros.Add(10001);
        // heros.Add(10002);
        // heros.Add(10003);
        // 默认给一点 至少做个样子 虽然说没实现相关的商店功能
        Money = 10000;
    }
}
