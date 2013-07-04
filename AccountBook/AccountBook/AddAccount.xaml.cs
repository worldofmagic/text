using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using Coding4Fun.Phone.Controls;
using System.Windows.Navigation;

namespace AccountBook
{
    public partial class AddAccount : PhoneApplicationPage
    {
        private CameraCaptureTask cc;
        private TextBox LastForcusTextBox;

        public AddAccount()
        {
            InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.AddAccount_Loaded);
            this.listPickerIncome.SelectionChanged += new SelectionChangedEventHandler(this.listPickerIncome_SelectionChanged);     
        }

        private void AddAccount_Loaded(object sender, RoutedEventArgs e)
        {
            Common.BuildListPicker(this.listPickerIncome);         
            Common.BuildListPicker(this.listPickerExpenses); 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("Type"))
            {
                if (NavigationContext.QueryString["Type"].ToString() == "0")
                {
                    pivot.SelectedIndex = 0;

                }
                else
                {
                    pivot.SelectedIndex = 1;
                }
            }
        }
        private void ButtonAddPic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.cc = new CameraCaptureTask();
            this.cc.Completed += (delegate(object ss, PhotoResult arg)
            {
                if (arg.ChosenPhoto != null)
                {
                    BitmapImage image = new BitmapImage();
                    image.SetSource(arg.ChosenPhoto);
                    this.ImagePic.Source = image;
                }
            });
            this.cc.Show();
        }

        private void listPickerIncome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        //新增一条记账记录
        private void appbar_buttonAdd_Click(object sender, EventArgs e)
        {
            //用于隐藏软键盘
            pivot.Focus();
            SaveVoucher();
        }
        //新增一条记账记录并返回
        private void appbar_buttonFinish_Click(object sender, EventArgs e)
        {
            if (SaveVoucher())
            {
                //保存成功则返回上一页
                base.NavigationService.GoBack();
            }
        }
        //返回
        private void appbar_buttonCancel_Click(object sender, EventArgs e)
        {
            base.NavigationService.GoBack();
        }

        private bool SaveVoucher()
        {
            
            try
            {
                if (pivot.SelectedIndex == 0)
                {//收入
                    if (this.textBox_Income.Text.Trim() == "")
                    {
                        MessageBox.Show("金额不能为空！");
                        return false;
                    }
                    else
                    {
                        //一条记账记录的对象
                        Voucher voucher = new Voucher
                        {
                            Money = double.Parse(this.textBox_Income.Text),
                            Desc = this.textBox_IncomeDesc.Text,
                            DT = DateTime.Parse(this.DatePickerIncome.Value.Value.ToString("yyyy/MM/dd") + " " + this.TimePickerIncome.Value.Value.ToString("HH:mm:ss")),
                            Category = listPickerIncome.SelectedItem.ToString(),
                            Type = 0
                        };
                        //添加一条记录
                        App.voucherHelper.AddNew(voucher);
                    }
                }
                else
                {//支出
                    if (this.textBox_Expenses.Text.Trim() == "")
                    {
                        MessageBox.Show("金额不能为空！");
                        return false;
                    }
                    else
                    {
                        WriteableBitmap bmp = new WriteableBitmap(this.ImagePic, null);
                        //一条记账记录的对象
                        Voucher voucher = new Voucher
                        {
                            Money = double.Parse(this.textBox_Expenses.Text),
                            Desc = this.textBox_ExpensesDesc.Text,
                            DT = DateTime.Parse(this.DatePickerExpenses.Value.Value.ToString("yyyy/MM/dd") + " " + this.TimePickerExpenses.Value.Value.ToString("HH:mm:ss")),
                            Picture = ImageHelper.ToByteArray(this.ImagePic),
                            PictureHeight = bmp.PixelHeight,
                            PictureWidth = bmp.PixelWidth,
                            Category = listPickerExpenses.SelectedItem.ToString(),
                            Type = 1
                        };
                        //添加一条记录
                        App.voucherHelper.AddNew(voucher);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
            ToastPrompt tp = new ToastPrompt();
            tp.Background = pivot.Foreground;
            tp.Message = "保存成功";
            tp.Show();
            return true;
        }
    }
}