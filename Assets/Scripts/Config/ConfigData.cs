using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 读取.csv格式的数据表(以","隔开的数据格式 )
/// 后期通过网络框架获取MySQL数据内容
/// </summary>
public class ConfigData
{
    // 后期这个数据表通过网络框架, 解析, 获取
    // Key是字典的ID, 值是每一行的数据
    private Dictionary<int, Dictionary<string, string>> datas; // 每一个存储表所存储的数据
    public string fileName; // 配置表文件名称

    public ConfigData(string fileName) {
        this.fileName = fileName;
        this.datas = new Dictionary<int, Dictionary<string, string>>();
    }

    public TextAsset LoadFile()
    {
        return Resources.Load<TextAsset>($"Data/{fileName}");
    }

    // 读取
    public void Load(string txt)
    {
        string[] dataArr = txt.Split('\n'); // 换行
        // 获取第一行数据作为每一行数据字典key的值
        string[] titleArr = dataArr[0].Trim().Split(','); // 逗号切割
        // 内容从第三行开始读起(下标从二开始)
        for (int i = 2; i < dataArr.Length; i++)
        {
            string[] tempArr = dataArr[i].Trim().Split(',');
            Dictionary<string, string> tempData = new Dictionary<string, string>();
            for (int j = 0; j < tempArr.Length; j++)
            {
                tempData.Add(titleArr[j], tempArr[j]);
            }
            datas.Add(int.Parse(tempData["Id"]), tempData);
        }
    }

    public Dictionary<string, string> GetDataById(int id)
    {
        if (datas.ContainsKey(id))
        {
            return datas[id];
        }
        return null;
    }

    public Dictionary<int, Dictionary<string, string>> GetLines()
    {
        return datas;
    }
}
