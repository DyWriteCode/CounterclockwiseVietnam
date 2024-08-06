using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Common.Tools;

namespace Game.Helper
{
    /// <summary>
    /// 一些类的帮助器管理器
    /// </summary>
    public class HelperManager
    {
        public TimeHelper TimeHelper = new();
        public JsonHelper JsonHelper = new();
        public ToolsActive Tools = new();
    }
}
