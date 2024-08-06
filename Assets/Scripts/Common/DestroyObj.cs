using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自动删除物体
/// </summary>
namespace Game.Common
{
    public class DestroyObj : MonoBehaviour
    {
        public float Timer;

        void Start()
        {
            // 在指定时间后删除
            Destroy(gameObject, Timer);
        }
    }

}