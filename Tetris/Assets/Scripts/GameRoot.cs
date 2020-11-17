/****************************************************
    文件：GameRoot.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/16 20:1:21
	功能：游戏入口
*****************************************************/

using UnityEngine;

public class GameRoot : MonoBehaviour 
{
    public BaseView MenuView;
    public BaseView GameView;
    public BaseView TopListView;
    public BaseView SettingView;
    public void Start()
    {
        MVCSystem.RegisterController(Consts.E_GameStart,typeof(GameStartController));

        MVCSystem.SendEvent(Consts.E_GameStart,MenuView,GameView,TopListView,SettingView);
        
    }

    public void OnDestroy()
    {
        MVCSystem.SendEvent(Consts.E_SaveData);
    }
}