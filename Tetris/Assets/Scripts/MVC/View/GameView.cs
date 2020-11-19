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
    public RectTransform CurrentScore;
    public RectTransform PauseGameButton;
    public RectTransform HightestScore;

    public Text txt_CurrentScore;
    public Text txt_HightestScore;

    public Shape[] shapes;
    public Color[] colors;

    private GameDataModel m_GameDataModel;
    private MapModel m_MapModel;

    private Shape _currentShape;
    private float _timer = 0;
    public override string Name
    {
        get=> Consts.V_Game;
    }

    public override void RegisterAttationEvents()
    {
        RegisterAttationEvent(Consts.E_EnterGameView);
        RegisterAttationEvent(Consts.E_LeaveGameView);
        RegisterAttationEvent(Consts.E_AddCurrentScoreFinished);
        RegisterAttationEvent(Consts.E_ShapeMoveFinished);
        RegisterAttationEvent(Consts.E_ShapePlaceFinished);
    }

    public override void HandleEvent(string eventName, params object[] datas)
    {
        if (eventName.Equals(Consts.E_EnterGameView))
        {
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
        else if (eventName.Equals(Consts.E_LeaveGameView))
        {
            LeaveView();
        }
        else if (eventName.Equals(Consts.E_AddCurrentScoreFinished))
        {
            UpdateScore();
        }
        else if (eventName.Equals(Consts.E_ShapeMoveFinished))
        {
            if ((bool)datas[0] == false)
            {
                switch ((int)datas[1])
                {
                    case 0:
                        _currentShape.Up();
                        SendEvent(Consts.E_ShapePlaceStart, _currentShape.transform);
                        break;
                    case 1:
                        _currentShape.Right();
                        break;
                    case 2:
                        _currentShape.Left();
                        break;
                    case 3:
                        _currentShape.AnticlockwiseRotation();
                        break;
                }

            }
        }
        else if (eventName.Equals(Consts.E_ShapePlaceFinished))
        {
            SendEvent(Consts.E_AddCurrentScoreStart, (int)datas[0]*Consts.ClearLineScore);
            _currentShape = null;
            
        }
    }

    public void EnterView()
    {
        UpdateScore();
        gameObject.SetActive(true);
        CurrentScore.DOAnchorPosY(-CurrentScore.sizeDelta.y/2, 0.5f);
        PauseGameButton.DOAnchorPosY(-CurrentScore.sizeDelta.y / 2, 0.5f);
        HightestScore.DOAnchorPosY(-HightestScore.sizeDelta.y / 2, 0.5f);
        Camera.main.DOOrthoSize(m_GameDataModel.MinCameraSize, 0.5f);
    }

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

    public void UpdateScore()
    {
        txt_CurrentScore.text = m_GameDataModel.CurrentScore.ToString();
        txt_HightestScore.text = m_GameDataModel.HightestScore.ToString();
    }

    public void OnPauseGameButtonDown()
    {
        AudioManager.Instance.PlayUIMusic(Consts.A_Cursor);
        LeaveView();
        SendEvent(Consts.E_PauseGame);
        SendEvent(Consts.E_EnterMenuView,true);
    }

    public void Update()
    {
        if(m_GameDataModel.IsPlaying&&!m_MapModel.Pause)
        {
            if(_currentShape==null)
            {
                SpawnShape();
            }
            else
            {
                _timer += Time.deltaTime;
                if (_timer > m_MapModel.FallTime)
                {
                    _timer = 0;
                    _currentShape.Donw();
                    SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform,0);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _currentShape.Left();
                SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform, 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _currentShape.Right();
                SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform, 2);
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                _currentShape.ClockwiseRotation();
                SendEvent(Consts.E_ShapeMoveStart, _currentShape.transform, 3);
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                SendEvent(Consts.E_ChangShaepDownSpeed,1/Consts.ShapeFallDownSpeed);
            }
        }
    }

    private void SpawnShape()
    {
        int index = Random.Range(0, shapes.Length);
        int indexColor= Random.Range(0, colors.Length);
        _currentShape = Instantiate(shapes[index], GameObject.Find("Map").transform);
        _currentShape.InitShape(colors[indexColor]);
    }
}