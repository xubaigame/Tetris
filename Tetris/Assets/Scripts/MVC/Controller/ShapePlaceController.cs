/****************************************************
    文件：ShapePlaceController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/19 0:20:41
	功能：图形添加地图Controller类
*****************************************************/

using UnityEngine;

public class ShapePlaceController : BaseController
{
    public override void Execute(params object[] datas)
    {
        (GetModel(Consts.M_Map) as MapModel).ShapePlace((Transform)datas[0]);
    }
}