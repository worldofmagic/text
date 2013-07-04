using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
namespace AccountBook
{
    public partial class YearReport : PhoneApplicationPage
    {
        private int year;
        public YearReport()
        {
            InitializeComponent();
        }
        // 处理页面加载事件
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            year = DateTime.Now.Year;
            DisplayVoucherData();
        }
        // 处理菜单栏单击事件
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch ((sender as ApplicationBarIconButton).Text)
                {
                    case "上一年":
                        this.year--;
                        break;
                    case "下一年":
                        this.year++;
                        break;
                }
                DisplayVoucherData();         
            }
            catch
            {
            }
        }
        // 展现记账的数据
        private void DisplayVoucherData()
        {
            //本年的收入
            double inSum = Common.GetYearSummaryIncome(year);
            //本年的支出
            double exSum = Common.GetYearSummaryExpenses(year);
            //显示本年收入
            inTB.Text = "收入:" + inSum;
            //显示本年支出
            exTB.Text = "支出:" + exSum;
            //显示本年结余
            balanceTB.Text = "结余:" + (inSum - exSum);
            //绑定当前年份的记账记录
            listYearReport.ItemsSource = Common.GetThisYearAllRecords(year);
            PageTitle.Text = year + "年";
        }
    }
}