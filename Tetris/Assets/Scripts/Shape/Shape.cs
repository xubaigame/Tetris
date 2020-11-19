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
    public int minPositionX;
    public int maxPositionx;

    private Transform pivot;

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

    public void Donw()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
    }

    public void Up()
    {
        Vector3 pos = transform.position;
        pos.y += 1;
        transform.position = pos;
    }

    public void Left()
    {
        Vector3 pos = transform.position;
        pos.x -= 1;
        transform.position = pos;
    }

    public void Right()
    {
        Vector3 pos = transform.position;
        pos.x += 1;
        transform.position = pos;
    }

    public void ClockwiseRotation()
    {
        transform.RotateAround(pivot.position,Vector3.forward,90);
    }

    public void AnticlockwiseRotation()
    {
        transform.RotateAround(pivot.position, Vector3.forward, -90);

    }



}