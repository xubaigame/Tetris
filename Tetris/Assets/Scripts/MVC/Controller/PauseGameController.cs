/****************************************************
    文件：PauseGameController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/19 0:34:38
	功能：暂停游戏Controller类
*****************************************************/

using UnityEngine;

public class PauseGameController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_Map) as MapModel).PauseGame();
    }
}