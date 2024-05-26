using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// .bin格式二进制的存档数据文件
/// 联机版起到一个缓存账号密码的作用
/// 单机版起到缓存所有信息的作用
/// 串行化 串行化是指存储和获取磁盘文件 内存或其他地方中的对象
/// </summary>
[System.Serializable]
public class ArchiveData
{
    public string SaveTime; // 存档的时间
    public int KeyId; // 存档的密钥对应的密钥库ID
    public bool IsRight = true; // 用于判断读档是否成功
}
