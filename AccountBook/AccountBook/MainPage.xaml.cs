using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace AccountBook
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();     
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }
        //页面加载处理
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            trexStoryboard.Begin();
            //设置收入Tile的总收入金额
            SummaryIncome.Content ="总收入："+ Common.GetSummaryIncome().ToString()+"元";
            //设置支出Tile的总支出金额
            SummaryExpenses.Content = "总支出" + Common.GetSummaryExpenses().ToString()+"元";
            //计算月结余
            double mouthIncome = Common.GetThisMouthSummaryIncome();
            double mouthExpenses = Common.GetThisMouthSummaryExpenses();
            MouthBalance.Content = "月结余：" + (mouthIncome - mouthExpenses).ToString() + "月";
            //计算年结余
            double yearIncome = Common.GetThisYearSummaryIncome();
            double yearExpenses = Common.GetThisYearSummaryExpenses();
            YearBalance.Content = "年结余：" + (yearIncome - yearExpenses).ToString() + "月";
            //获取今日的账单记录，并绑定到首页的ListBox控件进行显示
            listToday.ItemsSource = Common.GetThisDayAllRecords(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        }
        //跳转到新增一笔收入页面
        private void Income_Tile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddAccount.xaml?Type=0", UriKind.Relative));
        }
        //跳转到新增一笔支出页面
        private void Expenses_Tile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddAccount.xaml?Type=1", UriKind.Relative));
        }
        //跳转到图表分析页面
        private void Chart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ChartPage.xaml", UriKind.Relative));
        }
        //跳转到月报表页面
        private void MouthReport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MouthReport.xaml", UriKind.Relative));
        }
        //跳转到年报表页面
        private void YearReport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/YearReport.xaml", UriKind.Relative));
        }
        //跳转到添加类别页面
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddCategory.xaml", UriKind.Relative));
        }
        //跳转到查询页面
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Search.xaml", UriKind.Relative));
        }
        //跳转到理财计划页面
        private void Note_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Note.xaml", UriKind.Relative));
        }
        //跳转到设置页面
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Setting.xaml", UriKind.Relative));
        }
    }
}