using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id; // 物体ID
    public Dictionary<string, string> data; // 数据表
    public int Step; // 行动力
    public int Attack; // 攻击力
    public int Type; // 类型
    public int MaxHp; // 最大血量
    public int CurHp; // 当前血量
    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp; // 身体图片渲染组件
    public GameObject stop0bj; // 停止行动的标记物体
    public Animator ani; // 动画组件

    private bool _isStop; // 是否移动完成标记
    public bool IsStop
    {
        get 
        { 
            return _isStop;
        }
        set 
        {
            stop0bj.SetActive(value);
            if (value == true) 
            { 
                bodySp.color = Color.gray;
            }
            else
            {
                bodySp.color = Color.white;
            }
            _isStop = value;
        }
    }

    protected virtual void Start()
    {
        AddEvents();
    }

    protected virtual void OnDestroy()
    {
        RemoveEvents();
    }

    // 选中回调函数
    protected virtual void OnSelectCallback(System.Object args)
    {
        // 执行未选中
        GameApp.MsgCenter.PostEvent(Defines.OnUnSelectEvent);
        // test
        // bodySp.color = Color.red;
        GameApp.MapManager.ShowStepGird(this, Step);
    }

    // 没有选中回调函数
    protected virtual void OnUnSelectCallback(System.Object args)
    {
        // test
        // bodySp.color = Color.white;
        GameApp.MapManager.HideStepGird(this, Step);
    }

    // 注册事件
    protected virtual void AddEvents()
    {
        GameApp.MsgCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
        GameApp.MsgCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
    }

    // 移除事件
    protected virtual void RemoveEvents()
    {
        GameApp.MsgCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
        GameApp.MsgCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallback);
    }

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stop0bj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }
}
