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
    //UI元素引用
    public Text CurrentScore;

    //数据模型引用
    private GameDataModel _gameData;
    public override string Name
    {
        get => Consts.V_LoseGame;
    }

    /// <summary>
    /// 注册响应事件
    /// </summary>
    public override void RegisterAttationEvents()
    {
        //注册进入失败界面响应事件
        RegisterAttationEvent(Consts.E_EnterLoseGameView);
    }
    public override void HandleEvent(string eventName, params object[] datas)
    {
        //响应进入失败界面事件
        if (eventName.Equals(Consts.E_EnterLoseGameView))
        {
            _gameData = GetModel(Consts.M_GameData) as GameDataModel;
            EnterView();
        }

    }

    /// <summary>
    /// 进入界面方法
    /// </summary>
    public void EnterView()
    {
        CurrentScore.text = _gameData.CurrentScore.ToString();
        gameObject.SetActive(true);
        transform.GetChild(0).DOScale(Vector3.one, 0.5f);
    }

    /// <summary>
    /// 离开界面方法
    /// </summary>
    public void LeaveView()
    {
        transform.GetChild(0).DOScale(Vector3.zero, 0.5f).onComplete = () =>
        {
            CurrentScore.text = string.Empty;
            gameObject.SetActive(false);
        };
    }

    /// <summary>
    /// 开始新游戏按钮点击事件
    /// </summary>
    public void OnRestartGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_GameEnd,true);
        LeaveView();
        SendEvent(Consts.E_EnterGameView,true);
    }

    /// <summary>
    /// 返回菜单界面点击事件
    /// </summary>
    public void OnBackHomeButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_GameEnd,true);
        LeaveView();
        SendEvent(Consts.E_LeaveGameView);
        SendEvent(Consts.E_EnterMenuView, true);
    }
}