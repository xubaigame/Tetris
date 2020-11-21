/****************************************************
    文件：Shape.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/18 21:36:36
	功能：图形类
*****************************************************/

using UnityEngine;

public class Shape : MonoBehaviour 
{
    //方块位置
    public int minPositionX;
    public int maxPositionx;

    //旋转锚点
    private Transform pivot;

    /// <summary>
    /// 初始化方块
    /// </summary>
    /// <param name="color">颜色</param>
    public void InitShape(Color color)
    {
        pivot = transform.Find("Pivot");
        foreach (Transform t in transform)
        {
            if (t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }

        int x = Random.Range(minPositionX, maxPositionx + 1);
        transform.position = new Vector3(x, 20, 0);
    }

    /// <summary>
    /// 向下移动一格
    /// </summary>
    public void Donw()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
    }

    /// <summary>
    /// 向上移动一格
    /// </summary>
    public void Up()
    {
        Vector3 pos = transform.position;
        pos.y += 1;
        transform.position = pos;
    }

    /// <summary>
    /// 向左移动一格
    /// </summary>
    public void Left()
    {
        Vector3 pos = transform.position;
        pos.x -= 1;
        transform.position = pos;
    }

    /// <summary>
    /// 向右移动一格
    /// </summary>
    public void Right()
    {
        Vector3 pos = transform.position;
        pos.x += 1;
        transform.position = pos;
    }

    /// <summary>
    /// 围绕锚点顺时针旋转90度
    /// </summary>
    public void ClockwiseRotation()
    {
        transform.RotateAround(pivot.position,Vector3.forward,90);
    }

    /// <summary>
    /// 围绕锚点逆时针旋转90度
    /// </summary>
    public void AnticlockwiseRotation()
    {
        transform.RotateAround(pivot.position, Vector3.forward, -90);

    }



}