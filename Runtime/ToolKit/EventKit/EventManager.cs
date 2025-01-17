// *********************************************************************************
// # Project: JFramework
// # Unity: 2022.3.5f1c1
// # Author: Charlotte
// # Version: 1.0.0
// # History: 2023-10-24  23:58
// # Copyright: 2023, Charlotte
// # Description: This is an automatically generated comment.
// *********************************************************************************

using System;
using System.Collections.Generic;
using JFramework.Interface;
using Sirenix.OdinInspector;

namespace JFramework
{
    public sealed partial class GlobalManager
    {
        /// <summary>
        /// 事件管理器
        /// </summary>
        public sealed class EventManager : Controller
        {
            /// <summary>
            /// 事件观察字典
            /// </summary>
            [ShowInInspector] private readonly Dictionary<Type, HashSet<IEvent>> observers = new Dictionary<Type, HashSet<IEvent>>();

            /// <summary>
            /// 事件管理器侦听事件
            /// </summary>
            /// <param name="event">传入观察的游戏对象</param>
            /// <typeparam name="T">事件类型</typeparam>
            /// <returns>返回是否能被侦听</returns>
            public void Listen<T>(IEvent<T> @event) where T : struct, IEvent
            {
                if (!Runtime) return;
                if (!observers.ContainsKey(typeof(T)))
                {
                    observers.Add(typeof(T), Event<T>.events = new HashSet<IEvent>());
                }

                Event<T>.Listen(@event);
            }

            /// <summary>
            /// 事件管理器移除事件
            /// </summary>
            /// <param name="event">传入观察的游戏对象</param>
            /// <typeparam name="T">事件类型</typeparam>
            /// <returns>返回是否能被移除</returns>
            public void Remove<T>(IEvent<T> @event) where T : struct, IEvent
            {
                if (!Runtime) return;
                Event<T>.Remove(@event);
            }

            /// <summary>
            /// 事件管理器广播事件
            /// </summary>
            /// <param name="event">传入观察事件数据</param>
            /// <typeparam name="T">事件类型</typeparam>
            public void Invoke<T>(T @event = default) where T : struct, IEvent
            {
                if (!Runtime) return;
                Event<T>.Invoke(@event);
            }

            /// <summary>
            /// 事件管理器销毁
            /// </summary>
            private void OnDestroy()
            {
                foreach (var observer in observers.Values)
                {
                    observer.Clear();
                }

                observers.Clear();
            }
        }
    }
}