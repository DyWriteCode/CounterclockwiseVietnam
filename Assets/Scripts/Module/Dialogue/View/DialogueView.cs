using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 对话消息,信息类
/// </summary>
public class DialogueInfo
{
    public string Name;
    public string MsgIxt;
    public string ImgPath = null;
    public string ImgPathLeft = null;
    public string ImgPathRight = null;
    public string Type;
    public string AniName = null;
    public System.Action Callback;
}

/// <summary>
/// 剧情对话页面
/// </summary>
public class DialogueView : BaseView
{
    DialogueInfo Info;
    List<DialogueInfo> Infos;
    Animator AniLeft;
    Animator AniRight;

    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("NextBtn").onClick.AddListener(onBtn);
        AniLeft = Find<Image>("CharImg/left").GetComponent<Animator>();
        AniRight = Find<Image>("CharImg/right").GetComponent<Animator>();
    }

    public override void Open(params object[] args)
    {
        Find<Image>("CharImg/right").sprite = null;
        Find<Image>("CharImg/left").sprite = null;
        Find<Image>("CharImg/right").enabled = false;
        Find<Image>("CharImg/left").enabled = false;
        Infos = args[0] as List<DialogueInfo>;
        if (Infos.Count == 0)
        {
            GameApp.ViewManager.Close(ViewId);
            return;
        }
        Info = Infos[0];
        Find<Text>("NameTxt").text = $"{Info.Name}:";
        // Find<Text>("ContentTxt").text = Info.MsgIxt;
        StartCoroutine(GameApp.DialogueManager.TypeWriterEffect(Info.MsgIxt.Length * 3f, Info.MsgIxt, Find<Text>("ContentTxt")));
        if (Info.Type == "all")
        {
            Find<Image>("CharImg/right").enabled = true;
            Find<Image>("CharImg/left").enabled = true;
            Find<Image>("CharImg/right").sprite = Resources.Load<Sprite>(Info.ImgPathRight ?? Info.ImgPath);
            Find<Image>("CharImg/left").sprite = Resources.Load<Sprite>(Info.ImgPathLeft ?? Info.ImgPath);
        }
        else if (Info.Type =="none")
        {
            Find<Image>("CharImg/right").enabled = false;
            Find<Image>("CharImg/left").enabled = false;
        }
        else if (Info.Type == "left")
        {
            Find<Image>($"CharImg/left").enabled = true;
            Find<Image>($"CharImg/left").sprite = Resources.Load<Sprite>(Info.ImgPathLeft);
        }
        else if (Info.Type == "right")
        {
            Find<Image>($"CharImg/right").enabled = true;
            Find<Image>($"CharImg/right").sprite = Resources.Load<Sprite>(Info.ImgPathRight);
        }
        if (Info.AniName != "null" && Info.Type == "all" || Info.AniName != "")
        {
            AniLeft.Play(Info.AniName);
            AniRight.Play(Info.AniName);
        }
        else if (Info.AniName != "null" && Info.Type == "left" || Info.AniName != "")
        {
            AniLeft.Play(Info.AniName);
        }
        else if (Info.AniName != "null" && Info.Type == "right" || Info.AniName != "")
        {
            AniRight.Play(Info.AniName);
        }
    }

    private void onBtn()
    {
        Info.Callback?.Invoke();
        Infos.Remove(Info);
        GameApp.ViewManager.Close(ViewId);
        GameApp.ViewManager.Open(ViewType.DialogueView, Infos);
    }
}
