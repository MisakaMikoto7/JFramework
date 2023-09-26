using System.Collections.Generic;
using JFramework.Interface;
using UnityEngine;

// ReSharper disable All

namespace JFramework.Core
{
    /// <summary>
    /// 输入管理器
    /// </summary>
    public static class InputManager
    {
        /// <summary>
        /// 输入管理字典
        /// </summary>
        internal static readonly Dictionary<KeyCode, IInput> inputs = new Dictionary<KeyCode, IInput>();

        /// <summary>
        /// 是否活跃
        /// </summary>
        public static bool isActive;

        /// <summary>
        /// 注册
        /// </summary>
        internal static void Register()
        {
            isActive = true;
            GlobalManager.OnUpdate += OnUpdate;
        }

        /// <summary>
        /// 更新输入
        /// </summary>
        private static void OnUpdate()
        {
            if (!GlobalManager.Runtime || !isActive) return;
            foreach (var (key, input) in inputs)
            {
                if (Input.GetKeyDown(key))
                {
                    input.OnEnter();
                }
                else if (Input.GetKeyUp(key))
                {
                    input.OnExit();
                }
            }
        }

        /// <summary>
        /// 获取输入事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static InputAction Get(KeyCode key)
        {
            if (!inputs.TryGetValue(key, out var input))
            {
                input = new InputAction();
                inputs[key] = input;
            }

            return (InputAction)input;
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        internal static void UnRegister()
        {
            foreach (var key in inputs.Keys)
            {
                if (inputs.TryGetValue(key, out var input))
                {
                    ((InputAction)input).Clear();
                }
            }

            inputs.Clear();
            isActive = false;
        }
    }
}