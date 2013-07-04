using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;

namespace AccountBook
{
    public class Common
    {
        public static void BuildListPicker(ListPicker listPicker)
        {
            List<string> list = new List<string>();
            if (listPicker.Items.Count <= 0)
            {
                listPicker.FontSize = 50;
                list.Add("工资");
                list.Add("房租");
                list.Add("娱乐");
                list.Add("餐饮");
                list.Add("交通");
                list.Add("其他");
            }
            listPicker.ItemsSource = from c in list
                                     select c into c
                                     orderby c
                                     select c;
        }
        /// <summary>
        /// 获取预算的金额
        /// </summary>
        /// <param name="ItemName">类别名称</param>
        /// <returns></returns>
        public static double GetLimitOf(string ItemName)
        {
            IEnumerable<Budget> source = from c in App.budgetHelper.data
                                         where c.ItemName == ItemName
                                         select c;
            if (source.Count<Budget>() > 0)
            {
                return (double)source.FirstOrDefault<Budget>().Limit;
            }
            return -1.0;
        }
        /// <summary>
        /// 获取所有的记账记录
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Voucher> GetAllRecords()
        {
            return (from c in App.voucherHelper.data select c);
        }
        /// <summary>
        /// 获取某月的所有记录
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static IEnumerable<Voucher> GetThisMonthAllRecords(int month, int year)
        {
            return (from c in App.voucherHelper.data
                    where (c.DT.Month == month) && (c.DT.Year == year)
                    select c);
        }
        /// <summary>
        /// 获取某日的所有记录
        /// </summary>
        /// <param name="day">日</param>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static IEnumerable<Voucher> GetThisDayAllRecords(int day, int month, int year)
        {
            return (from c in App.voucherHelper.data
                    where (c.DT.Day == day) && (c.DT.Month == month) && (c.DT.Year == year)
                    select c);
        }
        /// <summary>
        /// 获取本年的所有记录
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static IEnumerable<Voucher> GetThisYearAllRecords(int year)
        {
            return (from c in App.voucherHelper.data
                    where c.DT.Year == year
                    select c);
        }
        /// <summary>
        /// 获取本月的类别总金额
        /// </summary>
        /// <param name="ItemName">类别名称</param>
        /// <param name="type">类别类型</param>
        /// <returns>金额</returns>
        public static double GetThisMonthSummaryOf(string ItemName,short type)
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Month == DateTime.Now.Month) && (c.DT.Year == DateTime.Now.Year)) && (c.Type == type) && (c.Category == ItemName)
                                          select c.Money)).Sum();
        }
        /// <summary>
        ///  获取本月的类别总金额
        /// </summary>
        /// <param name="category">类别实体</param>
        /// <returns>金额</returns>
        public static double GetThisMonthSummaryOf(Category category)
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Month == DateTime.Now.Month) && (c.DT.Year == DateTime.Now.Year)) && (c.Type == category.Type) && (c.Category == category.Name)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取总支出
        /// </summary>
        /// <returns>金额</returns>
        public static double GetSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where c.Type == 1
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本年的支出
        /// </summary>
        /// <returns>金额</returns>
        public static double GetThisYearSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == DateTime.Now.Year)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某年的支出
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static double GetYearSummaryExpenses(int year)
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == year)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本月的支出
        /// </summary>
        /// <returns></returns>
        public static double GetThisMouthSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == DateTime.Now.Year)) && ((c.DT.Month == DateTime.Now.Month)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某月的支出
        /// </summary>
        /// <param name="mouth">月份</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static double GetMouthSummaryExpenses(int mouth, int year)
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == year)) && ((c.DT.Month == mouth)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取总收入
        /// </summary>
        /// <returns>金额</returns>
        public static double GetSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where c.Type == 0
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本年的收入
        /// </summary>
        /// <returns>金额</returns>
        public static double GetThisYearSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == DateTime.Now.Year)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某年的收入
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static double GetYearSummaryIncome(int year)
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == year)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本月的收入
        /// </summary>
        /// <returns>金额</returns>
        public static double GetThisMouthSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == DateTime.Now.Year)) && ((c.DT.Month == DateTime.Now.Month)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某月的收入
        /// </summary>
        /// <param name="mouth">月份</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static double GetMouthSummaryIncome(int mouth, int year)
        {
            return ((IEnumerable<double>)(from c in App.voucherHelper.data
                                          where ((c.DT.Year == year)) && ((c.DT.Month == mouth)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 查询记账记录
        /// </summary>
        /// <param name="begin">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="keyWords">关键字</param>
        /// <returns>记账记录</returns>
        public static IEnumerable<Voucher> Search(DateTime? begin, DateTime? end, string keyWords)
        {
            if (keyWords == "")
            {
                return (from c in App.voucherHelper.data
                        where c.DT >= begin && c.DT <= end
                        select c);
            }
            else
            {
                return (from c in App.voucherHelper.data
                        where c.DT >= begin && c.DT <= end && c.Desc.IndexOf(keyWords) >= 0
                        select c);
            }
        }
    }
}
