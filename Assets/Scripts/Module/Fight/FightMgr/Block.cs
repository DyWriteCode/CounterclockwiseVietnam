using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图单元格子类型枚举
/// </summary>
public enum BlockType
{
    Null, // 空
    Obstacle, // 障碍物
}

/// <summary>
/// 地图的单元格子
/// </summary>
public class Block : MonoBehaviour
{
    public int RowIndex; // 行的下标
    public int ColIndex; // 列的下标
    public BlockType Type; // 格子类型
    private SpriteRenderer selectSp; // 选中格子图片
    private SpriteRenderer gridSp; // 网格图片
    private SpriteRenderer dirSp; // 移动方向图片

    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();
        GameApp.MsgCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MsgCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    private void OnDestroy()
    {
        GameApp.MsgCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MsgCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    // 显示格子
    public void ShowGird(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }

    // 隐藏格子
    public void HideGird()
    {
        gridSp.enabled = false;
    }

    // 选中事件回调函数
    private void OnSelectCallBack(System.Object args)
    {
        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
        if (GameApp.CommandManager.IsRunningCommand == false)
        {
            GameApp.ViewManager.Open(ViewType.FightOptionDesView);
        }
    }

    // 未选中事件回调函数
    private void OnUnSelectCallBack(System.Object args)
    {
        dirSp.sprite = null;
        GameApp.ViewManager.Close((int)ViewType.FightOptionDesView);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    // 设置箭头方向的图片资源 和 颜色
    public void SetDirSp(Sprite sp, Color color)
    {
        dirSp.sprite = sp;
        dirSp.color = color;
    }

    //显示格子
    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }

    //隐藏格子
    public void HideGrid()
    {
        gridSp.enabled = false;
    }
}