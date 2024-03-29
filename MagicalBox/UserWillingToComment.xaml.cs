﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MagicalBox
{
    /// <summary>
    /// UserWillingToComment.xaml 的交互逻辑
    /// </summary>
    public partial class UserWillingToComment : Window
    {
        public UserWillingToComment()
        {
            InitializeComponent();
        }

        private void User_Willing_Comment_Button_Click(object sender, RoutedEventArgs e)
        {
            new UserComment().Show();
            Window window = Window.GetWindow(this);//关闭父窗体
            window.Close();
        }

        private void User_Unwilling_Comment_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("感谢您的选择，接下来将跳转到抽奖页面！");
            new UserRoll().Show();
            Window window = Window.GetWindow(this);//关闭父窗体
            window.Close();
        }
    }
}
