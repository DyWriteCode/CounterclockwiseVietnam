using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 用户控制管理器 键盘操作 鼠标操作等
/// </summary>
public class UserInputManager
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 点击到UI
            }
            else
            {
                Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition, delegate (Collider2D col)
                {
                    if (col != null)
                    {
                        // 检测到有碰撞体的对象
                        GameApp.MessageManager.PostEvent(col.gameObject, Defines.OnSelectEvent);
                    }
                    else
                    {
                        GameApp.MessageManager.PostEvent(Defines.OnUnSelectEvent);
                    }
                });
            }
        }
    }
}
