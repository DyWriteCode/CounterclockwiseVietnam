using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能接口
/// </summary>
public interface ISkill
{
    SkillProperty SkillPro { get; set; }

    void ShowSkillArea();

    void HideSkillArea();
}
