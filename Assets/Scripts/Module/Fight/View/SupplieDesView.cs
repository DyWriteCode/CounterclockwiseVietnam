using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Tools;
using UnityEngine.UI;

/// <summary>
/// 补给物信息面板
/// </summary>
public class SupplieDesView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Supplie supplie = args[0] as Supplie;
        Find<Image>("bg/icon").SetIcon(supplie.data["Icon"]);
        Find<Image>("bg/hp/fill").fillAmount = (float)supplie.CurHp / (float)supplie.MaxHp;
        Find<Text>("bg/hp/txt").text = $"{supplie.CurHp} / {supplie.MaxHp}";
        Find<Text>("bg/atkTxt/txt").text = supplie.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = supplie.Step.ToString();
    }
}
