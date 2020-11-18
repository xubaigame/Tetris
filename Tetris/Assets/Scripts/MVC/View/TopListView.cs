/****************************************************
    文件：TopListView.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/16 23:41:20
	功能：排行榜界面View类
*****************************************************/

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TopListView : BaseView
{
    public Text txt_HeightestScore;
    public Text txt_GameCount;


    private GameDataModel m_GameDataModel;
    public override string Name
    {
        get =>Consts.V_TopList;
    }
    public override void RegisterAttationEvents()
    {
        RegisterAttationEvent(Consts.E_EnterTopListView);
        RegisterAttationEvent(Consts.E_ClearDataFinished);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        if(eventName.Equals(Consts.E_EnterTopListView))
        {
            m_GameDataModel = MVCSystem.GetModel(Consts.M_GameData) as GameDataModel;
            EnterView();
        }
        else if(eventName.Equals(Consts.E_ClearDataFinished))
        {
            txt_GameCount.text = m_GameDataModel.GameCount.ToString();
            txt_HeightestScore.text = m_GameDataModel.HightestScore.ToString();
        }
    }

    public void EnterView()
    {
        transform.GetChild(0).localScale = Vector3.zero;
        txt_GameCount.text = m_GameDataModel.GameCount.ToString();
        txt_HeightestScore.text = m_GameDataModel.HightestScore.ToString();

        gameObject.SetActive(true);
        transform.GetChild(0).DOScale(Vector3.one, 0.5f);
    }
    public void LeaveView()
    {
        txt_GameCount.text = string.Empty;
        txt_HeightestScore.text = string.Empty;
        transform.GetChild(0).DOScale(Vector3.zero, 0.5f).onComplete=()=>
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

    public void OnClearDataButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        SendEvent(Consts.E_ClearDataStart);
    }
}