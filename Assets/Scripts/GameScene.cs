using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common;
using Game.Config;
using UnityEngine.EventSystems;

/// <summary>
/// 继承MonoBehaviour的脚本,需要挂载物体,跳转脚本后当前脚本不删除
/// </summary>
public class GameScenes : SingletonMono<GameScenes>
{
    /// <summary>
    /// 鼠标图标
    /// </summary>
    public Texture2D mouseTxt;
    /// <summary>
    /// 保证在切换场景时不被删除的物体以节省资源
    /// </summary>
    public List<GameObject> KeepAlive = new List<GameObject>();
    /// <summary>
    /// 每针间隔时间
    /// </summary>
    private float dt;
    /// <summary>
    /// 是否加载完毕
    /// </summary>
    private static bool isLoaded = false;

    public override void Awake()
    {
        base.Awake();
        if (isLoaded == true)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoaded = true;
            // 手动创建事件系统
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            eventSystem.transform.SetParent(transform);
            DontDestroyOnLoad(gameObject);
            foreach (var item in KeepAlive)
            {
                DontDestroyOnLoad(item.gameObject);
            }
            GameApp.Instance.Init();
        }
    }

    public override void Start()
    {
        base.Start();
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

    public override void Update()
    {
        base.Update();
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }

    /// <summary>
    /// 注册控制器
    /// </summary>
    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Loading, new LoadingController());
        GameApp.ControllerManager.Register(ControllerType.Upgrade, new UpgradeController());
        GameApp.ControllerManager.Register(ControllerType.Level, new LevelController());
        GameApp.ControllerManager.Register(ControllerType.Fight, new FightController());
        GameApp.ControllerManager.Register(ControllerType.Dialogue, new DialogueController());
    }

    /// <summary>
    /// 初始化所有控制器
    /// </summary>
    void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }

    /// <summary>
    /// 注册配置表
    /// </summary>
    void RegisterConfigs()
    {
        GameApp.ConfigManager.Register("enemy", new ConfigData("enemy"));
        GameApp.ConfigManager.Register("level", new ConfigData("level"));
        GameApp.ConfigManager.Register("option", new ConfigData("option"));
        GameApp.ConfigManager.Register("player", new ConfigData("player"));
        GameApp.ConfigManager.Register("role", new ConfigData("role"));
        GameApp.ConfigManager.Register("skill", new ConfigData("skill"));
        GameApp.ConfigManager.Register("dialogue", new ConfigData("dialogue"));
        GameApp.ConfigManager.Register("massif", new ConfigData("massif"));
        GameApp.ConfigManager.Register("role", new ConfigData("role"));
        GameApp.ConfigManager.Register("pitfall", new ConfigData("pitfall"));
        GameApp.ConfigManager.Register("supplies", new ConfigData("supplies"));
    }
}
