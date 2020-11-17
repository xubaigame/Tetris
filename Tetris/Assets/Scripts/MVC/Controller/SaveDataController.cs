/****************************************************
    文件：SaveDataController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/18 0:55:5
	功能：保存数据Controller类
*****************************************************/

using UnityEngine;

public class SaveDataController : BaseController
{
    public override void Execute(params object[] datas)
    {
        GameDataModel gameDataModel = GetModel(Consts.M_GameData) as GameDataModel;

        gameDataModel.SaveData();
    }
}