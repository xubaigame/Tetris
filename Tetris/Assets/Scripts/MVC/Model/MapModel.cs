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
    public override string Name
    {
        get => Consts.M_Map;
    }
    

    public const int NORMAL_ROWS = 20;
    public const int MAX_ROWS = 23;
    public const int MAX_COLUMNS = 10;

    private float _fallTime = 0.8f;

    private bool _pause = false;
    public bool Pause { get => _pause; }
    public float FallTime { get => _fallTime;}

    private Transform[,] _mapTransform = new Transform[MAX_COLUMNS, MAX_ROWS];

    public void NewGame()
    {
        ClearMap();
        _pause = false;
    }

    public void GameEnd()
    {
        ClearMap();

        _pause = false;
    }
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
    public void PauseGame()
    {
        _pause = true;
    }

    public void ResumeGame()
    {
        _pause = false;
    }

    public void ChangeSpeed(float speed)
    {
        _fallTime = _fallTime*speed;
    }
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

    private bool IsMapPosition(Vector2 position)
    {
        return position.x >= 0 && position.x < MAX_COLUMNS && position.y >= 0;
    }

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
        ChangeSpeed(Consts.ShapeFallDownSpeed);

    }

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
            }
        }
        return count;
    }

    private bool CheckIsRowFull(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (_mapTransform[i, row] == null)
                return false;
        }
        return true;
    }

    private void DeleteRow(int row)
    {
        for(int i=0;i<MAX_COLUMNS;i++)
        {
            GameTools.Instance.DestroyGameObject(_mapTransform[i, row].gameObject);
            _mapTransform[i, row] = null;
        }
    }

    private void MoveDownRowsAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);
        }
    }

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
}