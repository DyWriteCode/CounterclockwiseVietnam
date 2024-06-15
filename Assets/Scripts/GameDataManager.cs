using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据管理器(存储玩家基本的游戏信息)
/// </summary>
public class GameDataManager
{
    public List<int> heros; // 英雄列表
    public int Money; // 金币数量

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
    }
}
