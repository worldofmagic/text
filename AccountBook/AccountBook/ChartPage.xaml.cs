using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using Microsoft.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;

namespace AccountBook
{
    public partial class ChartPage : PhoneApplicationPage
    {
        public ChartPage()
        {
            InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.Report_Loaded);
        }
        // 页面加载事件处理
        private void Report_Loaded(object sender, RoutedEventArgs e)
        {
            // 创建图表的数据源对象
            ObservableCollection<ChartData> collecion = new ObservableCollection<ChartData>();
            // 获取所有的记账记录
            IEnumerable<Voucher> allRecords = Common.GetAllRecords();
            // 获取所有记账记录里面的类别
            IEnumerable<string> enumerable2 = (from c in allRecords select c.Category).Distinct<string>();
            // 按照类别来统计记账的数目
            foreach (var item in enumerable2)
            {
                // 获取该类别下的钱的枚举集合
                IEnumerable<double> enumerable3 = from c in allRecords.Where<Voucher>(c => c.Category == item) select c.Money;
                // 添加一条图表的数据
                ChartData data = new ChartData
                {
                    Sum = enumerable3.Sum(),
                    TypeName = item
                };
                collecion.Add(data);
            }
            // 新建一个饼状图表的控件对象
            PieSeries series = new PieSeries();
            // 新建一个柱形图表的控件对象
            ColumnSeries series2 = new ColumnSeries();
            // 绑定数据源
            series.ItemsSource = collecion;
            series.DependentValueBinding = new Binding("Sum");
            series.IndependentValueBinding = new Binding("TypeName");
            series2.ItemsSource = collecion;
            series2.DependentValueBinding = new Binding("Sum");
            series2.IndependentValueBinding = new Binding("TypeName");
            // 添加到图表里面
            this.chart1.Series.Clear();
            this.chart1.Series.Add(series);
            this.chart2.Series.Clear();
            this.chart2.Series.Add(series2);
        }
    }
}