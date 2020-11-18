/****************************************************
    文件：SettingView.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/17 23:14:10
	功能：设置界面View类
*****************************************************/

using DG.Tweening;
using System.Collections;
using System.Threading;
using UnityEngine;

public class SettingView : BaseView
{
    private GameDataModel _gameData;
    public GameObject muteFlag;
    private bool _mute;

    public override string Name
    {
        get => Consts.E_EnterSettingView;
    }
    public override void RegisterAttationEvents()
    {
        RegisterAttationEvent(Consts.E_EnterSettingView);
        RegisterAttationEvent(Consts.E_ChangeMuteFinished);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        if(eventName.Equals(Consts.E_EnterSettingView))
        {
            //_mute = (GetModel(Consts.M_GameData) as GameDataModel).Mute == 1 ? true : false;
            _gameData = GetModel(Consts.M_GameData) as GameDataModel;
            _mute = _gameData.Mute == 0 ? false : true;
            EnterView();
        }
        if(eventName.Equals(Consts.E_ChangeMuteFinished))
        {
            //_mute = (int)datas[0]==0?false:true;
            _mute = _gameData.Mute == 0 ? false : true;
            AudioManager.Instance.SetMuteState(_mute);
            muteFlag.SetActive(_mute);
        }
    }

    public void EnterView()
    {
        muteFlag.SetActive(_mute);
        gameObject.SetActive(true);
        transform.GetChild(0).DOScale(Vector3.one, 0.5f);
    }
    public void LeaveView()
    {
        transform.GetChild(0).DOScale(Vector3.zero, 0.5f).onComplete = () =>
        {
            gameObject.SetActive(false);
        };
    }

    public void OnBackButtonDown()
    {
        //AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_EnterMenuView, false);
    }

    public void OnChangeMuteStateButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Balloon);
        SendEvent(Consts.E_ChangeMuteStart);
    }
}