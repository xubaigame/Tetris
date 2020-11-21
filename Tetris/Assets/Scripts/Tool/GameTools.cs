/****************************************************
    文件：GameTools.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/19 0:48:44
	功能：游戏工具类
*****************************************************/

using UnityEngine;

public class GameTools : MonoBehaviour 
{
    public static GameTools Instance = null;

    public void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 删除场景中的游戏物体
    /// </summary>
    /// <param name="go">游戏物体</param>
    public void DestroyGameObject(GameObject go)
    {
        Destroy(go);
    }
}