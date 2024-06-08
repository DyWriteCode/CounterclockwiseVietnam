using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/// <summary>
/// 开发工具类
/// 与Tools保持同步的一个动态工具类
/// 没有任何静态方法
/// </summary>
public class ToolsActive
{
    // 临时使用的变量
    public int test = 0;
    public int test2 = 0;
    public int test3 = 0;

    // 设置图标
    public void SetIcon(UnityEngine.UI.Image img, string res)
    {
        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }

    //检测鼠标位置是杏有2d碰撞物休
    public void ScreenPointToRay2D(Camera cam, Vector2 mousePos, System.Action<Collider2D> callback)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
        callback?.Invoke(col);
    }

    //检测鼠标位置是杏有2d碰撞物休
    public Collider2D ScreenPointToRay2D(Camera cam, Vector2 mousePos)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
        return col;
    }

    // 占位函数 
    public void PassProgram()
    {
        Debug.Log("use a pass program, please remember to clear it");
        return;
    }

    // 退出游戏
    public void ExitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    // 截取字符串
    public string CutString(string s, string s1, string s2)
    {
        int n1, n2;
        n1 = s.IndexOf(s1, 0) + s1.Length;
        n2 = s.IndexOf(s2, n1);
        return s.Substring(n1, n2 - n1);
    }

    // 转换变量类型
    
}
