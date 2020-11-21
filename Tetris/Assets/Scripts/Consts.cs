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
    public readonly static string E_LeaveGameView = "LeaveGameView";
    public readonly static string E_EnterTopListView = "EnterTopListView";
    public readonly static string E_EnterSettingView = "EnterSettingView";
    public readonly static string E_EnterLoseGameView = "EnterLoseGameView";

    public readonly static string E_ChangeMuteStart = "ChangeMuteStart";
    public readonly static string E_ChangeMuteFinished = "ChangeMuteFinished";

    public readonly static string E_GameBegin = "GameBegin";
    public readonly static string E_GameEnd = "GameEnd";

    public readonly static string E_ClearDataStart = "ClearDataStart";
    public readonly static string E_ClearDataFinished = "E_ClearDataFinished";
    public readonly static string E_SaveData = "SaveData";
    public readonly static string E_AddCurrentScoreStart = "AddCurrentScoreStart";
    public readonly static string E_AddCurrentScoreFinished = "AddCurrentScoreFinished";

    public readonly static string E_ShapeMoveStart = "ShapeMoveStart";
    public readonly static string E_ShapeMoveFinished = "ShapeMoveFinished";
    public readonly static string E_ShapePlaceStart = "ShapePlaceStart";
    public readonly static string E_ShapePlaceFinished = "ShapePlaceFinished";
    public readonly static string E_ChangShaepDownSpeed = "ChangShaepDownSpeed";
    public readonly static string E_PauseGame = "PauseGame";
    public readonly static string E_ResumeGame = "ResumeGame";

    //ViewName
    public readonly static string V_Menu = "MenuView";
    public readonly static string V_Game = "GameView";
    public readonly static string V_TopList = "TopListView";
    public readonly static string V_Setting = "SettingView";
    public readonly static string V_LoseGame = "LoseGame";

    //ModelName
    public readonly static string M_GameData = "GameDataModel";
    public readonly static string M_Map = "MapModel";
    #endregion

    //PlayerPrefabs
    public readonly static string P_GameCount = "GameCount";
    public readonly static string P_HightestScore = "HightestScore";
    public readonly static string P_Mute = "Mute";


    //AudioName
    public readonly static string A_Cursor = "Cursor";
    public readonly static string A_Balloon = "Balloon";
    public readonly static string A_ShapDrop = "Drop";
    public readonly static string A_Lineclear = "Lineclear";
    public readonly static string A_Gameover = "Gameover";

    //Others
    public readonly static float ShapeDownSpeed = 0.8f;
    public readonly static float ShapeDownSpeedX2 = 0.1f;
    public readonly static int ClearLineScore = 100;
}