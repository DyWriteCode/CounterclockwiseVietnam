using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 存档系统管理类
/// 只完成了基本几种数据类型的加密与存储
/// 1 如果是直接存放游戏对象的话呢 它的列表以及字典就不需要进行加密
/// 2 如果是嵌套的字典 只需加密嵌套那一部分外部字典不需要加密
/// 3 如果是嵌套的列表 最外层列表不需加密，只需加密最内层列表
/// </summary>
public class ArchiveManager
{
    // {"type", "type id"}
    public Dictionary<string, string> TypeIdentifier = new Dictionary<string, string>
    {
        { "string", "10001" },
        { "int", "10002" },
        { "float", "10003" },
        { "list", "10004" },
        { "dict", "10005" }
    };

// 游戏存档
public IEnumerator SaveArchive(ArchiveData save, string fileName)
    {
        // 创建一个二进制格式化程序
        BinaryFormatter bf = new BinaryFormatter();  // 引入命名空间using System.Runtime.Serialization.Formatters.Binary;
        // 创建一个文件流（就是在Assets中创建的一个文件夹名称为StreamFile,然后系统会给我创建一个byBin的文本文档用于保存信息）
        FileStream fileStream = File.Create(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin");  //引入命名空间using System.IO;
        // 用二进制格式化程序的序列化方法来序列化Save对象，参数：创建的文件流和需要系列化对象
        bf.Serialize(fileStream, save);
        // 关闭流
        fileStream.Close();
        //检测这个二进制文件是否存在，也就是是否保存成功
        if (File.Exists(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin"))
        {
             GameApp.ViewManager.Open(ViewType.TipView, "游戏已存档");
        }
        else
        {
            GameApp.ViewManager.Open(ViewType.TipView, "游戏存档失败");
        }
        yield return null;
    }

    // 游戏读档
    public T LoadArchive<T>(string fileName) where T : ArchiveData, new()
    {
        // 如果该二进制文件存在的话
        if (File.Exists(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin"))
        {
            // 反系列化的过程
            // 创建一个二进制格式化程序
            BinaryFormatter bf = new BinaryFormatter();
            // 打开一个文件流
            FileStream fileStream = File.Open(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin", FileMode.Open);
            // 调用格式化程序的反序列化方法，将文件流转换为一个Save对象
            T save = (T)bf.Deserialize(fileStream);
            // 关闭文件流
            fileStream.Close();
            GameApp.ViewManager.Open(ViewType.TipView, "游戏读档成功");
            return save;
        }
        else
        {
            GameApp.ViewManager.Open(ViewType.TipView, "游戏读档失败");
            return new T
            {
                IsRight = false
            };
        }
    }

    // 游戏删档
    public void DelArchive(string fileName)
    {
        if (File.Exists(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin"))
        {
            File.Delete(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin");
            GameApp.ViewManager.Open(ViewType.TipView, "游戏删档成功");
        }
        else
        {
            GameApp.ViewManager.Open(ViewType.TipView, "游戏删档失败");
        }
    }

    // 游戏存档的内容加密
    public string DataToArchiveNormal(string key, System.Object value)
    {
        string result = "";
        if (value.GetType() == typeof(string))
        {
            result = _AES.EncryptString(key, $"{TypeIdentifier["string"]}{value}");
        }
        else if (value.GetType() == typeof(int))
        {
            result = _AES.EncryptString(key, $"{TypeIdentifier["int"]}{value}");
        }
        else if (value.GetType() == typeof(float))
        {
            result = _AES.EncryptString(key, $"{TypeIdentifier["float"]}{value}");
        }
        else if (value.GetType() == typeof(bool))
        {
            result = _AES.EncryptString(key, $"{TypeIdentifier["bool"]}{value}");
        }
        return _AES.EncryptString(key, result);
    }

    public string DataToArchiveList<T>(string key, T value)
    {
        string result = "";
        if (value.GetType() != typeof(T))
        {
            result = _AES.EncryptString(key, $"{TypeIdentifier["list"]}");
        }
        else 
        {
            result = TypeIdentifier["list"];
            foreach (var item in (value as List<T>))
            {
                if (typeof(List<>).IsAssignableFrom(item.GetType()) == false || typeof(Dictionary<,>).IsAssignableFrom(item.GetType()) == false)
                {
                    result += DataToArchiveNormal(key, item);
                    result += ";";
                    continue;
                }
                if (typeof(List<>).IsAssignableFrom(item.GetType()) == true)
                { 
                    result += DataToArchiveList<T>(key, item);
                    result += ";";
                    continue;
                }
            }
        }
        return _AES.EncryptString(key, result);
    }

    public string DataToArchiveDict<TKey, TValue>(string key, System.Object value)
    {
        string result = "";
        return result;
    }


    // 游戏存档的内容解密
    public System.Object ArchiveToData(string key, Dictionary<string, string> value)
    {
        if (value.ContainsKey("string"))
        {
            return _AES.DecryptString(key, value["string"]);
        }
        if (value.ContainsKey("int"))
        {
            return int.Parse(_AES.DecryptString(key, value["int"]));
        }
        if (value.ContainsKey("float"))
        {
            return float.Parse(_AES.DecryptString(key, value["float"]));
        }
        if (value.ContainsKey("bool"))
        {
            return _AES.DecryptString(key, value["bool"]) == "True";
        }
        if (value.ContainsKey("list"))
        {
            List<string> temp = new List<string>(_AES.DecryptString(key, value["list"]).Split(";"));
            string temp2;
            for (int i = 0; i < temp.Count - 1; i++)
            {
                temp2 = _AES.DecryptString(key, temp[i]);
                temp[i] = temp2;
            }
            return temp;
        }
        if (value.ContainsKey("dict"))
        {
            List<string> temp = new List<string>(_AES.DecryptString(key, value["dict"]).Split(";"));
            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i = 0; i < temp.Count - 1; i++)
            {
                 result[_AES.DecryptString(key, temp[i]).Split("-")[0]] = _AES.DecryptString(key, temp[i]).Split("-")[1];
            }
            return result;
        }
        return null;
    }

    public ArchiveManager()
    {
        // 保存AESKEY 由于建立了一个密钥库 废弃的这种方式
        PlayerPrefs.SetString("AESKEY", "a3a2f89bdad3d49ba6f260221b2e717b");
        // test
        //SaveArchive(new TestAchive
        //{
        //    hello = "test",
        //    tags = new List<string>() { "test", "test"},
        //    tags2 = new Dictionary<string, string>() { { "test", "test"} }
        //}, "test");
        //Debug.Log(LoadArchive<TestAchive>("test").hello);
        //Debug.Log(LoadArchive<TestAchive>("test").tags[0]);
        //Debug.Log(LoadArchive<TestAchive>("test").tags2["test"]);
    }
}
