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
    #region 字段
    private int _maxCameraSize = 18;
    private int _minCameraSize = 13;
    private int _currentScore = 0;
    private int _hightestScore = 0;
    private int _gameCount = 0;
    private int _mute = 0;
    private bool _isPlaying = false;
    #endregion

    #region 属性
    public override string Name
    {
        get => Consts.M_GameData;
    }
    public int MinCameraSize { get => _minCameraSize; }
    public int MaxCameraSize { get => _maxCameraSize; }
    public int CurrentScore { get => _currentScore; }
    public int HightestScore { get => _hightestScore; }
    public int GameCount { get => _gameCount; }
    public int Mute { get => _mute; }
    public bool IsPlaying { get => _isPlaying; }
    #endregion

    /// <summary>
    /// 数据模型初始化
    /// </summary>
    public void InitData()
    {
        LoadData();
        SendEvent(Consts.E_EnterMenuView,true);
    }

    /// <summary>
    /// 加载本地保存数据
    /// </summary>
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

    /// <summary>
    /// 将内存数据保存至本地
    /// </summary>
    public void SaveData()
    {
        
        PlayerPrefs.SetInt(Consts.P_GameCount,_gameCount);
        PlayerPrefs.SetInt(Consts.P_HightestScore, _hightestScore);
        PlayerPrefs.SetInt(Consts.P_Mute, Mute);
    }

    /// <summary>
    /// 得分方法
    /// </summary>
    /// <param name="score">得分</param>
    public void AddCurrentScore(int score)
    {
        _currentScore += score;
        if(_currentScore>_hightestScore)
        {
            _hightestScore = _currentScore;
        }
        SendEvent(Consts.E_AddCurrentScoreFinished);
    }

    /// <summary>
    /// 清除本地保存数据
    /// </summary>
    public void ClearData()
    {
        if (IsPlaying)
        {
            _gameCount = 1;
            _hightestScore = _currentScore;
        }
        else
        {
            _gameCount = 0;
            _hightestScore = 0;
        } 
        SendEvent(Consts.E_ClearDataFinished);
    }

    /// <summary>
    /// 静音/接触静音
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

    /// <summary>
    /// 开始一局新游戏
    /// </summary>
    public void NewGame()
    {
        _currentScore = 0;
        _gameCount++;
        _isPlaying = true;
    }

    /// <summary>
    /// 游戏结束方法
    /// </summary>
    public void GameEnd()
    {
        _isPlaying = false;
        SaveData();
    }
}