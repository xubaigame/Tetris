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

    public Text gameTitle;
    public RectTransform buttonGroup;
    public Transform map;
    public GameObject restartButton;

    private GameDataModel _gameData;

    public override string Name
    {
        get => Consts.V_Menu;
    }
    public override void RegisterAttationEvents()
    {
        RegisterAttationEvent(Consts.E_EnterMenuView);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        if(eventName.Equals(Consts.E_EnterMenuView))
        {
            if((bool)datas[0])
                EnterView(datas);
            _gameData = GetModel(Consts.M_GameData) as GameDataModel;
            AudioManager.Instance.SetMuteState(_gameData.Mute == 1 ? true : false);
            restartButton.SetActive(_gameData.IsPlaying);
        }
    }

    private void EnterView(params object[] datas)
    {
        gameObject.SetActive(true);

        gameTitle.rectTransform.DOAnchorPosY(0, 0.5f);

        buttonGroup.DOAnchorPosY(buttonGroup.sizeDelta.y/2, 0.5f);

        map.DOScale(Vector3.one, 0.5f);
    }

    private void LeaveView()
    {
        gameTitle.rectTransform.DOAnchorPosY(gameTitle.rectTransform.sizeDelta.y, 0.5f);

        buttonGroup.DOAnchorPosY(-buttonGroup.sizeDelta.y / 2, 0.5f).onComplete=() =>
        {
            gameObject.SetActive(false);
        };
        
    }

    public void OnPlayGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_EnterGameView,true);
        //SendEvent(Consts.E_EnterLoseGameView);
    }
    public void OnRestartGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_ResumeGame);
        SendEvent(Consts.E_EnterGameView,false);
    }
    public void OnSettingGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_EnterSettingView);
    }
    public void OnTopListButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_EnterTopListView);
    }
}