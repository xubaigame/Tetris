/****************************************************
    文件：GameDataModel.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/16 20:40:17
	功能：游戏数据Model类
*****************************************************/

using UnityEngine;

public class GameDataModel : BaseModel
{
    private int _maxCameraSize = 18;
    private int _minCameraSize = 13;
    private int _currentScore = 0;
    private int _hightestScore = 0;
    private int _gameCount = 0;
    private int _mute = 0;

    public override string Name
    {
        get => Consts.M_GameData;
    }
    public int MinCameraSize { get => _minCameraSize;}
    public int MaxCameraSize { get => _maxCameraSize;}
    public int CurrentScore { get => _currentScore;}
    public int HightestScore { get => _hightestScore;}
    public int GameCount { get => _gameCount;}
    public int Mute { get => _mute;}

    public void InitData()
    {
        LoadData();
        SendEvent(Consts.E_EnterMenuView,true,Mute);
    }

    public void LoadData()
    {
        if(PlayerPrefs.HasKey(Consts.P_GameCount))
        {
            _gameCount = PlayerPrefs.GetInt(Consts.P_GameCount);
        }
        if(PlayerPrefs.HasKey(Consts.P_HightestScore))
        {
            _hightestScore = PlayerPrefs.GetInt(Consts.P_HightestScore);
        }
        if(PlayerPrefs.HasKey(Consts.P_Mute))
        {
            _mute = PlayerPrefs.GetInt(Consts.P_Mute);
        }
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt(Consts.P_GameCount,_gameCount);
        PlayerPrefs.SetInt(Consts.P_HightestScore, _hightestScore);
        PlayerPrefs.SetInt(Consts.P_Mute, Mute);
    }

    /// <summary>
    /// 改变静音状态
    /// </summary>
    public void ChangeMuteState()
    {
        if (Mute==1)
        {
            _mute=0;
        }
        else if(Mute==0)
        {
            _mute = 1;
        }
        SendEvent(Consts.E_ChangeMuteFinished, Mute);
    }
}