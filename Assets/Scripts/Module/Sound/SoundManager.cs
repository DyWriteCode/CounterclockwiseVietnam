using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource; // 播放bgm的音频组件
    private Dictionary<string, AudioClip> clips; // 音频缓存字典
    private bool isStop; // 是否静音
    public bool IsStop
    {
        get
        {
            return isStop;
        }
        set
        {
            isStop = value;
            if (isStop == true)
            {
                bgmSource.Pause();
            }
            else
            {
                bgmSource.Play();
            }
        }
    }
    private float bgmVolume; // bgm当前音量
    public float BgmVolume { 
        get => bgmVolume;
        set
        {
            bgmVolume = value;
            bgmSource.volume = bgmVolume;
        }
    }
    private float effectVolume; // 音效当前音量(攻击，受伤，防御等短音效)
    public float EffectVolume
    {
        get => effectVolume;
        set
        {
            effectVolume = value;
        }
    }

    public SoundManager()
    {
        clips = new Dictionary<string, AudioClip>();
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        isStop = false;
        bgmVolume = 1.0f;
        effectVolume = 1.0f;
    }

    public void PlayBGM(string res)
    {
        if (isStop == true)
        {
            return;
        }
        // 没有当前音频
        if (clips.ContainsKey(res) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip);
        }
        bgmSource.clip = clips[res];
        bgmSource.Play(); // 播放音频
    }

    public void PlayEffect(string name, Vector3 pos)
    {
        if (isStop == true)
        {
            return;
        }
        AudioClip clip = null;
        if (clips.ContainsKey(name) == false)
        {
            clip = Resources.Load<AudioClip>($"Sounds/{name}");
            clips.Add(name, clip);
        }
        AudioSource.PlayClipAtPoint(clips[name], pos);
    }
}
