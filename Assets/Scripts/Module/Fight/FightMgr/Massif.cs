using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 地形脚本
/// </summary>
public class Massif : ModelBase, ISkill
{
    public SkillProperty SkillPro { get; set; }
    private Slider hpSlider;

    protected override void Start()
    {
        base.Start();
        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
        data = GameApp.ConfigManager.GetConfigData("massif").GetDataById(Id);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        // Step = 0;
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        SkillPro = new SkillProperty(int.Parse(data["Skill"]));
    }

    public void Init(Dictionary<string, string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
        Id = int.Parse(this.data["Id"]);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        // Step = 0;
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;
        SkillPro = new SkillProperty(int.Parse(this.data["Skill"]));
    }

    public void HideSkillArea()
    {
        
    }

    public void ShowSkillArea()
    {
        
    }

    protected override void OnSelectCallback(object args)
    {
        if (GameApp.CommandManager.IsRunningCommand == true)
        {
            return;
        }
        base.OnSelectCallback(args);
        GameApp.ViewManager.Open(ViewType.MassifDesView, this);
    }

    protected override void OnUnSelectCallback(object args)
    {
        base.OnUnSelectCallback(args);
        GameApp.ViewManager.Close((int)ViewType.MassifDesView);
    }

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
            // 从地形集合中移除
            GameApp.FightManager.RemoveMessif(this);
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
