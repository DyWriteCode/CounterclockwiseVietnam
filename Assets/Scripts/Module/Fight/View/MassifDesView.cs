using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Tools;
using UnityEngine.UI;

/// <summary>
/// 地块信息面板
/// </summary>
public class MassifDesView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        Massif massif = args[0] as Massif;
        Find<Image>("bg/icon").SetIcon(massif.data["Icon"]);
        Find<Image>("bg/hp/fill").fillAmount = (float)massif.CurHp / (float)massif.MaxHp;
        Find<Text>("bg/hp/txt").text = $"{massif.CurHp} / {massif.MaxHp}";
        Find<Text>("bg/atkTxt/txt").text = massif.Attack.ToString();
        Find<Text>("bg/StepTxt/txt").text = massif.Step.ToString();
    }
}
