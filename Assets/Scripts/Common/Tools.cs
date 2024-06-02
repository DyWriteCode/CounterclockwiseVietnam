using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 开发工具类
/// </summary>
public static class Tools
{
    // 临时使用的变量
    public static int test = 0;
    public static int test2 = 0;
    public static int test3 = 0;

    // 设置图标
    public static void SetIcon(this UnityEngine.UI.Image img, string res)
    {
        img.sprite = Resources.Load<Sprite>($"Icon/{res}");
    }

    //检测鼠标位置是杏有2d碰撞物休
    public static void ScreenPointToRay2D(Camera cam, Vector2 mousePos, System.Action<Collider2D> callback)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
        callback?.Invoke(col);
    }

    //检测鼠标位置是杏有2d碰撞物休
    public static Collider2D ScreenPointToRay2D(Camera cam, Vector2 mousePos)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
        return col;
    }

    // 占位函数 
    public static void PassProgram()
    {
        Debug.Log("use a pass program, please remember to clear it");
        return;
    }

    // 退出游戏
    public static void ExitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    // 截取字符串
    public static string CutString(string s, string s1, string s2)
    {
        int n1, n2;
        n1 = s.IndexOf(s1, 0) + s1.Length;
        n2 = s.IndexOf(s2, n1);
        return s.Substring(n1, n2 - n1);
    }
}
