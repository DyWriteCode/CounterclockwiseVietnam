using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统一定义游戏中的管理器,在这个类里面进行初始化
/// </summary>
public class GameApp : Singleton<GameApp>
{
    public static SoundManager SoundManager; // 音频管理器定义
    public static ControllerManager ControllerManager;// 控制器管理器定义
    public static ViewManager ViewManager; // 视图管理器定义
    public static ConfigManager ConfigManager; // 配置表管理器定义
    public static CameraManager CameraManager; // 摄像机管理器定义
    public static MessageCenter MsgCenter; // 消息(事件)管理器(中心)定义 / 事件监听
    public static TimerManager TimerManager; // 计时器管理器定义
    public static FightWorldManager FightManager; // 战斗管理器定义
    public static MapManager MapManager; // 地图管理器定义
    public static GameDataManager GameDataManager; // 游戏数据管理器定义
    public static UserInputManager UserInputManager; // 用户控制管理器定义
    public static CommandManager CommandManager; // 命令管理器定义
    public static SkillManager SkillManager; // 技能管理器定义
    public static HelperManager HelperManager; // 帮助器管理器定义
    public static ArchiveManager ArchiveManager; // 存档管理器定义

    // 初始化所有管理器
    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
        ConfigManager = new ConfigManager();
        CameraManager = new CameraManager();
        MsgCenter = new MessageCenter();
        TimerManager = new TimerManager();
        FightManager = new FightWorldManager();
        MapManager = new MapManager();
        GameDataManager = new GameDataManager();
        UserInputManager = new UserInputManager();
        CommandManager = new CommandManager();
        SkillManager = new SkillManager();
        HelperManager = new HelperManager();
        ArchiveManager = new ArchiveManager();
    }

    public override void Update(float dt)
    {
        TimerManager.OnUpdate(dt);
        FightManager.Update(dt);
        UserInputManager.Update();
        CommandManager.Update(dt);
        SkillManager.Update(dt);
    }
}
