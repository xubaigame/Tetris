/****************************************************
    文件：AddCurrentScoreController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/20 0:11:4
	功能：增加分数Controller类
*****************************************************/

using UnityEngine;

public class AddCurrentScoreController : BaseController
{
    public override void Execute(params object[] datas)
    {
       (GetModel(Consts.M_GameData) as GameDataModel).AddCurrentScore((int)datas[0]);
    }
}