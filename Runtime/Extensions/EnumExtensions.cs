// *********************************************************************************
// # Project: JFramework
// # Unity: 2022.3.5f1c1
// # Author: Charlotte
// # Version: 1.0.0
// # History: 2023-10-24  23:40
// # Copyright: 2023, Charlotte
// # Description: This is an automatically generated comment.
// *********************************************************************************

using System;

namespace JFramework
{
    /// <summary>
    /// 枚举拓展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 将当前枚举切换到下一枚举
        /// 若当前为最后一个，则循环至第一个
        /// </summary>
        /// <param name="current">当前枚举的拓展</param>
        /// <typeparam name="T">传入任何枚举类型</typeparam>
        /// <returns>返回当前枚举的下一个值</returns>
        public static T ToNext<T>(this T current) where T : Enum
        {
            var enumArray = (T[])Enum.GetValues(typeof(T));
            var currIndex = Array.IndexOf(enumArray, current);
            var nextIndex = (currIndex + 1) % enumArray.Length;
            return enumArray[nextIndex];
        }

        /// <summary>
        /// 将当前枚举切换到上一枚举
        /// 若当前为第一个，则循环至最后一个
        /// </summary>
        /// <param name="current">当前枚举的拓展</param>
        /// <typeparam name="T">传入任何枚举类型</typeparam>
        /// <returns>返回当前枚举的上一个值</returns>
        public static T ToLast<T>(this T current) where T : Enum
        {
            var enumArray = (T[])Enum.GetValues(typeof(T));
            var currIndex = Array.IndexOf(enumArray, current);
            var lastIndex = (currIndex - 1 + enumArray.Length) % enumArray.Length;
            return enumArray[lastIndex];
        }
    }
}