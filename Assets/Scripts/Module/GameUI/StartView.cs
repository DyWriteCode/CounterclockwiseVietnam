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
                    KeyId = keyid,
                    IsStop = GameApp.ArchiveManager.DataToArchiveNormal(key, GameApp.SoundManager.IsStop),
                    BgmVolume = GameApp.ArchiveManager.DataToArchiveNormal(key, GameApp.SoundManager.BgmVolume),
                    EffectVolume = GameApp.ArchiveManager.DataToArchiveNormal(key, GameApp.SoundManager.EffectVolume),
                    IsDebug = GameApp.ArchiveManager.DataToArchiveNormal(key, GameApp.DebugManager.IsDebug),
                }, "SetArchive"));
                // test 
                Debug.Log(GameApp.ArchiveManager.ArchiveToDataList(key, GameApp.ArchiveManager.DataToArchiveList(key, new List<int>() { 1, 2, 3 })).ToString());
                //Debug.Log((int)GameApp.ArchiveManager.ArchiveToDataNormal(key, GameApp.ArchiveManager.DataToArchiveNormal(key, 10)));
                //Debug.Log((float)GameApp.ArchiveManager.ArchiveToDataNormal(key, GameApp.ArchiveManager.DataToArchiveNormal(key, 10.5f)));
                //Debug.Log((string)GameApp.ArchiveManager.ArchiveToDataNormal(key, GameApp.ArchiveManager.DataToArchiveNormal(key, "test")));
                // GameApp.ArchiveManager.DataToArchiveList(key, new List<int>() { 1, 2, 3 });
                //Dictionary<string, string> keyValues = new Dictionary<string, string>() { { "1", "1" }, { "2", "2" } };
                //GameApp.ArchiveManager.DataToArchiveDict(key, keyValues);
                //StartCoroutine(GameApp.ArchiveManager.SaveArchive(new TestAchive
                //{
                //KeyId = keyid,
                //list1 = GameApp.ArchiveManager.DataToArchive(key, new List<string>() {
                //    "1",
                //    "2",
                //    "3",
                //    "4",
                //    "5",
                //})
                //dict1 = GameApp.ArchiveManager.DataToArchive(key, new Dictionary<string, string>()
                //{
                //    {"1", "1"},
                //    {"2", "2"},
                //    {"3", "3"},
                //    {"4", "4"},
                //    {"5", "5"},
                //})
                //}, "TestArchive"));
                GameApp.TimerManager.Register(0.2f, delegate ()
                {
                    Tools.ExitGame(); // 退出游戏
                });
            },
            noCallBack = delegate ()
            {

            },
            MsgIxt = "你确定要退出游戏吗？"
        });
        // test
        // Controller.ApplyControllerFunc(ControllerType.Dialogue, Defines.OpenDialogueView, GameApp.DialogueManager.GetDialogueInfos(GameApp.ConfigManager.GetConfigData("dialogue"), 10001));
    }

    public override void Open(params object[] args)
    {
        base.Open(args);
        // 在开始就初始化好音频
        SetArchive archive = GameApp.ArchiveManager.LoadArchive<SetArchive>("SetArchive");
        // test
        TestAchive test = GameApp.ArchiveManager.LoadArchive<TestAchive>("TestArchive");
        if (archive.IsRight != false)
        {
            // GameApp.SoundManager.IsStop = (bool)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.IsStop);
            GameApp.SoundManager.IsStop = true;
            // GameApp.SoundManager.BgmVolume = (float)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.BgmVolume);
            GameApp.SoundManager.BgmVolume = 1.0f;
            // GameApp.SoundManager.EffectVolume = (float)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.EffectVolume);
            GameApp.SoundManager.EffectVolume = 1.0f;
            // GameApp.DebugManager.IsDebug = (bool)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], archive.IsDebug);
            GameApp.DebugManager.IsDebug = true;
            // test
            // List<string> list = (List<string>)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], test.list1);
            // for (int i = 0; i < list.Count - 1; i++)
            // {
            //     Debug.Log(list[i]);
            // }
            // Dictionary<string, string> dict = (Dictionary<string, string>)GameApp.ArchiveManager.ArchiveToData(AESKey.AESKEYS[archive.KeyId], test.dict1);
            //foreach (var item in dict)
            // { 
            //     Debug.Log(item.Key + "-" + item.Value);
            // }
        }
    }
}
