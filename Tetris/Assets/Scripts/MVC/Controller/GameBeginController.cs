/****************************************************
    文件：GameBeginController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/18 19:40:59
	功能：Nothing
*****************************************************/

using UnityEngine;

public class GameBeginController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_GameData) as GameDataModel).NewGame();
    }
}