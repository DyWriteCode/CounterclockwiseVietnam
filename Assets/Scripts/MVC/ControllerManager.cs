using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;

/// <summary>
/// 控制器管理器
/// </summary>
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules; // 控制器存储字典

    public ControllerManager()
    {
        _modules = new Dictionary<int, BaseController>();
    }

    // 注册控制器
    public void Register(int controllerKey, BaseController ctl)
    {
        if (_modules.ContainsKey(controllerKey) == false)
        {
            _modules.Add(controllerKey, ctl);
        }
    }

    // 注册控制器
    public void Register(ControllerType controllerType, BaseController ctl)
    {
        Register((int)controllerType, ctl);
    }

    // 执行所有控制器init函数
    public void InitAllModules()
    {
        foreach(var item in _modules)
        {
            item.Value.Init();
        }
    }

    // 注销控制器
    public void UnRegister(int controllerKey)
    {
        if (_modules.ContainsKey(controllerKey) == false)
        {
            _modules.Remove(controllerKey);
        }
    }

    // 清除所有控制器
    public void Clear()
    {
        _modules.Clear();
    }

    public void ClearAllModules()
    {
        List<int> keys = _modules.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }
    }

    // 跨模块触发消息
    public void ApplyFunc(int controllerKey, string eventName, System.Object[] args)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            _modules[controllerKey].ApplyFunc(eventName, args);
        }
    }

    //获取某个控制器的model对象
    public BaseModel GetControllerModel(int controllerKey)
    {
        if ( _modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        }
        else
        {
            return null;
        }
    }
}
