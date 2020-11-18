/****************************************************
    文件：ShapFallDownController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/19 0:6:13
	功能：图形下落Controller类
*****************************************************/

using UnityEngine;

public class ShapFallDownController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_Map) as MapModel).IsValidMapPosition((Transform)datas[0]);
    }
}