using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机管理器
/// </summary>
public class CameraManager
{
    /// <summary>
    /// 摄像机
    /// </summary>
    private Transform camTf;
    /// <summary>
    /// 摄像机之前的位置
    /// </summary>
    private Vector3 prePos; 

    /// <summary>
    /// 初始化
    /// </summary>
    public CameraManager() 
    {
        camTf = Camera.main.transform;
        prePos = camTf.transform.position;
    }

    /// <summary>
    /// 设置摄像机位置
    /// </summary>
    /// <param name="pos">要移动到的位置</param>
    public void SetPos(Vector3 pos)
    {
        pos.z = camTf.position.z;
        camTf.transform.position = pos;
    }

    /// <summary>
    /// 还原摄像机位置
    /// </summary>
    public void ResetPos()
    {
        camTf.transform.position = prePos;
    }
}
