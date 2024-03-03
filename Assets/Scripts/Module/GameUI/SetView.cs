using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置音量面板
/// </summary>
public class SetView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();
        Find<Button>("bg/closeBtn").onClick.AddListener(onCloseBtn);
        Find<Toggle>("bg/IsOpnSound").onValueChanged.AddListener(onIsStopBtn);
        Find<Slider>("bg/soundCount").onValueChanged.AddListener(onSliderBgmBtn);
        Find<Slider>("bg/effectCount").onValueChanged.AddListener(onSliderSoundEffectBtn);
        // 初始值设定
        Find<Toggle>("bg/IsOpnSound").isOn = GameApp.SoundManager.IsStop;
        Find<Slider>("bg/soundCount").value = GameApp.SoundManager.BgmVolume;
        Find<Slider>("bg/effectCount").value = GameApp.SoundManager.EffectVolume;
    }

    // 关闭设置面板按钮
    private void onCloseBtn()
    {
        GameApp.ViewManager.Close(ViewId); // 关闭掉自己
    }

    // 是否静音
    private void onIsStopBtn(bool isStop)
    {
        GameApp.SoundManager.IsStop = isStop;
    }

    // 设置bgm音量
    private void onSliderBgmBtn(float val)
    {
        GameApp.SoundManager.BgmVolume = val;
    }

    // 设置音效音量
    private void onSliderSoundEffectBtn(float val)
    {
        GameApp.SoundManager.EffectVolume = val;
    }
}
