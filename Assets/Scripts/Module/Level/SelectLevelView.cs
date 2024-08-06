using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common;
using UnityEngine.UI;

/// <summary>
/// 选择关卡信息视图
/// </summary>
public class SelectLevelView : BaseView
{
    protected override void OnStart()
    {
        base.OnStart();
        // 换一首音乐
        GameApp.SoundManager.PlayBGM("mapbgm");
        Find<Button>("close").onClick.AddListener(onCloseBtn);
        Find<Button>("level/fightBtn").onClick.AddListener(onFightBtn);
    }

    //显示关卡详情面板
    public void ShowLevelDes()
    {
        Find("level").SetActive(true);
        LevelData current = Controller.GetModel<LevelModel>().current;
        Find<Text>("level/name/txt").text = current.Name;
        Find<Text>("level/des/txt").text = current.Des;
    }

    //隐藏关卡详情面板
    public void HideLevelDes()
    {
        Find("level").SetActive(false);
    }

    // 关闭按钮回调(返回开始界面)
    private void onCloseBtn()
    {
        // 关闭掉本面板
        GameApp.ViewManager.Close(ViewId);
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "game";
        loadingModel.callback = delegate ()
        {
            // 打开开始面板
            Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenStartView);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }

    // 挑战按钮回调(转到战斗界面)
    private void onFightBtn()
    {
        // 关闭当前界面
        GameApp.ViewManager.Close(ViewId);
        // 摄像机重置位置
        GameApp.CameraManager.ResetPos();
        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = Controller.GetModel<LevelModel>().current.SceneName; // 跳转的战斗场景名称
        loadingModel.callback = delegate ()
        {
            // 加载成功后显示战斗等面板
            Controller.ApplyControllerFunc(ControllerType.Fight, Defines.BeginFight);
        };
        Controller.ApplyControllerFunc(ControllerType.Loading, Defines.LoadingScene, loadingModel);
    }
}
