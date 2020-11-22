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
    //View引用
    public BaseView MenuView;
    public BaseView GameView;
    public BaseView TopListView;
    public BaseView SettingView;
    public BaseView LoseGameView;
    public void Awake()
    {
        //获得当前屏幕分辨率,根据游戏设计分辨率（1080*1920）计算当前屏幕应该显示的窗口分辨率
        Screen.SetResolution(1080 * (Screen.currentResolution.height - 200) / 1920, Screen.currentResolution.height - 200, false);
    }
    public void Start()
    {
        //注册游戏开始事件
        MVCSystem.RegisterController(Consts.E_GameStart,typeof(GameStartController));

        //出发游戏开始事件
        MVCSystem.SendEvent(Consts.E_GameStart,MenuView,GameView,TopListView,SettingView, LoseGameView);
        
    }

    public void OnDestroy()
    {
        //关闭游戏是保存数据
        MVCSystem.SendEvent(Consts.E_SaveData);
    }
}