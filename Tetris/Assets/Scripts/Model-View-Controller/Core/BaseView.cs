/****************************************************
    文件：BaseView.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/14 23:43:15
	功能：View层基类
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView :MonoBehaviour 
{
    //视图名称
    private string _name;
    public abstract string Name { get; }

    //视图响应事件
    private List<string> _attationEvents = new List<string>();

    /// <summary>
    /// 判断当前视图是否响应某事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <returns>响应状态</returns>
    public bool IsEventContains(string eventName)
    {
        return _attationEvents.Contains(eventName);
    }

    /// <summary>
    /// 响应事件函数
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="datas">数据</param>
    public abstract void HandleEvent(string eventName, params object[] datas);

    /// <summary>
    /// 注册响应事件
    /// </summary>
    public virtual void RegisterAttationEvents() { }

    /// <summary>
    /// 根据类型获得模型
    /// </summary>
    /// <typeparam name="T">模型类型</typeparam>
    /// <returns>模型对象</returns>
    protected BaseModel GetModel<T>() where T:BaseModel
    {
        return MVCSystem.GetModel<T>();
    }

    /// <summary>
    /// 根据名称获得模型
    /// </summary>
    /// <param name="modelName">模型名称</param>
    /// <returns>模型对象</returns>
    protected BaseModel GetModel(string modelName)
    {
        return MVCSystem.GetModel(modelName);
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="data">数据</param>
    protected void SendEvent(string eventName,params object[] data)
    {
        MVCSystem.SendEvent(eventName, data);
    }
    
}