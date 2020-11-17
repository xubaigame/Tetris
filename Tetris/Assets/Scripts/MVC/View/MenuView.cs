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

    public Text GameTitle;
    public RectTransform ButtonGroup;

    public Transform Map;

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
            if (datas.Length >= 2)
                AudioManager.Instance.SetMuteState((int)datas[1]==1?true:false);
            if((bool)datas[0])
                EnterView(datas);
        }
    }

    private void EnterView(params object[] datas)
    {
        gameObject.SetActive(true);

        GameTitle.rectTransform.DOAnchorPosY(0, 0.5f);

        ButtonGroup.DOAnchorPosY(ButtonGroup.sizeDelta.y/2, 0.5f);

        Map.DOScale(Vector3.one, 0.5f);
    }

    private void LeaveView()
    {
        GameTitle.rectTransform.DOAnchorPosY(GameTitle.rectTransform.sizeDelta.y, 0.5f);

        ButtonGroup.DOAnchorPosY(-ButtonGroup.sizeDelta.y / 2, 0.5f).onComplete=() =>
        {
            gameObject.SetActive(false);
        };
        
    }

    public void OnPlayGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_EnterGameView);
    }
    public void OnRestartGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
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