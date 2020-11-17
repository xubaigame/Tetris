/****************************************************
    文件：AudioManager.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/17 23:48:12
	功能：声音管理类
*****************************************************/

using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    //单例模式
    public static AudioManager Instance = null;

    //音乐播放组件
    public AudioSource bgAudio;
    public AudioSource uiAudio;

    //静音标识
    private bool _mute = false;

    public void Start()
    {
        Instance = this;
    }

    /// <summary>
    /// 设置静音状态
    /// </summary>
    public void SetMuteState(bool state)
    {
        _mute = state;
        bgAudio.mute = _mute;
        uiAudio.mute = _mute;
    }


    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name">音乐名称</param>
    /// <param name="isLoop">循环状态</param>
    public void PlayBGMusic(string name, bool isLoop = true)
    {
        AudioClip ac = Resources.Load<AudioClip>("Audio/" + name);
        if (bgAudio.clip == null || bgAudio.clip.name != ac.name)
        {
            bgAudio.clip = ac;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name">音效名称</param>
    public void PlayUIMusic(string name)
    {
        AudioClip ac = Resources.Load<AudioClip>("Audio/" + name);
        uiAudio.clip = ac;
        uiAudio.Play();
    }
}