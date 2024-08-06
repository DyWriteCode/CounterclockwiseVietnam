using System.Collections;
using System.Collections.Generic;
using Game.Config;
using UnityEngine;

public class LevelData
{
    public int Id;
    public string Name;
    public string SceneName;
    public string Des;
    public bool IsFinish; // 是否通关的标记

    public LevelData(Dictionary<string, string> data)
    {
        Id = int.Parse(data["Id"]);
        Name = data["Name"];
        SceneName = data["SceneName"];
        Des = data["Des"];
        IsFinish = false;
    }
}

/// <summary>
/// 关卡数据
/// </summary>
public class LevelModel : BaseModel
{
    private ConfigData levelConfig;
    private Dictionary<int, LevelData> levels; // 关卡字典
    public LevelData current; // 当前关卡

    public LevelModel()
    {
        levels = new Dictionary<int, LevelData>();
    }

    public override void Init()
    {
        levelConfig = GameApp.ConfigManager.GetConfigData("level");
        foreach(var item in levelConfig.GetLines())
        {
            LevelData l_data = new LevelData(item.Value);
            levels.Add(l_data.Id, l_data); 
        }
    }

    public LevelData GetLevel(int id)
    {
        return levels[id];
    }
}
