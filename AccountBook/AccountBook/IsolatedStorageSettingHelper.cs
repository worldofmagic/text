using System;
using System.IO.IsolatedStorage;
namespace AccountBook
{
    /// <summary>
    /// 独立存储设置公共类
    /// </summary>
    public class IsolatedStorageSettingHelper
    {
        /// <summary>
        /// 判断独立存储设置是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否存在</returns>
        public static bool Exist(string key)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key);
        }
        /// <summary>
        /// 获取独立存储设置的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>键的值</returns>
        public static object Load(string key)
        {
            if (Exist(key))
            {
                return IsolatedStorageSettings.ApplicationSettings[key];
            }
            return null;
        }
        /// <summary>
        /// 保存独立存储设置的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="obj">键的值</param>
        public static void Save(string key, object obj)
        {
            if (Exist(key))
            {
                IsolatedStorageSettings.ApplicationSettings[key] = obj;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(key, obj);
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
