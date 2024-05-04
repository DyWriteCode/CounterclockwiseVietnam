using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特殊的view 用来在打包后测试日志的输出 定位bug
/// </summary>
class DebugView : MonoBehaviour
{
    // log结构体 规范日志结构
    struct Log
    {
        public string message;
        public string stackTrace;
        public LogType type;
    }
    public KeyCode toggleKey = KeyCode.Space; // 规定进入调试模式的按键
    public bool restrictLogCount = false; // 是否只保留一定数量的日志
    public int maxLogs = 1000; // 最大保留日志数量
    readonly List<Log> logs = new List<Log>();
    private Vector2 scrollPosition;
    private bool visible;
    private bool collapse;
    // 视觉元素
    static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
        };
    const string windowTitle = "Console";
    const int margin = 20;
    readonly GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
    readonly GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
    readonly Rect titleBarRect = new Rect(0, 0, 10000, 20);
    Rect windowRect = new Rect(margin, margin, Screen.width - (margin * 2), Screen.height - (margin * 2));

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            visible = !visible;
        }
    }

    void OnGUI()
    {
        if (!visible)
        {
            return;
        }
        windowRect = GUILayout.Window(123456, windowRect, DrawConsoleWindow, windowTitle);
    }

    // 显示列出记录的日志的窗口
    void DrawConsoleWindow(int windowID)
    {
        DrawLogsList();
        DrawToolbar();
        // 允许窗口通过其标题栏拖动
        GUI.DragWindow(titleBarRect);
    }

    // 显示可滚动的日志列表
    void DrawLogsList()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        // 循环访问记录的日志
        for (var i = 0; i < logs.Count; i++)
        {
            var log = logs[i];
            // 如果选择了折叠选项，则合并相同的消息
            if (collapse && i > 0)
            {
                var previousMessage = logs[i - 1].message;
                if (log.message == previousMessage)
                {
                    continue;
                }
            }
            GUIStyle style = new()
            {
                fontSize = 50,
                fontStyle = FontStyle.Bold,
            };
            style.normal.textColor = logTypeColors[log.type];
            GUILayout.Label($"[log]{log.message}", style);
        }
        GUILayout.EndScrollView();
        // 在绘制其他组件之前 确保 GUI 颜色已重置
        GUI.contentColor = Color.white;
    }

    // 显示用于筛选和更改日志列表的选项
    void DrawToolbar()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(clearLabel))
        {
            logs.Clear();
        }
        collapse = GUILayout.Toggle(collapse, collapseLabel, GUILayout.ExpandWidth(false));
        GUILayout.EndHorizontal();
    }

    // 记录日志回调中的日志
    void HandleLog(string message, string stackTrace, LogType type)
    {
        logs.Add(new Log
        {
            message = message,
            stackTrace = stackTrace,
            type = type,
        });
        TrimExcessLogs();
    }

    // 删除超过允许的最大数量的旧日志
    void TrimExcessLogs()
    {
        if (!restrictLogCount)
        {
            return;
        }
        var amountToRemove = Mathf.Max(logs.Count - maxLogs, 0);
        if (amountToRemove == 0)
        {
            return;
        }
        logs.RemoveRange(0, amountToRemove);
    }
}