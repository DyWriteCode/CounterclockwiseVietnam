using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自动删除物体
/// </summary>
public class DestroyObj : MonoBehaviour
{
    public float Timer;

    void Start()
    {
        Destroy(gameObject, Timer);
    }
}
