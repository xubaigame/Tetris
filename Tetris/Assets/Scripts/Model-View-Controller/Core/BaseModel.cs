/****************************************************
    文件：BaseModel.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/14 23:40:18
	功能：Model层基类
*****************************************************/

using UnityEngine;

public abstract class BaseModel 
{
    //模型名称
    private string  _name;
    public abstract string Name { get; }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="data">数据</param>
    protected void SendEvent(string eventName,params object[] data)
    {
        MVCSystem.SendEvent(eventName,data);
    }
}