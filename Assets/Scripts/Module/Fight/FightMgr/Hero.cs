using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 英雄脚本
/// </summary>
public class Hero : ModelBase, ISkill
{
    public SkillProperty SkillPro { get; set; }
    private Slider hpSlider;

    protected override void Start()
    {
        base.Start();
        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
    }

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
            GameApp.MessageManager.PostEvent(Defines.OnUnSelectEvent);       
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
        GameApp.MessageManager.AddTempEvent(Defines.OnAttackEvent, OnAttackCallBack);
        GameApp.MessageManager.AddTempEvent(Defines.OnIdleEvent, OnIdleCallBack);
        // 与OnInteractEvent这个事件合并了
        // GameApp.MessageManager.AddTempEvent(Defines.OnPickUpItemEvent, OnPickUpItemCallback);
        GameApp.MessageManager.AddTempEvent(Defines.OnInteractEvent, OnInteractCallback);
        GameApp.MessageManager.AddTempEvent(Defines.OnCancelEvent, OnCancelCallBack);
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
        GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this));
    }

    // 与物品交互状态
    private void OnInteractCallback(System.Object args)
    {
        GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this, true, delegate
        {
            // test
            //GameApp.ViewManager.Open(ViewType.SpeakView, new SpeakInfo()
            //{
            //    MsgIxt = "test",
            //    Trans = gameObject.transform.position,
            //    Callback = () => { }
            //});
            GameApp.ViewManager.Open(ViewType.TipView, "interact with player");
        }));
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
        GameApp.MapManager.ShowAttackStep(this, SkillPro.AttackRange, Color.red);
    }

    // 隐藏技能区域
    public void HideSkillArea()
    {
        GameApp.MapManager.HideAttackStep(this, SkillPro.AttackRange);
    }

    // 受伤
    public override void GetHit(ISkill skill)
    {
        // 播放受伤音效
        GameApp.SoundManager.PlayEffect("hit", transform.position);
        // 扣除血量
        CurHp -= skill.SkillPro.Attack;
        // 显示伤害数字
        GameApp.ViewManager.ShowHitNum($"-{skill.SkillPro.Attack}", Color.red, transform.position);
        // 击中特效
        PlayEffect(skill.SkillPro.AttackEffect);
        // 判断是否死亡
        if (CurHp <= 0)
        {
            CurHp = 0;
            PlayAni("die");
            Destroy(gameObject, 1.2f);
            // 从英雄集合中移除
            GameApp.FightManager.RemoveHero(this);
        }
        StopAllCoroutines();
        StartCoroutine(ChangeColor());
        StartCoroutine(UpdateSlider());
    }

    // 这个是个协程
    // 在C#中 协程是一个返回类型为IEnumerator的方法 这个方法通过使用yield return语句来指定暂停的位置
    // 协程在yield return处暂停 并在Unity 引擎的下一帧或等待的条件满足后继续执行
    // 协程在完成所有的yield return语句后会自动结束 也可以使用 yield break;来提前结束协程
    // 你可以使用 StartCoroutine 方法来启动协程 在MonoBehaviour的派生类中 比如在脚本中,可以通过StartCoroutine启动协程 
    private IEnumerator ChangeColor()
    {
        bodySp.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.25f);
        bodySp.material.SetFloat("_FlashAmount", 0);
    }

    private IEnumerator UpdateSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.DOValue((float)CurHp / (float)MaxHp, 0.25f);
        yield return new WaitForSeconds(0.75f);
        hpSlider.gameObject.SetActive(false);
    }
}
