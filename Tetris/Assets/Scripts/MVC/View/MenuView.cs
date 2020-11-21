/****************************************************
    文件：MenuView.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/16 20:20:38
	功能：菜单界面界面View类
*****************************************************/

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : BaseView
{
    // UI元素引用
    public Text gameTitle;
    public RectTransform buttonGroup;
    public Transform map;
    public GameObject restartButton;

    private GameDataModel _gameData;

    public override string Name
    {
        get => Consts.V_Menu;
    }

    /// <summary>
    /// 注册响应事件
    /// </summary>
    public override void RegisterAttationEvents()
    {
        //注册进入菜单界面响应事件
        RegisterAttationEvent(Consts.E_EnterMenuView);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        //响应进入菜单界面事件
        if (eventName.Equals(Consts.E_EnterMenuView))
        {
            if((bool)datas[0])
                EnterView(datas);
            _gameData = GetModel(Consts.M_GameData) as GameDataModel;
            AudioManager.Instance.SetMuteState(_gameData.Mute == 1 ? true : false);
            restartButton.SetActive(_gameData.IsPlaying);
        }
    }

    /// <summary>
    /// 进入界面方法
    /// </summary>
    /// <param name="datas">数据</param>
    private void EnterView(params object[] datas)
    {
        //map.localScale = Vector3.zero;
        gameObject.SetActive(true);

        gameTitle.rectTransform.DOAnchorPosY(0, 0.5f);

        buttonGroup.DOAnchorPosY(buttonGroup.sizeDelta.y/2, 0.5f);

        map.DOScale(Vector3.one, 0.5f);
    }

    /// <summary>
    /// 离开界面方法
    /// </summary>
    private void LeaveView()
    {
        gameTitle.rectTransform.DOAnchorPosY(gameTitle.rectTransform.sizeDelta.y, 0.5f);
        buttonGroup.DOAnchorPosY(-buttonGroup.sizeDelta.y / 2, 0.5f).onComplete=() =>
        {
            gameObject.SetActive(false);
        };
        
    }

    /// <summary>
    /// 开始游戏按钮点击事件
    /// </summary>
    public void OnPlayGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_EnterGameView,true);
        //SendEvent(Consts.E_EnterLoseGameView);
    }

    /// <summary>
    /// 继续游戏按钮点击事件
    /// </summary>
    public void OnRestartGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_ResumeGame);
        SendEvent(Consts.E_EnterGameView,false);
    }

    /// <summary>
    /// 打开设置界面按钮点击事件
    /// </summary>
    public void OnSettingGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_EnterSettingView);
    }

    /// <summary>
    /// 打开排行版界面点击事件
    /// </summary>
    public void OnTopListButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_EnterTopListView);
    }
}