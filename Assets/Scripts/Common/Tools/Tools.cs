using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Game.Common.Tools
{
    /// <summary>
    /// 开发工具类
    /// </summary>
    public static class Tools
    {
        // 临时使用的变量
        public static int test = 0;
        public static int test2 = 0;
        public static int test3 = 0;

        // 设置图标
        public static void SetIcon(this UnityEngine.UI.Image img, string res)
        {
            img.sprite = Resources.Load<Sprite>($"Icon/{res}");
        }

        //检测鼠标位置是杏有2d碰撞物休
        public static void ScreenPointToRay2D(Camera cam, Vector2 mousePos, System.Action<Collider2D> callback)
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
            callback?.Invoke(col);
        }

        //检测鼠标位置是杏有2d碰撞物休
        public static Collider2D ScreenPointToRay2D(Camera cam, Vector2 mousePos)
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            Collider2D col = Physics2D.OverlapCircle(worldPos, 0.02f);
            return col;
        }

        // 占位函数 
        public static void PassProgram()
        {
            Debug.Log("use a pass program, please remember to clear it");
            return;
        }

        // 退出游戏
        public static void ExitGame()
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        }

        // 截取字符串
        public static string CutString(string s, string s1, string s2)
        {
            int n1, n2;
            n1 = s.IndexOf(s1, 0) + s1.Length;
            n2 = s.IndexOf(s2, n1);
            return s.Substring(n1, n2 - n1);
        }

        // 转换变量类型
        public static T ChangeType<T>(object value)
        {
            // 获取类型的默认值
            T defaultValue = default(T);
            // 检查value是否为null
            if (value == null)
            {
                return defaultValue;
            }
            // 尝试直接转换
            try
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                // 如果直接转换失败，尝试使用更复杂的逻辑
                Type targetType = typeof(T);

                // 检查是否是集合类型
                if (typeof(IList).IsAssignableFrom(targetType))
                {
                    // 检查value是否是集合
                    if (value is IEnumerable)
                    {
                        Type elementType = targetType.GetGenericArguments()[0];
                        var list = new List<object>();
                        foreach (var item in (IEnumerable)value)
                        {
                            list.Add(ChangeType(elementType, item));
                        }
                        return (T)list.ConvertToType(targetType);
                    }
                }
                // 检查是否是字典类型
                else if (typeof(IDictionary).IsAssignableFrom(targetType))
                {
                    if (value is IDictionary)
                    {
                        Type keyType = targetType.GetGenericArguments()[0];
                        Type valueType = targetType.GetGenericArguments()[1];
                        IDictionary dict = (IDictionary)value;
                        var newDict = new Dictionary<object, object>();
                        foreach (DictionaryEntry entry in dict)
                        {
                            newDict.Add(ChangeType(keyType, entry.Key), ChangeType(valueType, entry.Value));
                        }
                        return (T)newDict.ConvertToType(typeof(Dictionary<,>).MakeGenericType(keyType, valueType));
                    }
                }
            }
            return defaultValue;
        }

        // 辅助方法，用于将List<object>转换为泛型列表
        private static object ConvertToType(this IList list, Type targetType)
        {
            var genericType = targetType.GetGenericArguments()[0];
            var convertedList = (IList)Activator.CreateInstance(targetType);
            foreach (var item in list)
            {
                convertedList.Add(ChangeType(genericType, item));
            }
            return convertedList;
        }

        // 辅助方法，用于将Dictionary<object, object>转换为泛型字典
        private static object ConvertToType(this IDictionary dict, Type targetType)
        {
            Type keyType = targetType.GetGenericArguments()[0];
            Type valueType = targetType.GetGenericArguments()[1];
            var convertedDict = (IDictionary)Activator.CreateInstance(targetType);
            foreach (DictionaryEntry entry in dict)
            {
                convertedDict[ChangeType(keyType, entry.Key)] = ChangeType(valueType, entry.Value);
            }
            return convertedDict;
        }

        // 辅助方法，用于将object转换为指定的类型
        private static object ChangeType(Type targetType, object value)
        {
            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
        }
    }
}
