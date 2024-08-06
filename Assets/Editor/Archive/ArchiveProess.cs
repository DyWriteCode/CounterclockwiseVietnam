using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Excel;
using System.Data;
using System.IO;
using UnityEngine.UI;
using Game.Archive.Data;
using Game.Archive.AES;

/// <summary>
/// 对存档进行处理
/// </summary>
public static class ArchiveProess
{
    [MenuItem("Tools/Archive/CreateNewArchive")]
    public static void Create()
    {
        int keyid = Random.Range(0, 99);
        string key = AESKey.AESKEYS[keyid];
        GameApp.Instance.Init();
        GameApp.ArchiveManager.SaveEditorArchive(new SetArchive
        {
            KeyId = keyid,
            IsStop = GameApp.ArchiveManager.DataToArchiveNormal(key, false),
            BgmVolume = GameApp.ArchiveManager.DataToArchiveNormal(key, 1.0f),
            EffectVolume = GameApp.ArchiveManager.DataToArchiveNormal(key, 1.0f),
            IsDebug = GameApp.ArchiveManager.DataToArchiveNormal(key, true),
        }, "SetArchive");
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Archive/DelAllArchive")]
    public static void Del()
    {
        GameApp.Instance.Init();
        GameApp.ArchiveManager.DelEditorArchive("SetArchive");
        AssetDatabase.Refresh();
    }
}
