using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 对话消息,信息类
/// </summary>
public class TalkInfo
{
    public string Name;
    public string MsgIxt;
    public string ImgPath = null;
    public string ImgPathLeft = null;
    public string ImgPathRight = null;
    public string Turn;
    public System.Action Callback;
}

/// <summary>
/// 剧情对话页面
/// </summary>
public class TalkView : BaseView
{
    TalkInfo Info;

    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("NextBtn").onClick.AddListener(onBtn);
        Find<Image>("CharImg/Right").enabled = false;
        Find<Image>("CharImg/Left").enabled = false;
    }

    public override void Open(params object[] args)
    {
        Info = args[0] as TalkInfo;
        Find<Text>("NameTxt").text = $"{Info.Name}:";
        // Find<Text>("ContentTxt").text = Info.MsgIxt;
        TypeWriterEffect(0.2f, Info.MsgIxt, Find<Text>("ContentTxt"));
        if (Info.Turn == "All")
        {
            Find<Image>("CharImg/Right").enabled = true;
            Find<Image>("CharImg/Left").enabled = true;
            Find<Image>("CharImg/Right").sprite = Resources.Load<Sprite>(Info.ImgPathRight);
            Find<Image>("CharImg/Left").sprite = Resources.Load<Sprite>(Info.ImgPathLeft);
        }
        else
        {
            Find<Image>($"CharImg/{Info.Turn}").enabled = true;
            Find<Image>($"CharImg/{Info.Turn}").sprite = Resources.Load<Sprite>(Info.ImgPath);
        }
    }

    private void onBtn()
    {
        Info.Callback?.Invoke();
        GameApp.ViewManager.Close(ViewId);
    }

    // 打字机效果
    private void TypeWriterEffect(float charsPer, string words, Text text)
    {
        text.text = "";
        float charsPerSecond = Mathf.Max(0.2f, charsPer);
        GameApp.TimerManager.Register(charsPerSecond, delegate ()
        {
            for (int i = 0; i < words.Length + 1; i++)
            {
                text.text = words.Substring(0, i);//刷新文本显示内容
            }
        });
    }
}
