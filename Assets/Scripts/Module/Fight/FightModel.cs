using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 选项
/// </summary>
public class OptionData
{
    public int Id;
    public string EventValue;
    public string Name;
}

/// <summary>
/// 战斗时使用的数据
/// </summary>
public class FightModel : BaseModel
{
    private List<OptionData> options;
    public ConfigData optionConfig;

    // 初始化
    public FightModel(BaseController ctl) : base(ctl) 
    {
        options = new List<OptionData>();
    }

    public override void Init()
    {
        optionConfig = GameApp.ConfigManager.GetConfigData("option");
        foreach (var item  in optionConfig.GetLines())
        {
            OptionData opData = new OptionData();
            opData.Id = int.Parse(item.Value["Id"]);
            opData.EventValue = item.Value["EventName"];
            opData.Name = item.Value["Name"];
            options.Add(opData);
        }
    }
}
