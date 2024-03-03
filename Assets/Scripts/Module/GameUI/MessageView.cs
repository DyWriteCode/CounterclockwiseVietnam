using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通知消息,信息类
/// </summary>
public class Messagelnfo
{
    public string MsgIxt;
    public System.Action okCallback;
    public System.Action noCallBack;
}

/// <summary>
/// 提示.通知界面
/// </summary>
public class MessageView : BaseView
{
    Messagelnfo Info;

    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("okBtn").onClick.AddListener(onOkBtn);
        Find<Button>("noBtn").onClick.AddListener(onNoBtn);
    }

    public override void Open(params object[] args)
    {
        Info = args[0] as Messagelnfo;
        Find<Text>("content/txt").text = Info.MsgIxt;
    }

    private void onOkBtn()
    {
        Info.okCallback?.Invoke();
        GameApp.ViewManager.Close(ViewId);
    }

    private void onNoBtn()
    {
        Info.noCallBack?.Invoke();
        GameApp.ViewManager.Close(ViewId);
    }
}
