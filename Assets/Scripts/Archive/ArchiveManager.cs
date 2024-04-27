using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// 存档系统管理类
/// </summary>
public class ArchiveManager
{
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

    public ArchiveManager()
    {
        // 保存AESKEY
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
