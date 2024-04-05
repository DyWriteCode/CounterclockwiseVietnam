using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 英雄脚本
/// </summary>
public class Hero : ModelBase, ISkill
{
    public SkillProperty SkillPro { get; set; }

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
        SkillPro = new SkillProperty(int.Parse(this.data["Skill"]));
    }

    // 被选中回调函数
    protected override void OnSelectCallback(object args)
    {
        // 玩家回合才能选中角色
        if (GameApp.FightManager.state == GameState.Player)
        {
            if (GameApp.CommandManager.IsRunningCommand == true)
            {
                return;
            }
            // 执行未选中
            GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);       
            // 不可以操作
            if (IsStop == false)
            {
                // 显示路径
                GameApp.MapManager.ShowStepGird(this, Step);
                // 添加显示路径指令
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));
                // 添加选项事件
                addOptionEvent();
            }
            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }
    }

    private void addOptionEvent()
    {
        GameApp.MsgCenter.AddTempEvent(Defines.OnAttackEvent, OnAttackCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnIdleEvent, OnIdleCallBack);
        GameApp.MsgCenter.AddTempEvent(Defines.OnCancelEvent, OnCancelCallBack);
    }

    // 取消 移动
    private void OnCancelCallBack(System.Object args)
    {
        // Debug.Log("Cancel");
        GameApp.CommandManager.UnDo();
    }

    // 待机状态
    private void OnIdleCallBack(System.Object args)
    {
        // Debug.Log("Idle");
        IsStop = true;
    }

    // 攻击状态
    private void OnAttackCallBack(System.Object args)
    {
        // Debug.Log("Attack");
    }

    // 没有被选中回调函数
    protected override void OnUnSelectCallback(object args)
    {
        base.OnUnSelectCallback(args);
        GameApp.ViewManager.Close((int)ViewType.HeroDesView);
    }

    // 显示技能区域
    public void ShowSkillArea()
    {

    }

    // 隐藏技能区域
    public void HideSkillArea()
    {
        
    }
}
