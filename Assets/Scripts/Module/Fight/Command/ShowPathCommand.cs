using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 显示移动路径的指令
/// </summary>
public class ShowPathCommand : BaseCommand
{
    public ShowPathCommand(ModelBase model) : base(model)
    {
        
    }

    public override bool Update(float dt)
    {
        // 点击鼠标后 确定启动的位置
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        return false;
    }
}
