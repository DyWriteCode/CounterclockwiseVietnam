using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 针对打包后能够提前出现一个页面 用以调整分辨率大小 便于开发
/// </summary>
public class ClosingScript
{
    /// <summary>
    /// 退出时干什么
    /// </summary>
    private static void Quit()
    {
        List<string> settings = new List<string>
            {
                "Screenmanager Resolution Width",
                "Screenmanager Resolution Height",
                "Screenmanager Fullscreen mode",
                "Screenmanager Stereo 3D",
                "UnityGraphicsQuality",
                "UnitySelectMonitor"
            };

        using (StreamWriter file = new StreamWriter(Path.Combine(Application.persistentDataPath, "ScreenSelectorPrefs.txt")))
        {
            foreach (string key in settings)
            {
                file.WriteLine(PlayerPrefs.GetInt(key, 0).ToString());
            }
        }
    }

    /// <summary>
    /// 在程序刚运行起来的时候调用
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.quitting += Quit;
    }
}
