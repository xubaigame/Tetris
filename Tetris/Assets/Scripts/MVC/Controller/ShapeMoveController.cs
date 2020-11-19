/****************************************************
    文件：ShapeMoveController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/19 23:43:3
	功能：图形运动（下降、左右移动、旋转）Controller类
*****************************************************/

using UnityEngine;

public class ShapeMoveController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_Map) as MapModel).IsValidMapPosition((Transform)datas[0], datas[1]);
    }
}