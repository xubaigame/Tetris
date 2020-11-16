/****************************************************
    文件：Consts.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/16 20:17:54
	功能：游戏常量
*****************************************************/

public static class Consts 
{
    #region MVC

    //EventName
    public readonly static string E_GameStart = "GameStart";
    public readonly static string E_EnterMenuView = "EnterMenuView";
    public readonly static string E_EnterGameView = "EnterGameView";
    public readonly static string E_EnterTopListView = "EnterTopListView";

    //ViewName
    public readonly static string V_Menu = "MenuView";
    public readonly static string V_Game = "GameView";
    public readonly static string V_TopList = "TopList";

    //ModelName
    public readonly static string M_GameData = "GameDataModel";
    #endregion

    //PlayerPrefabs
    public readonly static string P_GameCount = "GameCount";
    public readonly static string P_HightestScore = "HightestScore";
    public readonly static string P_Mute = "Mute";

}