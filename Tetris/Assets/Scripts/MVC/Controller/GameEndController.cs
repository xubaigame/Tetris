/****************************************************
    文件：GameEndController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/18 20:45:16
	功能：游戏结束Controller类
*****************************************************/

using UnityEngine;

public class GameEndController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_GameData) as GameDataModel).GameEnd();
        (GetModel(Consts.M_Map) as MapModel).GameEnd((bool)datas[0]);
    }
}