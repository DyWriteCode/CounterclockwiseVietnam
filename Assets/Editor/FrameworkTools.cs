using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using Sirenix.Utilities;

public class FrameworkTools : OdinMenuEditorWindow
{
    [MenuItem("FrameworkTools/FrameworkTools")]
    private static void OpenWindow()
    {
        var window = GetWindow<FrameworkTools>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(720, 720);
        window.titleContent = new GUIContent("FrameworkTools");
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        //这里的第一个参数为窗口名字，第二个参数为指定目录，第三个参数为需要什么类型，第四个参数为是否在家该文件夹下的子文件夹
        tree.AddAllAssetsAtPath("FrameworkTools", "Assets/Editor/Configs", typeof(ScriptableObject), true);
        return tree;
    }
}