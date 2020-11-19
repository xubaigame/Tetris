/****************************************************
    文件：ChangeSpeedController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/19 23:50:0
	功能：改变图形下落速度Controller类
*****************************************************/

using UnityEngine;

public class ChangeSpeedController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_Map) as MapModel).ChangeSpeed((float)datas[0]);
    }
}