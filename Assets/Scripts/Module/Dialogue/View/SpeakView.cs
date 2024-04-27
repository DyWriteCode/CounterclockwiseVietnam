using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 对话消息,信息类
/// </summary>
public class SpeakInfo
{
    public string MsgIxt;
    public Vector2 Trans;
    public System.Action Callback;
}

/// <summary>
/// 战斗时微对话视图
/// 主要是为了避免原对话时间过于长所以呢就新建了一个浮窗式的对话
/// </summary>
public class SpeakView : BaseView
{ 
    public override void Open(params object[] args)
    {
        SpeakInfo Info = args[0] as SpeakInfo;
        Find("bg/item").transform.position = Info.Trans + new Vector2(1.5f, 1f);
        StartCoroutine(GameApp.DialogueManager.TypeWriterEffect(Info.MsgIxt.Length * 3f, Info.MsgIxt, Find<Text>("bg/item/txt")));
        GameApp.TimerManager.Register(Info.MsgIxt.Length * 2f, delegate ()
        {
            Info.Callback?.Invoke();
            Find<Text>("bg/item/txt").text = "";
            GameApp.ViewManager.Close(ViewId);
        });
    }
}
