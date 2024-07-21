using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

/// <summary>
/// 视图信息类
/// </summary>
public class ViewInfo
{
    public string PrefabName; // 视图预制体名称
    public Transform parentTf; // 所在父级
    public BaseController controller; // 视图所属控制器
    public int Sorting_Order; // 显示层级(改变显示顺序)
}


/// <summary>
/// 视图管理器
/// </summary>
public class ViewManager
{
    public Transform canvasTf; // 画布组件
    public Transform worldCanvasTf; // 世界画布组件
    private Dictionary<int, IBaseView> _opens; // 开启中的视图
    private Dictionary<int, IBaseView> _viewCache; // 视图缓存
    private Dictionary<int, ViewInfo> _views; // 已经注册的视图信息

    public ViewManager()
    {
        canvasTf = GameObject.Find("Canvas").transform;
        worldCanvasTf = GameObject.Find("WorldCanvas").transform;
        _opens = new Dictionary<int, IBaseView>();
        _viewCache = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
    }

    //注册视图信息
    public void Reister(int key, ViewInfo viewInfo)
    {
        if (_views.ContainsKey(key) == false)
        {
            _views.Add(key, viewInfo);
        }
    }

    //注册视图信息
    public void Reister(ViewType viewType, ViewInfo viewInfo)
    {
        Reister((int)viewType, viewInfo);
    }

    // 注销视图信息
    public void UnReister(int key)
    {
        if (_views.ContainsKey(key))
        {
            _views.Remove(key);
        }
    }

    // 移除视图
    public void RemoveView(int key)
    {
        _views.Remove(key);
        _viewCache.Remove(key);
        _opens.Remove(key);
    }

    // 移除控制器中的面板视图
    public void RemoveViewByController(BaseController ctl)
    {
        foreach(var item in _views)
        {
            if (item.Value.controller == ctl)
            {
                RemoveView(item.Key);
            }
        }
    }


    // 视图是否开启
    public bool IsOpen(int key)
    {
        return _opens.ContainsKey(key);
    }

    // 获得某一个视图
    public IBaseView GetView(int key)
    {
        if (_opens.ContainsKey(key))
        {
            return _opens[key];
        }
        if (_viewCache.ContainsKey(key))
        {
            return _viewCache[key];
        }
        return null;
    }

    public T GetView<T>(int key) where T : class, IBaseView
    {
        IBaseView view = GetView(key);
        if (view != null)
        {
            return view as T;
        }
        return null;
    }

    // 销毁视图
    public void Destroy(int key)
    {
        IBaseView oldView = GetView(key);
        if (oldView != null)
        {
            UnReister(key);
            oldView.DestroyView();
            _viewCache.Remove(key);
        }
    }

    // 关闭面板视图
    public void Close(int key, params object[] args)
    {
        // 没有打开
        if(IsOpen(key) == false)
        {
            return;
        }
        IBaseView view = GetView(key);
        if (view != null)
        {
            _opens.Remove(key);
            view.Close(args);
            _views[key].controller.CloseView(view);
        }
    }

    // 打开某一个视图面板
    public void Open(int key, params object[] args)
    {
        IBaseView view = GetView(key);

        ViewInfo viewInfo = _views[key];
        if (view == null) 
        {
            // 不存在的视图进行资源加载
            string type = ((ViewType)key).ToString(); // 类型字符串与脚本相对应
            GameObject uiObj = UnityEngine.Object.Instantiate(Resources.Load($"View/{viewInfo.PrefabName}"), viewInfo.parentTf) as GameObject;
            Canvas canvas = null;
            Canvas can = null;
            if (uiObj.TryGetComponent<Canvas>(out can) == true)
            {
                canvas = can;
            }
            if (canvas == null)
            {
                canvas = uiObj.AddComponent<Canvas>();
            }
            if (uiObj.GetComponent<GraphicRaycaster>() == null)
            {
                uiObj.AddComponent<GraphicRaycaster>();
            }
            canvas.overrideSorting = true; // 可以设置层级
            canvas.sortingOrder = viewInfo.Sorting_Order; // 设置对应的层级
            view = uiObj.AddComponent(System.Type.GetType(type)) as IBaseView;
            view.ViewId = key; // 设置视图ID
            view.Controller = viewInfo.controller; // 设置视图控制器
            // 添加到视图缓存
            _viewCache.Add(key, view);
            viewInfo.controller.OnLoadView(view);
        }
        // 视图已打开
        if (this._opens.ContainsKey(key) == true)
        {
            return;
        }
        this._opens.Add(key, view);
        // 视图面板已经初始化过了
        if (view.IsInit() == true)
        {
            view.SetVisible(true); // 显示
            view.Open(args); // 打开
            viewInfo.controller.OpenView(view);
        }
        else
        {
            view.InitUI();
            view.InitData();
            view.Open(args);
            viewInfo.controller.OpenView(view);
        }
    }

    // 打开某一个视图面板
    public void Open(ViewType viewType, params object[] args)
    {
        Open((int)viewType, args);
    }

    // 显示伤害数字
    public void ShowHitNum(string num, Color color, Vector3 pos) 
    { 
        GameObject obj = UnityEngine.Object.Instantiate(Resources.Load("View/HitNum"), worldCanvasTf) as GameObject;
        obj.transform.position = pos;
        obj.transform.DOMove(pos + Vector3.up * 1.75f, 0.65f).SetEase(Ease.OutBack);
        UnityEngine.Object.Destroy(obj, 0.75f);
        Text hitTxt = obj.GetComponent<Text>();
        hitTxt.text = num;
        hitTxt.color = color;
    }

    // 关闭所有页面
    public void CloseAll()
    {
        List<IBaseView> list = _opens.Values.ToList();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Close(list[i].ViewId);
        }
    }
}
