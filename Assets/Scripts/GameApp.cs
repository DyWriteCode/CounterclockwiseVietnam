using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Archive;
using Game.Config;
using Game.Common;
using Game.Helper;

/// <summary>
/// 统一定义游戏中的管理器,在这个类里面进行初始化
/// </summary>
public class GameApp : Singleton<GameApp>
{
    /// <summary>
    /// 音频管理器
    /// </summary>
    public static SoundManager SoundManager;
    /// <summary>
    /// 控制器管理器
    /// </summary>
    public static ControllerManager ControllerManager;
    /// <summary>
    /// 视图管理器
    /// </summary>
    public static ViewManager ViewManager;
    /// <summary>
    /// 配置表管理器
    /// </summary>
    public static ConfigManager ConfigManager;
    /// <summary>
    /// 摄像机管理器定义
    /// </summary>
    public static CameraManager CameraManager;
    /// <summary>
    /// 消息(事件)管理器(中心) / 事件监听
    /// </summary>
    public static MessageManager MessageManager;
    /// <summary>
    /// 计时器管理器
    /// </summary>
    public static TimerManager TimerManager;
    /// <summary>
    /// 战斗管理器
    /// </summary>
    public static FightWorldManager FightManager;
    /// <summary>
    /// 地图管理器定义
    /// </summary>
    public static MapManager MapManager;
    /// <summary>
    /// 游戏数据管理器定义
    /// </summary>
    public static GameDataManager GameDataManager;
    /// <summary>
    /// 用户控制管理器
    /// </summary>
    public static UserInputManager UserInputManager;
    /// <summary>
    /// 命令管理器
    /// </summary>
    public static CommandManager CommandManager;
    /// <summary>
    /// 技能管理器
    /// </summary>
    public static SkillManager SkillManager;
    /// <summary>
    /// 帮助器管理器
    /// </summary>
    public static HelperManager HelperManager;
    /// <summary>
    /// 存档管理器
    /// </summary>
    public static ArchiveManager ArchiveManager;
    /// <summary>
    /// 剧情对话管理器
    /// </summary>
    public static DialogueManager DialogueManager;
    /// <summary>
    /// Debug管理器
    /// </summary>
    public static DebugManager DebugManager;
    /// <summary>
    /// 网络管理器
    /// </summary>
    public static NetManager NetManager;
    /// <summary>
    /// 基于AssetBundle的资源管理器
    /// </summary>
    public static ResourceManager ResourceManager;
    /// <summary>
    /// AssetBundle管理器
    /// </summary>
    public static AssetBundleManager AssetBundleManager;
    /// <summary>
    /// 基于ResourceManager的对象管理器
    /// </summary>
    public static ObjectManager ObjectManager;

    /// <summary>
    /// 初始化所有管理器
    /// </summary>
    public override void Init()
    {
        ObjectManager = new ObjectManager();
        ResourceManager = new ResourceManager();
        AssetBundleManager = new AssetBundleManager();
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        CameraManager = new CameraManager();
        MessageManager = new MessageManager();
        TimerManager = new TimerManager();
        FightManager = new FightWorldManager();
        MapManager = new MapManager();
        GameDataManager = new GameDataManager();
        UserInputManager = new UserInputManager();
        CommandManager = new CommandManager();
        SkillManager = new SkillManager();
        HelperManager = new HelperManager();
        ArchiveManager = new ArchiveManager();
        DialogueManager = new DialogueManager();
        DebugManager = new DebugManager();
        NetManager = new NetManager();
    }

    // dt 每针间隔时间
    public override void Update(float dt)
    {
        TimerManager.OnUpdate(dt);
        FightManager.Update(dt);
        UserInputManager.Update();
        CommandManager.Update(dt);
        SkillManager.Update(dt);
        NetManager.Update(dt);
    }
}
