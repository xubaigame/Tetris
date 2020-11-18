/****************************************************
    文件：LoseGameView.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/18 10:38:44
	功能：失败界面View类
*****************************************************/

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameView : BaseView
{
    public Text CurrentScore;
    private GameDataModel _gameData;
    public override string Name
    {
        get => Consts.V_LoseGame;
    }

    public override void RegisterAttationEvents()
    {
        RegisterAttationEvent(Consts.E_EnterLoseGameView);
    }
    public override void HandleEvent(string eventName, params object[] datas)
    {
        if(eventName.Equals(Consts.E_EnterLoseGameView))
        {
            _gameData = GetModel(Consts.M_GameData) as GameDataModel;
            EnterView();
        }

    }

    public void EnterView()
    {
        CurrentScore.text = _gameData.CurrentScore.ToString();
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

    public void OnRestartGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_GameEnd);
        LeaveView();
        SendEvent(Consts.E_EnterGameView,true);
    }

    public void OnBackHomeButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_GameEnd);
        LeaveView();
        SendEvent(Consts.E_LeaveGameView);
        SendEvent(Consts.E_EnterMenuView, true);
    }
}