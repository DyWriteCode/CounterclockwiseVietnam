using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设置方面的存档
/// </summary>
[System.Serializable]
public class SetArchive : ArchiveData
{
    // 存档内容以这样的方式存 {"Type", AES("Value")}
    public Dictionary<string, string> IsStop; // 是否静音 bool
    public Dictionary<string, string> BgmVolume; // Bgm音量 float
    public Dictionary<string, string> EffectVolume; // Effect音量 float
    public Dictionary<string, string> IsDebug; // 是否debug bool
}
