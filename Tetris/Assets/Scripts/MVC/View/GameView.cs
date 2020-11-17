/****************************************************
    文件：GameView.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/16 21:52:45
	功能：游戏界面View类
*****************************************************/

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameView : BaseView
{
    public RectTransform CurrentScore;
    public RectTransform PauseGameButton;
    public RectTransform HightestScore;

    private GameDataModel m_GameDataModel;
    public override string Name
    {
        get=> Consts.V_Game;
    }

    public override void RegisterAttationEvents()
    {
        RegisterAttationEvent(Consts.E_EnterGameView);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        if(eventName.Equals(Consts.E_EnterGameView))
        {
            m_GameDataModel = MVCSystem.GetModel(Consts.M_GameData) as GameDataModel;
            EnterView();
        }
    }

    public void EnterView()
    {
        gameObject.SetActive(true);
        CurrentScore.DOAnchorPosY(-CurrentScore.sizeDelta.y/2, 0.5f);
        PauseGameButton.DOAnchorPosY(-CurrentScore.sizeDelta.y / 2, 0.5f);
        HightestScore.DOAnchorPosY(-HightestScore.sizeDelta.y / 2, 0.5f);
        Camera.main.DOOrthoSize(m_GameDataModel.MinCameraSize, 0.5f);
    }

    public void LeaveView()
    {
        CurrentScore.DOAnchorPosY(CurrentScore.sizeDelta.y / 2, 0.5f);
        PauseGameButton.DOAnchorPosY(CurrentScore.sizeDelta.y / 2, 0.5f);
        HightestScore.DOAnchorPosY(HightestScore.sizeDelta.y / 2, 0.5f).onComplete+=()=>
        {
            gameObject.SetActive(false);
        };
        Camera.main.DOOrthoSize(m_GameDataModel.MaxCameraSize, 0.5f);
        
    }

    public void OnPauseGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_EnterMenuView,true);
    }
}