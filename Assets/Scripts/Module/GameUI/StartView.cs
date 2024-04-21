using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始游戏界面
/// </summary>
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("startBtn").onClick.AddListener(onStartGameBtn);
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onQuitGameBtn);
    }

    // 开始游戏
    private void onStartGameBtn()
    {
        // 关闭开始界面
        GameApp.ViewManager.Close(ViewId);
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "map";
        loadingModel.callback = delegate ()
        {
            // 打开选择关卡面板
            Controller.ApplyControllerFunc(ControllerType.Level, Defines.OpenSelectLevelView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }

    // 打开设置面极
    private void onSetBtn()
    {
        ApplyFunc(Defines.OpenSetView);
    }

    // 退出游戏
    private void onQuitGameBtn()
    {
        Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new Messagelnfo()
        {
            okCallback = delegate ()
            {
                Application.Quit(); // 退出游戏
            },
            noCallBack = delegate ()
            {

            },
            MsgIxt = "你确定要退出游戏吗？ "
        });
        //List<TalkInfo> test = new List<TalkInfo>
        //{
        //    new TalkInfo()
        //    {
        //        Name = "enemy",
        //        MsgIxt = "hi",
        //        Turn = "All",
        //        ImgPath = "icon/down",
        //        ImgPathLeft = "icon/up",
        //        ImgPathRight = "icon/down",
        //        Callback = delegate ()
        //        {

        //        }
        //    },
        //    new TalkInfo()
        //    {
        //        Name = "enemy",
        //        MsgIxt = "hi",
        //        Turn = "Left",
        //        ImgPath = "icon/down",
        //        ImgPathLeft = "icon/up",
        //        ImgPathRight = "icon/down",
        //        Callback = delegate ()
        //        {

        //        }
        //    },
        //    new TalkInfo()
        //    {
        //        Name = "enemy",
        //        MsgIxt = "hi",
        //        Turn = "Right",
        //        ImgPath = "icon/down",
        //        ImgPathLeft = "icon/up",
        //        ImgPathRight = "icon/down",
        //        Callback = delegate ()
        //        {

        //        }
        //    }
        //};
        //Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenTalkView, test);
    }
}
