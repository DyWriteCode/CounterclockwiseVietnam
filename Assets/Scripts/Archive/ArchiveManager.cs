using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Game.Archive.AES;
using Game.Archive.Data;
using Game.Common.Tools;
using UnityEngine;

namespace Game.Archive
{
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
        public static Dictionary<string, string> TypeIdentifier = new Dictionary<string, string>
    {
        { "string", "10001" },
        { "int", "10002" },
        { "float", "10003" },
        { "bool", "10004" },
        { "list", "10005" },
        { "dict", "10006" }
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

        public void SaveEditorArchive(ArchiveData save, string fileName)
        {
            if (File.Exists(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin"))
            {
                File.Delete(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin");
                File.Delete(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin.meta");
            }
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
                Debug.Log("The game has been saved");
            }
            else
            {
                Debug.Log("The game has not been saved");
            }
            return;
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

        public void DelEditorArchive(string fileName)
        {
            if (File.Exists(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin"))
            {
                File.Delete(Application.streamingAssetsPath + "/PlayerData" + $"/{fileName}.bin");
                Debug.Log("The game was deleted successfully");
            }
            else
            {
                Debug.Log("The game was not deleted successfully");
            }
        }

        // 游戏存档的内容加密
        public string DataToArchiveNormal(string key, System.Object value)
        {
            string result = "";
            if (value.GetType() == typeof(string))
            {
                result = $"{TypeIdentifier["string"]}[{_AES.EncryptString(key, value.ToString())}]";
            }
            else if (value.GetType() == typeof(int))
            {
                result = $"{TypeIdentifier["int"]}[{_AES.EncryptString(key, value.ToString())}]";
            }
            else if (value.GetType() == typeof(float))
            {
                result = $"{TypeIdentifier["float"]}[{_AES.EncryptString(key, value.ToString())}]";
            }
            else if (value.GetType() == typeof(bool))
            {
                result = $"{TypeIdentifier["bool"]}[{_AES.EncryptString(key, value.ToString())}]";
            }
            return _AES.EncryptString(key, result);
        }

        public string DataToArchiveList(string key, IEnumerable value)
        {
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.Append($"{TypeIdentifier["list"]}["); // 开始包裹列表
            bool isFirst = true; // 用于在连接字符串时跳过第一个分隔符
            foreach (var item in value)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    resultBuilder.Append("<;>");
                }
                // 根据元素类型递归处理或直接添加标识符
                resultBuilder.Append(ProcessItem(key, item));
            }
            resultBuilder.Append(']'); // 结束包裹列表
            return _AES.EncryptString(key, resultBuilder.ToString());
        }

        // 辅助方法，用于递归处理列表中的每个元素
        private string ProcessItem(string key, object item)
        {
            if (item is IEnumerable enumerableItem && (item is string) == false)
            {
                // 如果元素是IEnumerable类型（列表）且不是字符串，递归调用ProcessList
                return DataToArchiveList(key, (IEnumerable)item);
            }
            else if (item is Dictionary<object, object> dictItem)
            {
                // 如果元素是字典，递归调用ProcessDictionary
                return DataToArchiveDict(key, (Dictionary<object, object>)item);
            }
            else
            {
                return DataToArchiveNormal(key, item);
            }
        }

        public string DataToArchiveDict(string key, System.Object value)
        {
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.Append($"{TypeIdentifier["dict"]}["); // 开始包裹字典
            bool isFirst = true;
            if (value is IDictionary)
            {
                foreach (DictionaryEntry kvp in (value as IDictionary))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        resultBuilder.Append("<;>");
                    }
                    // 对于字典中的每个键值对，递归调用ProcessItem处理键和值
                    // 键不是字符串类型时，需要添加相应的标识符
                    string processedKey = ProcessItem(key, kvp.Key.ToString());
                    string processedValue = ProcessItem(key, kvp.Value.ToString());
                    resultBuilder.Append($"{processedKey}<->{processedValue}");
                }
            }
            resultBuilder.Append(']'); // 结束包裹字典
            return _AES.EncryptString(key, resultBuilder.ToString());
        }



        // 游戏存档的内容解密
        public System.Object ArchiveToDataNormal(string key, string value)
        {
            if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["string"])
            {
                return _AES.DecryptString(key, Tools.CutString(_AES.DecryptString(key, value), "[", "]"));
            }
            if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["int"])
            {
                return int.Parse(_AES.DecryptString(key, Tools.CutString(_AES.DecryptString(key, value), "[", "]")));
            }
            if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["float"])
            {
                return float.Parse(_AES.DecryptString(key, Tools.CutString(_AES.DecryptString(key, value), "[", "]")));
            }
            if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["bool"])
            {
                return _AES.DecryptString(key, Tools.CutString(_AES.DecryptString(key, value), "[", "]")) == "True";
            }
            return null;
        }

        public System.Object ArchiveToDataList(string key, string value)
        {
            List<object> result = new List<object>();
            if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["list"])
            {
                string[] temp = Tools.CutString(_AES.DecryptString(key, value), "[", "]").Split("<;>");
                foreach (var item in temp)
                {
                    result.Add(DecProcessItem(key, item));
                }
            }
            return result;
        }

        private System.Object DecProcessItem(string key, string value)
        {
            if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["list"])
            {
                return ArchiveToDataList(key, value);
            }
            else if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["dict"])
            {
                return ArchiveToDataDict(key, value);
            }
            else
            {
                return ArchiveToDataNormal(key, value);
            }
        }

        public System.Object ArchiveToDataDict(string key, string value)
        {
            Dictionary<object, object> result = new Dictionary<object, object>();
            // test
            // Debug.Log(value);
            if (_AES.DecryptString(key, value).Substring(0, 5) == TypeIdentifier["dict"])
            {
                string[] temp = Tools.CutString(_AES.DecryptString(key, value), "[", "]").Split("<;>");
                foreach (var item in temp)
                {
                    string[] tempItem = item.Split("<->");
                    // test
                    //Debug.Log(DecProcessItem(key, tempItem[0]));
                    //Debug.Log(DecProcessItem(key, tempItem[1]));
                    result[DecProcessItem(key, tempItem[0])] = DecProcessItem(key, tempItem[1]);
                }
            }
            return result;
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

}