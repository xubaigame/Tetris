/****************************************************
    文件：BaseController.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/14 23:44:12
	功能：Controller层基类
*****************************************************/

using System;
using UnityEngine;

public abstract class BaseController 
{
    /// <summary>
    /// 根据类型获得模型
    /// </summary>
    /// <typeparam name="T">模型类型</typeparam>
    /// <returns>模型对象</returns>
    protected BaseModel GetModel<T>() where T : BaseModel
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
    /// 根据类型获得视图
    /// </summary>
    /// <typeparam name="T">视图类型</typeparam>
    /// <returns>视图对象</returns>
    protected BaseView GetView<T>() where T : BaseView
    {
        return MVCSystem.GetView<T>();
    }

    /// <summary>
    /// 根据名称获得视图
    /// </summary>
    /// <param name="modelName">视图名称</param>
    /// <returns>视图对象</returns>
    protected BaseView GetView(string viewName)
    {
        return MVCSystem.GetView(viewName);
    }

    /// <summary>
    /// 注册模型
    /// </summary>
    /// <param name="model">模型对象</param>
    protected void RegisterModel(BaseModel model)
    {
        MVCSystem.RegisterModel(model);
    }

    /// <summary>
    /// 注册视图
    /// </summary>
    /// <param name="view">视图对象</param>
    protected void RegisterView(BaseView view)
    {
        MVCSystem.RegisterView(view);
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="controllerType">响应的Controller的Type对象</param>
    protected void RegisterController(string eventName,Type controllerType)
    {
        MVCSystem.RegisterController(eventName, controllerType);
    }

    /// <summary>
    /// 移除模型
    /// </summary>
    /// <param name="modelName">模型名称</param>
    public void RemoveModel(string modelName)
    {
        MVCSystem.RemoveModel(modelName);
    }

    /// <summary>
    /// 移除视图
    /// </summary>
    /// <param name="viewName">视图名称</param>
    public void RemoveView(string viewName)
    {
        MVCSystem.RemoveView(viewName);
    }

    /// <summary>
    /// 移除监听事件
    /// </summary>
    /// <param name="eventName">监听事件名称</param>
    public void RemoveController(string eventName)
    {
        MVCSystem.RemoveController(eventName);
    }

    /// <summary>
    /// 响应事件
    /// </summary>
    /// <param name="datas">数据</param>
    public abstract void Execute(params object[] datas);


}