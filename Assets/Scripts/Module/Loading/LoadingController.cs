using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common;
using UnityEngine.SceneManagement;

/// <summary>
/// 跳转,加载场景控制器
/// </summary>
public class LoadingController : BaseController
{
    AsyncOperation asyncOp;

    public LoadingController() : base()
    {
        // 注册加载游戏视图
        GameApp.ViewManager.Reister(ViewType.LoadingView, new ViewInfo()
        {
            PrefabName = "LoadingView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf
        });  
        InitModuleEvent(); // 初始化模块事件(全局事件已在UIController初始化,所以只需初始化本模块事件)
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.LoadingScene, loadSceneCallBack); // 注册并打开加载场景面板
    }

    // 加载场景的回调函数
    private void loadSceneCallBack(System.Object[] args)
    {
        LoadingModel loadingModel = args[0] as LoadingModel;
        SetModel(loadingModel);
        // 打开加载界面
        GameApp.ViewManager.Open(ViewType.LoadingView);
        // 加载场景
        asyncOp = SceneManager.LoadSceneAsync(loadingModel.SceneName);
        asyncOp.completed += onLoadedEndCallback;
    }

    // 加载完成后的回调函数
    private void onLoadedEndCallback(AsyncOperation asyncOp)
    {
        asyncOp.completed -= onLoadedEndCallback;
        // 稍微延迟一会儿，说不定后期有用
        GameApp.TimerManager.Register(0.25f, delegate ()
        {
            GetModel<LoadingModel>().callback?.Invoke(); // 执行回调函数
            GameApp.ViewManager.Close((int)ViewType.LoadingView); // 关闭掉加载面板
        });
    }
}
