using System;
using UnityEngine;

// ReSharper disable All
namespace JFramework.Core
{
    public static class DailyManager
    {
        public static event Action UpdateEvent;
        /// <summary>
        /// 每日刷新时间
        /// </summary>
        private const int RefreshTime = 5;
        
        /// <summary>
        /// 管理器名称
        /// </summary>
        private static string Name => nameof(DailyManager);

        /// <summary>
        /// 上一次检测时间
        /// </summary>
        private static float lastCheckTime;

        /// <summary>
        /// 当前日期
        /// </summary>
        private static DateTime dateTime => DateTime.Now;

        /// <summary>
        /// 明天的早上5点
        /// </summary>
        private static DateTime tomorrow;

        private static int lastDayOfYear
        {
            get => JsonManager.Load<int>(Name,true);
            set => JsonManager.Save(value, Name,true);
        }

        /// <summary>
        /// 管理器初始化
        /// </summary>
        internal static void Awake()
        {
            GlobalManager.Instance.UpdateEvent += OnUpdate;
            tomorrow = DateTime.Today.Add(new TimeSpan(1, RefreshTime, 0, 0));
        }

        /// <summary>
        /// 管理器更新
        /// </summary>
        private static void OnUpdate()
        {
            if (Time.realtimeSinceStartup - lastCheckTime > 5)
            {
                lastCheckTime = Time.realtimeSinceStartup;
                if (dateTime.DayOfYear != lastDayOfYear)
                {
                    //同一年
                    if (dateTime.DayOfYear > lastDayOfYear)
                    {
                        //超过一天未登陆
                        if (dateTime.DayOfYear - lastDayOfYear >= 2)
                        {
                            lastDayOfYear = dateTime.DayOfYear;
                            UpdateEvent?.Invoke();
                        }
                        //只有一天未登陆
                        else
                        {
                            if (dateTime.Hour >= RefreshTime)
                            {
                                lastDayOfYear = dateTime.DayOfYear;
                                UpdateEvent?.Invoke();
                            }
                        }
                    }
                    //跨年
                    else if (dateTime.DayOfYear < lastDayOfYear)
                    {
                        int lastYear = dateTime.Year - 1;
                        int lastYearTotalDays = lastYear % 4 == 0 ? 366 : 365;
                        //超过一天未登陆
                        if (lastYearTotalDays + dateTime.DayOfYear - lastDayOfYear >= 2)
                        {
                            lastDayOfYear = dateTime.DayOfYear;
                            UpdateEvent?.Invoke();
                        }
                        //一天未登陆并且到达刷新时间
                        else
                        {
                            if (dateTime.Hour >= RefreshTime)
                            {
                                lastDayOfYear = dateTime.DayOfYear;
                                UpdateEvent?.Invoke();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 销毁管理器
        /// </summary>
        internal static void Destroy()
        {
        }
    }
}