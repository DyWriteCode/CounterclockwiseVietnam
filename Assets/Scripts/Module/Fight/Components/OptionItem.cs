using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 选项
/// </summary>
public class OptionItem : MonoBehaviour
{
    OptionData op_data;

    public void Init(OptionData data)
    {
        op_data = data;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate ()
        {
            GameApp.MsgCenter.PostTempEvent(op_data.EventValue); // 执行配置表中所设定的Event
            GameApp.ViewManager.Close((int)ViewType.SelectOptionView); // 关闭选项界面
        });
        transform.Find("txt").GetComponent<Text>().text = op_data.Name; // 给Button添加文字
    }
}

