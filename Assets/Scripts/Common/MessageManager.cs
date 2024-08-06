using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    /// <summary>
    /// 消息处理中心脚本
    /// </summary>
    public class MessageManager
    {
        private Dictionary<string, System.Action<object>> msgDic; // 存储普通消息的字典
        private Dictionary<string, System.Action<object>> tempMsgDic; // 存储临时消息的字典, 执行完后移除
        private Dictionary<System.Object, Dictionary<string, System.Action<object>>> objMsgDic; // 存储特定游戏对象的消息字典

        public MessageManager()
        {
            msgDic = new Dictionary<string, System.Action<object>>();
            tempMsgDic = new Dictionary<string, System.Action<object>>();
            objMsgDic = new Dictionary<object, Dictionary<string, System.Action<object>>>();
        }

        // 添加事件
        public void AddEvent(string eventName, System.Action<object> callback)
        {
            if (msgDic.ContainsKey(eventName))
            {
                msgDic[eventName] += callback;
            }
            else
            {
                msgDic.Add(eventName, callback);
            }
        }

        // 移除事件
        public void RemoveEvent(string eventName, System.Action<object> callback)
        {
            if (msgDic.ContainsKey(eventName))
            {
                msgDic[eventName] -= callback;
                if (msgDic[eventName] == null)
                {
                    msgDic.Remove(eventName);
                }
            }
        }

        // 执行事件
        public void PostEvent(string eventName, object args = null)
        {
            if (msgDic.ContainsKey(eventName))
            {
                msgDic[eventName].Invoke(args);
            }
        }

        // 添加对象事件
        public void AddEvent(System.Object ListenerObj, string eventName, System.Action<object> callback)
        {
            if (objMsgDic.ContainsKey(ListenerObj))
            {
                if (objMsgDic[ListenerObj].ContainsKey(eventName))
                {
                    objMsgDic[ListenerObj][eventName] += callback;
                }
                else
                {
                    objMsgDic[ListenerObj].Add(eventName, callback);
                }
            }
            else
            {
                Dictionary<string, System.Action<object>> _tempDic = new Dictionary<string, System.Action<object>>
            {
                { eventName, callback }
            };
                objMsgDic.Add(ListenerObj, _tempDic);
            }
        }

        // 移除对象事件
        public void RemoveEvent(System.Object ListenerObj, string eventName, System.Action<object> callback)
        {
            if (objMsgDic.ContainsKey(ListenerObj))
            {
                if (objMsgDic[ListenerObj].ContainsKey(eventName))
                {
                    objMsgDic[ListenerObj][eventName] -= callback;
                    if (objMsgDic[ListenerObj][eventName] == null)
                    {
                        objMsgDic[ListenerObj].Remove(eventName);
                        if (objMsgDic[ListenerObj].Count == 0)
                        {
                            objMsgDic.Remove(ListenerObj);
                        }
                    }
                }
            }
        }

        // 移除对象所有事件
        public void RemoveObjAllEvent(System.Object ListenerObj, string eventName, System.Action<object> callback)
        {
            if (objMsgDic.ContainsKey(ListenerObj))
            {
                objMsgDic.Remove(ListenerObj);
            }
        }

        // 执行对象事件
        public void PostEvent(System.Object ListenerObj, string eventName, System.Object args = null)
        {
            if (objMsgDic.ContainsKey(ListenerObj))
            {
                if (objMsgDic[ListenerObj].ContainsKey(eventName))
                {
                    objMsgDic[ListenerObj][eventName].Invoke(args);
                }
            }
        }

        // 添加缓存事件
        public void AddTempEvent(string eventName, System.Action<object> callback)
        {
            if (tempMsgDic.ContainsKey(eventName))
            {
                tempMsgDic[eventName] = callback; // 添加的临时事件 是要覆盖的 并不是累加
            }
            else
            {
                tempMsgDic.Add(eventName, callback);
            }
        }

        // 执行缓存事件
        public void PostTempEvent(string eventName, object args = null)
        {
            if (tempMsgDic.ContainsKey(eventName))
            {
                tempMsgDic[eventName].Invoke(args);
                tempMsgDic[eventName] = null;
                tempMsgDic.Remove(eventName);
            }
        }
    }

}