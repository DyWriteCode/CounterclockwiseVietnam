#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

/// <summary>
/// 针对打包后能够提前出现一个页面 用以调整分辨率大小 便于开发
/// </summary>
public class PostProcessLauncherCopy
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        string dataPath = Path.Combine(Path.GetDirectoryName(pathToBuiltProject), "PersistentDataPath.txt");
        using (StreamWriter file = new StreamWriter(dataPath))
        {
            file.WriteLine(Path.Combine(Application.companyName, Application.productName));
        }

        switch (target)
        {
            case BuildTarget.StandaloneWindows:
                {
                    string launcherDir = Path.Combine(Directory.GetCurrentDirectory(), "LauncherExecutable", "x86", "WindowsPlayer.exe");
                    FileUtil.ReplaceFile(launcherDir, pathToBuiltProject);
                }
                break;

            case BuildTarget.StandaloneWindows64:
                {
                    string launcherDir = Path.Combine(Directory.GetCurrentDirectory(), "LauncherExecutable", "x64", "WindowsPlayer.exe");
                    FileUtil.ReplaceFile(launcherDir, pathToBuiltProject);
                }
                break;

            default:
                break;
        }
    }
}
#endif
