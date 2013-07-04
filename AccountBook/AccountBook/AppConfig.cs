using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace AccountBook
{
    /// <summary>
    /// 应用程序配置类
    /// </summary>
    public class AppConfig
    {
        //配置文件的名称
        private const string AppConfigFileName = "AppConfigFileName.dat";
        //键值对应字典
        private static Dictionary<string, string> items { get; set; }
        /// <summary>
        /// 获取配置文件里面键值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetData(string key)
        {
            if (items == null)
            {
                //读取配置文件
                ReadFromFile();
            }
            if (!items.ContainsKey(key))
            {
                items.Add(key, "");
            }
            return items[key];
        }
        /// <summary>
        /// 加载数据到时间控件
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="TimePicker">时间控件</param>
        /// <returns>是否成功</returns>
        public static bool LoadDataTo(string key, TimePicker TimePicker)
        {
            if (TimePicker == null)
            {
                return false;
            }
            if (items == null)
            {
                ReadFromFile();
            }
            if (!items.ContainsKey(key))
            {
                items.Add(key, "");
            }
            try
            {
                if (!string.IsNullOrEmpty(items[key]))
                {
                    //把键值转化为时间格式赋值给时间控件
                    TimePicker.Value = new DateTime?(DateTime.Parse(items[key]));
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 加载数据到开关控件
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="ToggleSwitch">开关控件</param>
        public static void LoadDataTo(string key, ToggleSwitch ToggleSwitch)
        {
            if (ToggleSwitch != null)
            {
                if (items == null)
                {
                    ReadFromFile();
                }
                if (!items.ContainsKey(key))
                {
                    items.Add(key, "");
                }
                try
                {
                    if (!string.IsNullOrEmpty(items[key]))
                    {
                        if (items[key].ToString().ToLower() == "true")
                        {
                            ToggleSwitch.IsChecked = true;
                        }
                        else
                        {
                            ToggleSwitch.IsChecked = false;
                        }
                    }
                }
                catch (Exception)
                {
                    ToggleSwitch.IsChecked = false;
                }
            }
        }
        /// <summary>
        /// 加载数据到密码控件
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="pwdBox">密码控件</param>
        public static void LoadDataTo(string key, PasswordBox pwdBox)
        {
            if (pwdBox != null)
            {
                if (items == null)
                {
                    ReadFromFile();
                }
                if (!items.ContainsKey(key))
                {
                    items.Add(key, "");
                }
                if (!string.IsNullOrEmpty(items[key]))
                {
                    pwdBox.Password = items[key];
                }
            }
        }

        public static void LoadDataTo(string key, TextBox textbox)
        {
            if (textbox != null)
            {
                if (items == null)
                {
                    ReadFromFile();
                }
                if (!items.ContainsKey(key))
                {
                    items.Add(key, "");
                }
                if (!string.IsNullOrEmpty(items[key]))
                {
                    textbox.Text = items[key];
                }
            }
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        public static void ReadFromFile()
        {
            //判断配置文件是否存在
            if (!IsolatedStorageHelper.isFileExist("AppConfigFileName.dat"))
            {
                //不存在配置文件则新建一个键值对应字典
                items = new Dictionary<string, string>();
            }
            else
            {
                //配置文件存在则读取配置文件
                try
                {
                    //打开配置文件
                    IsolatedStorageFileStream stream = IsolatedStorageHelper.OpenFile("AppConfigFileName.dat", FileMode.Open);
                    //创建键值对应字典的可序列化类对象
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, string>));
                    //序列化配置文件为键值对应字典
                    items = serializer.ReadObject(stream) as Dictionary<string, string>;
                    //关闭文件流
                    stream.Close();
                }
                catch (Exception)
                {
                    //打开文件或者序列化异常则重新创建一个键值对应字典
                    items = new Dictionary<string, string>();
                }
            }
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        public static void SaveToFile()
        {
            //创建配置文件
            IsolatedStorageFileStream stream = IsolatedStorageHelper.CreateFile("AppConfigFileName.dat");
            //用可序列化对象把键值对应字典写入配置文件
            new DataContractSerializer(typeof(Dictionary<string, string>)).WriteObject(stream, items);
            //关闭文件流
            stream.Close();
        }
        /// <summary>
        /// 设置时间值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="TimePicker">时间控件</param>
        public static void SetData(string key, TimePicker TimePicker)
        {
            if (items == null)
            {
                ReadFromFile();
            }
            if (!items.ContainsKey(key))
            {
                items.Add(key, TimePicker.Value.Value.ToString("HH:mm"));
            }
            else
            {
                items[key] = TimePicker.Value.Value.ToString("HH:mm");
            }
        }
        /// <summary>
        /// 设置开关值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="ToggleSwitch">开关控件</param>
        public static void SetData(string key, ToggleSwitch ToggleSwitch)
        {
            if (items == null)
            {
                ReadFromFile();
            }
            if (!items.ContainsKey(key))
            {
                items.Add(key, ToggleSwitch.IsChecked.ToString());
            }
            else
            {
                items[key] = ToggleSwitch.IsChecked.ToString();
            }
        }
        /// <summary>
        /// 设置普通值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetData(string key, string value)
        {
            if (items == null)
            {
                ReadFromFile();
            }
            if (!items.ContainsKey(key))
            {
                items.Add(key, value);
            }
            else
            {
                items[key] = value;
            }
        }
    }
}
