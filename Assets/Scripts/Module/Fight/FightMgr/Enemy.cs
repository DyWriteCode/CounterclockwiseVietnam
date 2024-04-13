using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ModelBase
{
    protected override void Start()
    {
        base.Start();
        data = GameApp.ConfigManager.GetConfigData("enemy").GetDataById(Id);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    // 被选中回调函数
    protected override void OnSelectCallback(object args)
    {
        if (GameApp.CommandManager.IsRunningCommand == true)
        {
            return;
        }
        base.OnSelectCallback(args);
        GameApp.ViewManager.Open(ViewType.EnemyDesView, this);
    }

    // 没有被选中回调函数
    protected override void OnUnSelectCallback(object args)
    {
        base.OnUnSelectCallback(args);
        GameApp.ViewManager.Close((int)ViewType.EnemyDesView);
    }
}
