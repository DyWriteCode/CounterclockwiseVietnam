using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Game.Common;
using Game.Common.Tools;

/// <summary>
/// 用户控制管理器 键盘操作 鼠标操作等
/// </summary>
public class UserInputManager
{
    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 鼠标点击到UI
            }
            else
            {
                // 触发对应事件
                Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, delegate (Collider2D col)
                {
                    if (col != null)
                    {
                        // 检测到有碰撞体的对象
                        GameApp.MessageManager.PostEvent(col.gameObject, Defines.OnSelectEvent);
                    }
                    else
                    {
                        // 没有检测到有碰撞体的对象 
                        GameApp.MessageManager.PostEvent(Defines.OnUnSelectEvent);
                    }
                });
            }
        }
    }
}
