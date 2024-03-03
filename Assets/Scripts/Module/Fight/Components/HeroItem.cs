using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 处理英雄图标拖拽的脚本
/// </summary>
public class HeroItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Dictionary<string, string> data;

    void Start()
    {
        transform.Find("icon").GetComponent<Image>().SetIcon(data["Icon"]);
    }

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    // 开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Open(ViewType.DragHeroView, data["Icon"]);
    }

    // 结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        GameApp.ViewManager.Close((int)ViewType.DragHeroView);
        // 检测拖提后的位立是否有block脚本
        Tools.ScreenPointToRay2D(eventData.pressEventCamera, eventData.position, delegate (Collider2D col) 
        {
            if (col != null)
            {
                Block b = col.GetComponent<Block>();
                if (b != null)
                {
                    // 有方块
                    // Debug.Log(b);
                    Destroy(gameObject); // 删除图像
                    // 创建英雄物体
                    GameApp.FightManager.AddHero(b, data);
                }  
            }
        });
    }


    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
