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
                int keyid = Random.Range(0, 99);
                string key = AESKey.AESKEYS[keyid];
                StartCoroutine(GameApp.ArchiveManager.SaveArchive(new SetArchive
                {
                    IsStop = GameApp.ArchiveManager.DataToArchive(key, GameApp.SoundManager.IsStop),
                    BgmVolume = GameApp.ArchiveManager.DataToArchive(key, GameApp.SoundManager.BgmVolume),
                    EffectVolume = GameApp.ArchiveManager.DataToArchive(key, GameApp.SoundManager.EffectVolume),
                    IsDebug = GameApp.ArchiveManager.DataToArchive(key, GameApp.DebugManager.IsDebug),
                    KeyId = keyid,
                }, "SetArchive")); 
                GameApp.TimerManager.Register(0.2f, delegate ()
                {
                    Application.Quit(); // 退出游戏
                });
            },
            noCallBack = delegate ()
            {

            },
            MsgIxt = "你确定要退出游戏吗？"
        });
        // test
        //Controller.ApplyControllerFunc(ControllerType.Dialogue, Defines.OpenDialogueView, GameApp.DialogueManager.GetDialogueInfos(GameApp.ConfigManager.GetConfigData("dialogue"), 10001));
    }

    public override void Open(params object[] args)
    {
        base.Open(args);
        // 在开始就初始化好音频
        SetArchive archive = GameApp.ArchiveManager.LoadArchive<SetArchive>("SetArchive");
        if (archive.IsRight != false)
        {
            GameApp.SoundManager.IsStop = (bool)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.IsStop);
            GameApp.SoundManager.BgmVolume = (float)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.BgmVolume);
            GameApp.SoundManager.EffectVolume = (float)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.EffectVolume);
            GameApp.DebugManager.IsDebug = (bool)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.IsDebug);
        }
    }
}
