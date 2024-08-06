using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Archive.Data
{
    /// <summary>
    /// 设置方面的存档
    /// </summary>
    [System.Serializable]
    public class SetArchive : ArchiveData
    {
        // 存档内容以这样的方式存 {"Type", AES("Value")} => 废弃
        // 存档内容以这样的方式存 AES("type [value;value;value......]")
        public string IsStop; // 是否静音 bool
        public string BgmVolume; // Bgm音量 float
        public string EffectVolume; // Effect音量 float
        public string IsDebug; // 是否debug bool
    }
}