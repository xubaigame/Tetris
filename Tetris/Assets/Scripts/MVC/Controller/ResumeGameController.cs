/****************************************************
    文件：ResumeGameController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/19 0:35:15
	功能：恢复游戏Controller类
*****************************************************/

using UnityEngine;

public class ResumeGameController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_Map) as MapModel).ResumeGame();
    }
}