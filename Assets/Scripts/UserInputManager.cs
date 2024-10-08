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
    /// <summary>
    /// 每一帧调用
    /// </summary>
    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame == true)
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                // 鼠标点击到UI
                // TODO : 到时候可以做回放系统的时候可以在这里写一下逻辑
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
