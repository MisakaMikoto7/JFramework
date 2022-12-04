using System;
using System.Collections.Generic;
using JFramework.Async;
using UnityEngine;

namespace JFramework.Basic
{
    public class BaseStateMachine : MonoBehaviour
    {
        protected Dictionary<string, IState> stateDict = new Dictionary<string, IState>();
        protected IState state;

        protected virtual void Awake()
        {
            if (Debugger.LogLevel == LogLevel.Middle)
            {
                Debugger.Log(gameObject.name + "添加OnUpdate监听");
            }

            MonoManager.Instance.AddListener(OnUpdate);
        }

        [Obsolete("[JFramework] 请使用OnUpdate来代替Update管理生命周期", true)]
        protected void Update()
        {
        }

        protected virtual void OnDestroy()
        {
            if (Debugger.LogLevel == LogLevel.Middle)
            {
                Debugger.Log(gameObject.name + "移除OnUpdate监听");
            }

            MonoManager.Instance.RemoveListener(OnUpdate);
        }

        protected virtual void OnUpdate() => state.OnUpdate();

        public void ChangeState(string type)
        {
            state?.OnExit();
            state = stateDict[type];
            state.OnEnter();
        }

        public async void ChangeState(string type, float time)
        {
            await new WaitForSeconds(time);
            if (this == null) return;
            ChangeState(type);
        }
    }
}