using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承MonoBehaviour的脚本,需要挂载物体,跳转脚本后当前脚本不删除
/// </summary>
public class GameScenes : MonoBehaviour
{
    public Texture2D mouseTxt; // 鼠标图标
    private float dt;
    private static bool isLoaded = false;

    private void Awake()
    {
        if (isLoaded == true)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
            GameApp.Instance.Init();
        }
    }

    void Start()
    {
        // 设置鼠标样式
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);
        // 播放背景音乐
        GameApp.SoundManager.PlayBGM("login");
        RegisterConfigs(); // 注册所有配置表
        GameApp.ConfigManager.LoadAllConfigs(); // 加载所有配置表
        //测试配置表
        // ConfigData tempData = GameApp.ConfigManager.GetConfigData("enemy");
        // string name = tempData.GetDataById(10001)["Name"];
        // Debug.Log(name);
        RegisterModule(); // 注册控制器
        InitModule(); // 初始化所有控制器
    }

    // 注册控制器
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameApp.ControllerManager.Register(ControllerType.Upgrade, new UpgradeController());
        GameApp.ControllerManager.Register(ControllerType.Level, new LevelController());
        GameApp.ControllerManager.Register(ControllerType.Fight, new FightController());
    }

    // 初始化所有控制器
    void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }

    // 注册配置表
    void RegisterConfigs()
    {
        GameApp.ConfigManager.Register("enemy", new ConfigData("enemy"));
        GameApp.ConfigManager.Register("level", new ConfigData("level"));
        GameApp.ConfigManager.Register("option", new ConfigData("option"));
        GameApp.ConfigManager.Register("player", new ConfigData("player"));
        GameApp.ConfigManager.Register("role", new ConfigData("role"));
        GameApp.ConfigManager.Register("skill", new ConfigData("skill"));
    }
    
    void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}
