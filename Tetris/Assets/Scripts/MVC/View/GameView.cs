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

    public Text txt_CurrentScore;
    public Text txt_HightestScore;
    private GameDataModel m_GameDataModel;
    public override string Name
    {
        get=> Consts.V_Game;
    }

    public override void RegisterAttationEvents()
    {
        RegisterAttationEvent(Consts.E_EnterGameView);
        RegisterAttationEvent(Consts.E_LeaveGameView);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        if(eventName.Equals(Consts.E_EnterGameView))
        {
            
            m_GameDataModel = MVCSystem.GetModel(Consts.M_GameData) as GameDataModel;
            if (m_GameDataModel.IsPlaying == false||(bool)datas[0])
                SendEvent(Consts.E_GameBegin);
            EnterView();

        }
        else if(eventName.Equals(Consts.E_LeaveGameView))
        {
            LeaveView();
        }
    }

    public void EnterView()
    {
        txt_CurrentScore.text = m_GameDataModel.CurrentScore.ToString();
        txt_HightestScore.text = m_GameDataModel.HightestScore.ToString();
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
            txt_CurrentScore.text = string.Empty;
            txt_HightestScore.text = string.Empty;
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