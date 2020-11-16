/****************************************************
    文件：MVCSystem.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/14 23:50:31
	功能：MVC核心控制类
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public static class MVCSystem 
{
    
    public static Dictionary<string, BaseModel> _models = new Dictionary<string, BaseModel>();
    public static Dictionary<string, BaseView> _views = new Dictionary<string, BaseView>();
    public static Dictionary<string, Type> _commandMap = new Dictionary<string, Type>();

    /// <summary>
    /// 注册模型
    /// </summary>
    /// <param name="model">要注册的模型对象</param>
    public static void RegisterModel(BaseModel model)
    {
        if(_models.ContainsKey(model.Name))
        {
            Debug.Log("MVCSystem Error: 名称为"+model.Name+"的模型已经添加，请勿重复添加！");
            return;
        }
        _models[model.Name] = model;
    }

    /// <summary>
    /// 注册视图
    /// </summary>
    /// <param name="view">要注册的视图对象</param>
    public static void RegisterView(BaseView view)
    {
        if (_views.ContainsKey(view.Name))
        {
            Debug.Log("MVCSystem Error: 名称为" + view.Name + "的视图已经添加，请勿重复添加！");
            return;
        }
        _views[view.Name] = view;
        view.RegisterAttationEvents();
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="controllerType">响应事件的Controller的Type对象</param>
    public static void RegisterController(string eventName,Type controllerType)
    {
        if (_commandMap.ContainsKey(eventName))
        {
            Debug.Log("MVCSystem Error: 名称为" + eventName + "的事件已经添加，请勿重复添加！");
            return;
        }
        _commandMap[eventName] = controllerType;
    }

    /// <summary>
    /// 移除模型
    /// </summary>
    /// <param name="modelName">模型名称</param>
    public static void RemoveModel(string modelName)
    {
        if (!_models.ContainsKey(modelName))
        {
            Debug.Log("MVCSystem Error: 名称为" + modelName + "的模型不存在，删除失败！");
            return;
        }
        _models.Remove(modelName);
    }

    /// <summary>
    /// 移除视图
    /// </summary>
    /// <param name="viewName">视图名称</param>
    public static void RemoveView(string viewName)
    {
        if (!_views.ContainsKey(viewName))
        {
            Debug.Log("MVCSystem Error: 名称为" + viewName + "的视图不存在，删除失败！");
            return;
        }
        _views.Remove(viewName);
    }

    /// <summary>
    /// 移除监听事件
    /// </summary>
    /// <param name="eventName">监听事件名称</param>
    public static void RemoveController(string eventName)
    {
        if (!_commandMap.ContainsKey(eventName))
        {
            Debug.Log("MVCSystem Error: 名称为" + eventName + "的响应事件不存在，删除失败！");
            return;
        }
        _commandMap.Remove(eventName);
    }

    /// <summary>
    /// 根据类型获得模型
    /// </summary>
    /// <typeparam name="T">模型类型</typeparam>
    /// <returns></returns>
    public static BaseModel GetModel<T>() where T :BaseModel
    {
        foreach(BaseModel m in _models.Values)
        {
            if (m is T)
                return m;
        }

        return null;
    }

    /// <summary>
    /// 根据名字获得模型
    /// </summary>
    /// <param name="modelName">模型名字</param>
    /// <returns></returns>
    public static BaseModel GetModel(string modelName)
    {
        foreach (BaseModel m in _models.Values)
        {
            if (m.Name.Equals(modelName))
                return m;
        }

        return null;
    }

    /// <summary>
    /// 根据类型获得视图
    /// </summary>
    /// <typeparam name="T">视图类型</typeparam>
    /// <returns></returns>
    public static BaseView GetView<T>() where T : BaseView
    {
        foreach (BaseView v in _views.Values)
        {
            if (v is T)
                return v;
        }

        return null;
    }

    /// <summary>
    /// 根据名字获得视图
    /// </summary>
    /// <param name="viewName">视图名字</param>
    /// <returns></returns>
    public static BaseView GetView(string viewName)
    {
        foreach (BaseView v in _views.Values)
        {
            if (v.Name.Equals(viewName))
                return v;
        }

        return null;
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="data">数据</param>
    public static void SendEvent(string eventName,params object[] data)
    {
        //Controller尝试响应事件
        if (_commandMap.ContainsKey(eventName))
        {
            Type t = _commandMap[eventName];
            BaseController controller = Activator.CreateInstance(t) as BaseController;
            controller.Execute(data);
        }

        //View尝试响应事件
        foreach (BaseView v in _views.Values)
        {
            if (v.IsEventContains(eventName))
            {
                v.HandleEvent(eventName, data);
            }
        }

    }
}