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
    //UI元素引用
    public RectTransform CurrentScore;
    public RectTransform PauseGameButton;
    public RectTransform HightestScore;

    public Text txt_CurrentScore;
    public Text txt_HightestScore;

    //方块预制体引用
    public Shape[] shapes;
    //颜色引用
    public Color[] colors;

    //数据模型引用
    private GameDataModel m_GameDataModel;
    private MapModel m_MapModel;

    //当前方块
    private Shape _currentShape;
    private float _timer = 0;
    public override string Name
    {
        get=> Consts.V_Game;
    }

    /// <summary>
    /// 注册响应事件
    /// </summary>
    public override void RegisterAttationEvents()
    {
        //注册进入游戏界面响应事件
        RegisterAttationEvent(Consts.E_EnterGameView);
        //注册离开游戏界面响应事件
        RegisterAttationEvent(Consts.E_LeaveGameView);
        //注册添加分数完成响应事件
        RegisterAttationEvent(Consts.E_AddCurrentScoreFinished);
        //注册方块移动结束响应事件
        RegisterAttationEvent(Consts.E_ShapeMoveFinished);
        //注册方块放置进地图完成响应事件
        RegisterAttationEvent(Consts.E_ShapePlaceFinished);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        //响应进入游戏界面事件
        //datas[0] 开始游戏标志——true：开始新游戏 false:继续游戏
        if (eventName.Equals(Consts.E_EnterGameView))
        {
            //请求更新数据模型
            m_GameDataModel = GetModel(Consts.M_GameData) as GameDataModel;
            m_MapModel = GetModel(Consts.M_Map) as MapModel;
            if (m_GameDataModel.IsPlaying == false || (bool)datas[0])
            {
                if (_currentShape != null)
                {
                    Destroy(_currentShape.gameObject);
                    _currentShape = null;
                }
                SendEvent(Consts.E_GameBegin);
            }
            EnterView();
        }

        //响应离开游戏界面事件
        else if (eventName.Equals(Consts.E_LeaveGameView))
        {
            LeaveView();
        }

        //响应添加分数完成事件
        else if (eventName.Equals(Consts.E_AddCurrentScoreFinished))
        {
            //更新UI显示数据
            txt_CurrentScore.text = m_GameDataModel.CurrentScore.ToString();
            txt_HightestScore.text = m_GameDataModel.HightestScore.ToString();
        }

        //响应方块移动结束事件
        //datas[0] 移动成功标志——true：移动成功 false:移动失败
        //datas[1] 移动方向——下、左、右、旋转
        else if (eventName.Equals(Consts.E_ShapeMoveFinished))
        {
            switch ((int)datas[1])
            {
                case 0:
                    //向下移动失败
                    if ((bool)datas[0] == false)
                    {
                        //向上回退一格
                        _currentShape.Up();
                        //放置方块进地图
                        SendEvent(Consts.E_ShapePlaceStart, _currentShape.transform);
                    }
                    //向下移动成功，播放音乐
                    else
                        AudioManager.Instance.PlayUIMusic(Consts.A_ShapDrop);
                    break;
                case 1:
                    //向左移动失败
                    if ((bool)datas[0] == false)
                    {
                        //向右回退一格
                        _currentShape.Right();
                    }
                    //向左移动成功，播放音乐
                    else
                        AudioManager.Instance.PlayUIMusic(Consts.A_Balloon);
                    break;
                case 2:
                    //向右移动失败
                    if ((bool)datas[0] == false)
                    {
                        //向左回退一格
                        _currentShape.Left();
                    }
                    //向右移动成功，播放音乐
                    else
                        AudioManager.Instance.PlayUIMusic(Consts.A_Balloon);
                    break;
                case 3:
                    //顺时针旋转90度失败
                    if ((bool)datas[0] == false)
                    {
                        //逆时针旋转90度
                        _currentShape.AnticlockwiseRotation();
                    }
                    //顺时针旋转90度成功，播放音乐
                    else
                        AudioManager.Instance.PlayUIMusic(Consts.A_Balloon);
                    break;
            }
        
        }

        //响应方块放置进地图完成事件
        else if (eventName.Equals(Consts.E_ShapePlaceFinished))
        {
            //方块放置成功，获得可消除行数，请求增加得分。
            SendEvent(Consts.E_AddCurrentScoreStart, (int)datas[0]*Consts.ClearLineScore);
            //当前方块置为空
            _currentShape = null;
            
        }
    }

    /// <summary>
    /// 进入界面方法
    /// </summary>
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

    /// <summary>
    /// 离开界面方法
    /// </summary>
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

    /// <summary>
    /// 暂停按钮点击事件
    /// </summary>
    public void OnPauseGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_PauseGame);
        SendEvent(Consts.E_EnterMenuView,true);
    }

    /// <summary>
    /// 生成方块方法
    /// </summary>
    private void SpawnShape()
    {
        int index = Random.Range(0, shapes.Length);
        int indexColor = Random.Range(0, colors.Length);
        _currentShape = Instantiate(shapes[index], GameObject.Find("Map").transform);
        _currentShape.InitShape(colors[indexColor]);
    }

    /// <summary>
    /// 游戏运行逻辑
    /// </summary>
    public void Update()
    {

        //游戏开发并且未暂停时
        if(m_GameDataModel.IsPlaying&&!m_MapModel.Pause)
        {
            //当前方块为空，生成一个新的方块
            if(_currentShape==null)
            {
                SpawnShape();
            }
            else
            {
                //计时器计时
                _timer += Time.deltaTime;
                //超过方块下落事件，方块下落
                if (_timer > m_MapModel.FallTime)
                {
                    _timer = 0;
                    _currentShape.Donw();
                    //检验下落是否成功
                    SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform,0);
                }
            }
            //左箭头方块左移
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _currentShape.Left();
                //检验下落是否成功
                SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform, 1);
            }
            //右箭头方块右移
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _currentShape.Right();
                //检验下落是否成功
                SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform, 2);
            }
            //上箭头方块顺时针旋转
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _currentShape.ClockwiseRotation();
                //检验下落是否成功
                SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform, 3);
            }
            //下箭头方块加速下落
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //改变方块下落间隔
                SendEvent(Consts.E_ChangShaepDownSpeed);
            }
        }
    }

    
}