﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDFramework.DataListener
{

    abstract public class ADataListener
    {
        public class CallBackCache
        {
            public Action<object> CallBack;
            public object Param;
        }

        /// <summary>
        /// 所有的数据
        /// </summary>
        protected Dictionary<string, object> dataMap;

        //注册数据变动事件刷新
        protected Dictionary<string, List<Action<object>>> callbackMap;

        /// <summary>
        /// 一次性监听的回调
        /// </summary>
        protected Dictionary<string, List<Action<object>>> onceCallbackMap;

        /// <summary>
        /// 注册事件缓存
        /// </summary>
        protected Dictionary<string, List<object>> valueCacheMap;

        public ADataListener()
        {
            dataMap = new Dictionary<string, object>();
            callbackMap = new Dictionary<string, List<Action<object>>>();
            valueCacheMap = new Dictionary<string, List<object>>();
            onceCallbackMap = new Dictionary<string, List<Action<object>>>();
        }

        virtual public void InitData()
        {
        }

        /// <summary>
        /// 注册数据
        /// </summary>
        /// <param name="name"></param>
        virtual public void AddData(string name)
        {
            if (!dataMap.ContainsKey(name))
            {
                dataMap[name] = null;
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        virtual public void SetData(string name, object value, bool isTriggerCallback = true)
        {
            if (dataMap.ContainsKey(name))
            {
                dataMap[name] = value;
            }
            else
            {
                BDebug.LogError("设置无效,无该数据:" + name);
                return;
            }

            //调用数据改变
            if (isTriggerCallback)
            {
                //all
                List<Action<object>> actions = null;

                //onece
                if (onceCallbackMap.TryGetValue(name, out actions))
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        var a = actions[i];
                        onceCallbackMap[name].Remove(a);
                        a(value);
                    }
                   
                }

                if (callbackMap.TryGetValue(name, out actions))
                {
                    for (int i = actions.Count - 1; i >= 0; i--)
                    {
                        var a = actions[i];
                        a(value);
                    }
                }
                else
                {
                    List<object> list = null;
                    valueCacheMap.TryGetValue(name, out list);
                    if (list == null)
                    {
                        list = new List<object>();
                        list.Add(value);
                        valueCacheMap[name] = list;
                    }
                    else
                    {
                        list.Add(value);
                    }
                }

                //
            }
        }


        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isUseCallback"></param>
        virtual public void TriggerEvent(string name, object value = null, bool isUseCallback = true)
        {
            SetData(name, value, isUseCallback);
        }

        /// <summary>
        /// 获取玩家数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        virtual public T GetData<T>(string name)
        {
            T t = default(T);
            if (dataMap.ContainsKey(name))
            {
                var _value = dataMap[name];
                if (_value == null)
                {
                    t = default(T);
                    dataMap[name] = t;
                }
                else
                {
                    t = (T) _value;
                }
            }
            else
            {
                dataMap[name] = t;
            }

            return t;
        }


        /// <summary>
        /// 属性变动事件注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        virtual public void AddListener(string name, Action<object> callback = null, bool isTriggerCacheData = false)
        {
            if (dataMap.ContainsKey(name) == false)
            {
                BDebug.LogError("暂时无数据,提前监听:" + name);
            }

            //
            List<Action<object>> actions = null;

            if (callbackMap.TryGetValue(name, out actions))
            {
                actions.Add(callback);
            }
            else
            {
                actions = new List<Action<object>>();
                actions.Add(callback);
                callbackMap[name] = actions;
            }

            if (isTriggerCacheData)
            {
                List<object> list = null;
                this.valueCacheMap.TryGetValue(name, out list);
                if (list != null)
                {
                    foreach (var value in list)
                    {
                        callback(value);
                    }

                    //置空
                    this.valueCacheMap[name] = new List<object>();
                }
            }
        }


        /// <summary>
        /// 属性变动事件注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        virtual public void AddListenerOnce(string name, Action<object> callback = null,
            bool isTriggerCacheData = false)
        {
            if (dataMap.ContainsKey(name) == false)
            {
                BDebug.LogError("无数据,提前监听:" + name);
            }

            //
            List<Action<object>> actions = null;

            if (onceCallbackMap.TryGetValue(name, out actions))
            {
                actions.Add(callback);
            }
            else
            {
                actions = new List<Action<object>>();
                actions.Add(callback);
                onceCallbackMap[name] = actions;
            }

            if (isTriggerCacheData)
            {
                List<object> list = null;
                this.valueCacheMap.TryGetValue(name, out list);
                if (list != null)
                {
                    foreach (var value in list)
                    {
                        callback(value);
                    }

                    //置空
                    this.valueCacheMap[name] = new List<object>();
                }
            }
        }


        /// <summary>
        /// 移除属性变动事件注册
        /// </summary>
        /// <param name="name"></param>
        virtual public void ClearListener(string name)
        {
            if (callbackMap.ContainsKey(name))
            {
                callbackMap.Remove(name);
            }
        }


        /// <summary>
        /// 移除属性变动事件注册
        /// </summary>
        /// <param name="name"></param>
        virtual public void RemoveListener(string name, Action<object> callback)
        {
            List<Action<object>> actions = null;
            if (callbackMap.TryGetValue(name, out actions))
            {
                actions.Remove(callback);
            }
        }


        /// <summary>
        /// 移除属性变动事件注册
        /// </summary>
        /// <param name="name"></param>
        virtual public void RemoveListener(string name)
        {
            callbackMap.Remove(name);
        }


        /// <summary>
        /// 获取所有的name
        /// </summary>
        /// <returns></returns>
        public List<string> GetDataNames()
        {
            return this.dataMap.Keys.ToList();
        }


        /// <summary>
        /// 是否含有某个值
        /// </summary>
        /// <returns></returns>
        public bool ContainsKey(string name)
        {
            return this.dataMap.ContainsKey(name);
        }

    }
}