using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace AccountBook
{
    /// <summary>
    /// 图片处理类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 把字节数组转化为位图
        /// </summary>
        /// <param name="imageBytes">字节数组</param>
        /// <returns>位图</returns>
        public static BitmapImage ByteToImageSource(byte[] imageBytes)
        {
            BitmapImage image = new BitmapImage();
            MemoryStream streamSource = new MemoryStream(imageBytes);
            image.SetSource(streamSource);
            return image;
        }
        /// <summary>
        /// 从图片的目录获取位图
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>位图</returns>
        public static BitmapImage GetImageFromContentPath(string path)
        {
            Uri uriResource = new Uri(path, UriKind.Relative);
            using (BinaryReader reader = new BinaryReader(Application.GetResourceStream(uriResource).Stream))
            {
                return ByteToImageSource(reader.ReadBytes((int)reader.BaseStream.Length));
            }
        }
        /// <summary>
        /// 从图片的资源的路径获取位图
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns>位图</returns>
        public static BitmapImage GetImageFromResourcePath(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }
        /// <summary>
        /// 把图片转化成字节数组
        /// </summary>
        /// <param name="img">图片控件</param>
        /// <returns>字节数组</returns>
        public static byte[] ToByteArray(Image img)
        {
            //把图片转化为可写位图
            WriteableBitmap bitmap = new WriteableBitmap(img, null);
            MemoryStream streamSource = new MemoryStream();
            System.Windows.Media.Imaging.Extensions.SaveJpeg(bitmap, streamSource, bitmap.PixelWidth, bitmap.PixelHeight, 0, 100);
            //像素宽度
            int pixelWidth = bitmap.PixelWidth;
            //像素高度
            int pixelHeight = bitmap.PixelHeight;
            //像素int数组
            int[] pixels = bitmap.Pixels;
            //图片像素int数组的长度
            int length = pixels.Length;
            //初始化int数组对应的byte数组
            byte[] buffer = new byte[(4 * pixelWidth) * pixelHeight];
            //把int数组转化成byte数组
            int index = 0;
            for (int i = 0; index < length; i += 4)
            {
                int num6 = pixels[index];
                buffer[i] = (byte)(num6 >> 0x18);
                buffer[i + 1] = (byte)(num6 >> 0x10);
                buffer[i + 2] = (byte)(num6 >> 8);
                buffer[i + 3] = (byte)num6;
                index++;
            }
            new BitmapImage().SetSource(streamSource);
            return buffer;
        }
    }
}
