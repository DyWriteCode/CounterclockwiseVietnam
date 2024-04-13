using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能管理器
/// </summary>
public class SkillManager
{
    private GameTimer timer; // 计时器
    //skill:使用的技能 targets:技能的作用日标 callback:回调
    private Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)> skills; // 技能队列

    public SkillManager() 
    { 
        timer = new GameTimer();
        skills = new Queue<(ISkill skill, List<ModelBase> targets, System.Action callback)> ();
    }

    // 添加技能
    public void AddSkill(ISkill skill, List<ModelBase> targets = null, System.Action callback = null)
    {
        skills.Enqueue((skill, targets, callback));
    }

    // 使用技能
    public void UseSkill(ISkill skill, List<ModelBase> targets = null, System.Action callback = null)
    {
        ModelBase current = (ModelBase)skill;
        // 看向一个目标
        if (targets.Count > 0)
        {
            // 默认看向第一个
            current.LootAtModel(targets[0]);
        }
        current.PlaySound(skill.SkillPro.Sound); // 播放音效
        current.PlayAni(skill.SkillPro.AniName); // 播放动画
        // 延迟攻击
        timer.Register(skill.SkillPro.AttackTime, delegate ()
        {
            // 技能的最多作用个数
            int atkCount = skill.SkillPro.AttackCount >= targets.Count ? targets.Count : skill.SkillPro.AttackCount;
            for (int i = 0; i < atkCount; i++)
            {
                targets[i].GetHit(skill); // 敌人受伤
            }
        });
        // 技能的持续时长
        timer.Register(skill.SkillPro.Time, delegate ()
        {
            // 回到待机状态
            current.PlayAni("idle");
            callback?.Invoke(); // 执行攻击回调函数
        });
    }

    // 更新
    public void Update(float dt)
    {
        timer.OnUpdate(dt);
        if (timer.Count() == 0 && skills.Count > 0)
        {
            // 下一个使用的技能
            var next = skills.Dequeue();
            if (next.targets != null)
            {
                UseSkill(next.skill, next.targets, next.callback); // 使用技能
            }
        }
    }

    // 是否正在使用技能
    public bool IsRunningSkill()
    {
        if (timer.Count() == 0 && skills.Count == 0)
        {
            return false;
        }
        else 
        { 
            return true;
        }
    }

    // 清空技能
    public void Clear()
    {
        timer.Break();
        skills.Clear();
    }
}