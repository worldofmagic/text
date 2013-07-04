using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
namespace AccountBook
{
    public partial class MouthReport : PhoneApplicationPage
    {
        private int mouth;
        private int year;
        public MouthReport()
        {
            InitializeComponent();
        }
        // 处理页面加载事件
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            mouth = DateTime.Now.Month;
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
                    case "上一月":
                        this.mouth--;
                        if (this.mouth <= 0)
                        {
                            this.year--;
                            this.mouth = 12;
                        }
                        break;
                    case "下一月":
                        this.mouth++;
                        if (this.mouth >= 12)
                        {
                            this.year++;
                            this.mouth = 1;
                        }
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
            //本月的收入
            double inSum = Common.GetMouthSummaryIncome(mouth, year);
            //本月的支出
            double exSum = Common.GetMouthSummaryExpenses(mouth, year);
            //显示本月收入
            inTB.Text = "收入:" + inSum;
            //显示本月支出
            exTB.Text = "支出:" + exSum;
            //显示本月结余
            balanceTB.Text = "结余:" + (inSum - exSum);
            //绑定当前月份的记账记录
            listMouthReport.ItemsSource = Common.GetThisMonthAllRecords(mouth, year);
            PageTitle.Text = year + "年" + mouth + "月";
        }
    }
}