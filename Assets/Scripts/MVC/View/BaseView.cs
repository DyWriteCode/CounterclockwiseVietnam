using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 继承MONO类调用IBV接口
/// </summary>
public class BaseView : MonoBehaviour, IBaseView
{
    public int ViewId { get; set; }
    public BaseController Controller { get; set; }
    protected Canvas _canvas;
    protected Dictionary<string, GameObject> m_cache_gos; // 缓存物体字典
    private bool _isInit = false; // 是否初始化

    private void Awake()
    {
        _canvas = gameObject.GetComponent<Canvas>();
        m_cache_gos = new Dictionary<string, GameObject>();
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }

    // 触发其他控制器事件
    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
    {
        this.Controller.ApplyControllerFunc(controllerKey, eventName, args);
    }

    // 触发本模块事件
    public void ApplyFunc(string eventName, params object[] args)
    {
        this.Controller.ApplyFunc(eventName, args);
    }

    public virtual void Close(params object[] args)
    {
        SetVisible(false); // 隐藏
    }

    public void DestroyView()
    {
        Controller = null;
        Destroy(gameObject);
    }

    public virtual void InitData()
    {
        _isInit = true;
    }

    // 初始化UI
    public virtual void InitUI()
    {
        
    }

    // 视图是否已经初始化
    public bool IsInit()
    {
        return _isInit;
    }

    // 视图是否显示
    public bool IsShow()
    {
        return _canvas.enabled == true;
    }

    public virtual void Open(params object[] args)
    {
        
    }

    public void SetVisible(bool value)
    {
        this._canvas.enabled = value;
    }

    public GameObject Find(string res)
    {
        if (m_cache_gos.ContainsKey(res))
        {
            return m_cache_gos[res];
        }
        m_cache_gos.Add(res, transform.Find(res).gameObject);
        return m_cache_gos[res];
    }

    public T Find<T>(string res) where T : Component
    {
        GameObject obj = Find(res);
        return obj.GetComponent<T>();
    }
}
