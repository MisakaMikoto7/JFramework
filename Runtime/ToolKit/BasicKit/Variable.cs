// *********************************************************************************
// # Project: JFramework
// # Unity: 2022.3.5f1c1
// # Author: Charlotte
// # Version: 1.0.0
// # History: 2023-12-21  20:55
// # Copyright: 2023, Charlotte
// # Description: This is an automatically generated comment.
// *********************************************************************************

using System;
using JFramework.Interface;

namespace JFramework
{
    /// <summary>
    /// 属性数值类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Variable<T> : IVariable where T : struct
    {
        /// <summary>
        /// 泛型数值
        /// </summary>
        public T value;

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        public Variable()
        {
            value = new T();
        }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="value"></param>
        public Variable(T value)
        {
            this.value = value;
        }
    }
}