using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace AccountBook
{
    public partial class Search : PhoneApplicationPage
    {
        public Search()
        {
            InitializeComponent();
        }

        // 处理菜单栏单击事件，查询记账记录
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            DateTime? begin = DatePickerBegin.Value;
            DateTime? end = DatePickerEnd.Value;
            listReport.ItemsSource = Common.Search(begin, end, keyWords.Text);
        }
    }
}