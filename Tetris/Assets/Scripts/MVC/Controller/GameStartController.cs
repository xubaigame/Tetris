/****************************************************
    文件：GameStartController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/16 20:15:55
	功能：游戏开始事件控制器
*****************************************************/

using UnityEngine;

public class GameStartController : BaseController
{
    public override void Execute(params object[] datas)
    {
        //注册model
        RegisterModel(new GameDataModel());

        //注册View
        RegisterView(datas[0] as BaseView);
        RegisterView(datas[1] as BaseView);
        RegisterView(datas[2] as BaseView);
        RegisterView(datas[3] as BaseView);
        RegisterView(datas[4] as BaseView);
        //注册Controller
        RegisterController(Consts.E_ChangeMuteStart, typeof(ChangeMuteController));
        RegisterController(Consts.E_SaveData, typeof(SaveDataController));
        RegisterController(Consts.E_GameBegin, typeof(GameBeginController));
        RegisterController(Consts.E_GameEnd, typeof(GameEndController));
        RegisterController(Consts.E_ClearDataStart, typeof(ClearDataController));
        //调用模型方法
        (GetModel(Consts.M_GameData) as GameDataModel).InitData();
    }


}