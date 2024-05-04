using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设置方面的存档
/// </summary>
[System.Serializable]
public class SetArchive : ArchiveData
{
    public bool IsStop;
    public float BgmVolume;
    public float EffectVolume;
    public bool IsDebug;
}
