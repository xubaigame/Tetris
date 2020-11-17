/****************************************************
    文件：ChangeMuteController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/18 0:25:41
	功能：改变经营状态Controller类
*****************************************************/

using UnityEngine;

public class ChangeMuteController : BaseController
{
    public override void Execute(params object[] datas)
    {
        GameDataModel gameDataModel = GetModel(Consts.M_GameData) as GameDataModel;

        gameDataModel.ChangeMuteState();
    }
}