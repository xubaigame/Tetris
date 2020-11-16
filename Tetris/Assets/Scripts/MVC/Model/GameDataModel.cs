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

    public void InitData()
    {
        ReadData();
        SendEvent(Consts.E_EnterMenuView,true);
    }

    public void ReadData()
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
}