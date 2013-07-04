using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
namespace AccountBook
{
    public class IsolatedStorageHelper
    {
        //获取应用独立存储文件
        private static IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();
        /// <summary>
        /// 创建一个独立存储文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>独立存储文件流</returns>
        public static IsolatedStorageFileStream CreateFile(string path)
        {
            try
            {
                //如果文件存在需要先将文件删除
                if (isFileExist(path))
                {
                    DeleteFile(path);
                }
                return iso.CreateFile(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 删除一个独立存储文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteFile(string path)
        {
            try
            {
                iso.DeleteFile(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 判断独立存储文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否存在</returns>
        public static bool isFileExist(string path)
        {
            return iso.FileExists(path);
        }
        /// <summary>
        /// 打开一个独立存储文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="mode">文件模型</param>   
        /// <returns>独立存储文件流</returns>
        public static IsolatedStorageFileStream OpenFile(string path, FileMode mode)
        {
            if (isFileExist(path))
            {
                return iso.OpenFile(path, mode);
            }
            return null;
        }
        /// <summary>
        /// 读取独立存储文件流的内容
        /// </summary>
        /// <param name="fs">独立存储文件流</param>
        /// <returns>文件内容字符串</returns>
        public static string ReadAllText(IsolatedStorageFileStream fs)
        {
            try
            {
                //转化为刻度流
                StreamReader reader = new StreamReader(fs);
                //把流转化为字符串
                string str = reader.ReadToEnd();
                //关闭流
                reader.Close();
                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 读取独立存储文件流的内容
        /// </summary>
        /// <param name="FileFullName">文件路径</param>
        /// <returns>文件内容字符串</returns>
        public static string ReadAllText(string FileFullName)
        {
            try
            {
                IsolatedStorageFileStream fs = OpenFile(FileFullName, FileMode.Open);
                if (fs == null)
                {
                    return "";
                }
                string str = ReadAllText(fs);
                fs.Close();
                return str;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 读取文件里面的
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="type">对象类型</param>
        /// <returns>XML序列化对象</returns>  
        public static object ReadObjectFromFile(string path, Type type)
        {
            try
            {
                FileStream stream = OpenFile(path, FileMode.Open);
                object obj2 = new DataContractSerializer(type).ReadObject(stream);
                stream.Close();
                return obj2;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 把字符串写入独立存储文件
        /// </summary>
        /// <param name="fs">独立存储文件流</param>
        /// <param name="Text">字符串</param>
        /// <returns>是否成功</returns>
        public static bool WriteAllText(IsolatedStorageFileStream fs, string Text)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fs);
                writer.Write(Text);
                writer.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 把字符串写入独立存储文件
        /// </summary>
        /// <param name="FileFullName">文件名字</param>
        /// <param name="Text">字符创</param>
        /// <returns>是否成功</returns>
        public static bool WriteAllText(string FileFullName, string Text)
        {
            IsolatedStorageFileStream fs = CreateFile(FileFullName);
            if (WriteAllText(fs, Text))
            {
                fs.Close();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 把可序列化对象写入独立存储文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="type">对象类型</param>
        /// <param name="obj">可序列化对象</param>
        /// <returns></returns>
        public static bool WriteObjectToFile(string path, Type type, object obj)
        {
            try
            {
                FileStream stream = CreateFile(path);
                new DataContractSerializer(type).WriteObject(stream, obj);
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
