/****************************************************
    文件：MapModel.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/18 22:3:5
	功能：地图数据Model类
*****************************************************/

using UnityEngine;

public class MapModel:BaseModel 
{

    #region 字段
    public const int NORMAL_ROWS = 20;
    public const int MAX_ROWS = 23;
    public const int MAX_COLUMNS = 10;

    private float _fallTime = Consts.ShapeDownSpeed;

    private bool _pause = false;
    #endregion

    #region 属性
    public override string Name
    {
        get => Consts.M_Map;
    }
    public bool Pause { get => _pause; }
    public float FallTime { get => _fallTime; }
    #endregion

    //地图信息
    private Transform[,] _mapTransform = new Transform[MAX_COLUMNS, MAX_ROWS];

    /// <summary>
    /// 开始一局新游戏
    /// </summary>
    public void NewGame()
    {
        ClearMap();
        _pause = false;
    }

    /// <summary>
    /// 游戏结束方法
    /// </summary>
    /// <param name="clearMap">是否清空地图</param>
    public void GameEnd(bool clearMap)
    {
        if(clearMap)
            ClearMap();
        _pause = true;
    }

    /// <summary>
    /// 清空地图
    /// </summary>
    public void ClearMap()
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            for (int j = 0; j < MAX_ROWS; j++)
            {
                if(_mapTransform[i,j]!=null)
                {
                    GameTools.Instance.DestroyGameObject(_mapTransform[i, j].gameObject);
                    _mapTransform[i, j] = null;
                }
            }
        }
    }
    #region 暂停/恢复方法
    //可优化，二合一

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        _pause = true;
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ResumeGame()
    {
        _pause = false;
    }
    #endregion

    /// <summary>
    /// 更改方块下落速度
    /// </summary>
    public void ChangeSpeed()
    {
        if (_fallTime == Consts.ShapeDownSpeed)
            _fallTime = Consts.ShapeDownSpeedX2;
        else if(_fallTime== Consts.ShapeDownSpeedX2)
            _fallTime = Consts.ShapeDownSpeed;
    }

    /// <summary>
    /// 当前移动是否有效
    /// </summary>
    /// <param name="shape">方块位置</param>
    /// <param name="operation">移动方向</param>
    public void IsValidMapPosition(Transform shape,object operation)
    {
        bool result = true;
        foreach (Transform child in shape)
        {
            if (child.tag != "Block") continue;
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);
            Vector2 pos = new Vector2(x, y);
            if (IsMapPosition(pos) == false)
            {
                result = false;
                break;
            }
            if(_mapTransform[(int)pos.x,(int)pos.y]!=null)
            {
                result = false;
                break;
            }
        }
        SendEvent(Consts.E_ShapeMoveFinished, result, operation);
    }

    /// <summary>
    /// 判断当前位置是否处于地图中
    /// </summary>
    /// <param name="position">当前位置</param>
    /// <returns>判断结果</returns>
    private bool IsMapPosition(Vector2 position)
    {
        return position.x >= 0 && position.x < MAX_COLUMNS && position.y >= 0;
    }

    /// <summary>
    /// 将方块添加至地图中
    /// </summary>
    /// <param name="shape">方块位置</param>
    public void ShapePlace(Transform shape)
    {
        foreach (Transform child in shape)
        {
            if (child.tag != "Block") continue;
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);
            _mapTransform[x, y] = child;
        }
        int count = CheckMap();
        SendEvent(Consts.E_ShapePlaceFinished, count);
        _fallTime = Consts.ShapeDownSpeed;
        IsGameover();
    }
    
    /// <summary>
    /// 检查地图是否存在可消除行
    /// </summary>
    /// <returns>可消除行数</returns>
    private int CheckMap()
    {
        int count = 0;
        for (int i = 0; i < MAX_ROWS; i++)
        {
            bool isFull = CheckIsRowFull(i);
            if(isFull)
            {
                count++;
                DeleteRow(i);
                MoveDownRowsAbove(i + 1);
                i--;
                AudioManager.Instance.PlayUIMusic(Consts.A_Lineclear);
            }
        }
        return count;
    }

    /// <summary>
    /// 检查行是否可消除
    /// </summary>
    /// <param name="row">行号</param>
    /// <returns>消除状态</returns>
    private bool CheckIsRowFull(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (_mapTransform[i, row] == null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 消除行
    /// </summary>
    /// <param name="row">行号</param>
    private void DeleteRow(int row)
    {
        for(int i=0;i<MAX_COLUMNS;i++)
        {
            GameTools.Instance.DestroyGameObject(_mapTransform[i, row].gameObject);
            _mapTransform[i, row] = null;
        }
    }

    /// <summary>
    /// 将行号上方所有行向下移动
    /// </summary>
    /// <param name="row">行号</param>
    private void MoveDownRowsAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);
        }
    }

    /// <summary>
    /// 将行号上方一行向下移动
    /// </summary>
    /// <param name="row">行号</param>
    private void MoveDownRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if(_mapTransform[i,row]!=null)
            {
                _mapTransform[i, row - 1] = _mapTransform[i, row];
                _mapTransform[i, row] = null;
                _mapTransform[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    /// <summary>
    /// 检查游戏是否结束
    /// </summary>
    public void IsGameover()
    {
        for(int i=NORMAL_ROWS;i<MAX_ROWS;i++)
        {
            for (int j = 0; j < MAX_COLUMNS; j++)
            { 
                if(_mapTransform[j,i]!=null)
                {
                    AudioManager.Instance.PlayUIMusic(Consts.A_Gameover);
                    SendEvent(Consts.E_GameEnd, false);
                    SendEvent(Consts.E_EnterLoseGameView);
                }
            }
        }
    }
}