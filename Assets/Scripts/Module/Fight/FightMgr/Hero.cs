using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 英雄脚本
/// </summary>
public class Hero : ModelBase
{
    public void Init(Dictionary<string, string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(this.data["Id"]);
        Type =  int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
    }

    // 被选中回调函数
    protected override void OnSelectCallback(object args)
    {
        base.OnSelectCallback(args);
        GameApp.ViewManager.Open(ViewType.HeroDesView, this);
    }

    // 没有被选中回调函数
    protected override void OnUnSelectCallback(object args)
    {
        base.OnUnSelectCallback(args);
        GameApp.ViewManager.Close((int)ViewType.HeroDesView);
    }
}
