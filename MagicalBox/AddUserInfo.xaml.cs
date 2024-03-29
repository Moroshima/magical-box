﻿using MagicalBox.Database;
using MagicalBox.Model;
using System;
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
    /// AddUserInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AddUserInfo : Window
    {
        public AddUserInfo()
        {
            InitializeComponent();
        }

        private void Add_User_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Add_User_BoxId.Text == "") MessageBox.Show("盒子编号为必填项，请有效填写！");
            else if (Add_User_IdCard.Text == "") MessageBox.Show("有效证件为必填项，请有效填写！");
            else if (Add_User_PhoneNumber.Text == "") MessageBox.Show("电话号码为必填项，请有效填写！");
            else if(Add_User_BackupLink.Text == "") MessageBox.Show("备用联系方式为必填项，请有效填写！");
            else
            {
                if (int.TryParse(Add_User_BoxId.Text, out int BoxId_Number))
                {
                    string PhoneNumber_String = Add_User_PhoneNumber.Text;
                    string PhoneNumber_First = PhoneNumber_String.Substring(0, 1);
                    if (PhoneNumber_First == "1" && PhoneNumber_String.Length == 11)
                    {
                        if (Add_User_IdCard.Text.Length == 18
                        && int.TryParse(Add_User_IdCard.Text.Substring(0, 6), out int TryOutput1)
                        && int.TryParse(Add_User_IdCard.Text.Substring(6, 6), out int TryOutput2)
                        && int.TryParse(Add_User_IdCard.Text.Substring(12, 5), out int TryOutput3)
                        && (int.TryParse(Add_User_IdCard.Text.Substring(17, 1), out int TryOutput4) || Add_User_IdCard.Text.Substring(17, 1)=="X" || Add_User_IdCard.Text.Substring(17, 1) == "x"))
                        {
                            string IdCard_Text = Add_User_IdCard.Text;
                            int Birth_Year_Number = int.Parse(IdCard_Text.Substring(6, 4));
                            int Birth_Month_Number = int.Parse(IdCard_Text.Substring(10, 2));
                            int MaxDayLength = 0;
                            switch (Birth_Month_Number)
                            {
                                case 1:
                                    MaxDayLength = 31;
                                    break;
                                case 2:
                                    if ((Birth_Year_Number % 4 == 0 && Birth_Year_Number % 100 != 0) || Birth_Year_Number % 400 == 0)
                                        MaxDayLength = 29;
                                    else MaxDayLength = 28;
                                    break;
                                case 3:
                                    MaxDayLength = 31;
                                    break;
                                case 4:
                                    MaxDayLength = 30;
                                    break;
                                case 5:
                                    MaxDayLength = 31;
                                    break;
                                case 6:
                                    MaxDayLength = 30;
                                    break;
                                case 7:
                                    MaxDayLength = 31;
                                    break;
                                case 8:
                                    MaxDayLength = 31;
                                    break;
                                case 9:
                                    MaxDayLength = 30;
                                    break;
                                case 10:
                                    MaxDayLength = 31;
                                    break;
                                case 11:
                                    MaxDayLength = 30;
                                    break;
                                case 12:
                                    MaxDayLength = 31;
                                    break;
                                default:
                                    MaxDayLength = 0;
                                    break;
                            }
                            int Birth_Day_Number = int.Parse((IdCard_Text.Substring(12, 2)));
                            if (Birth_Year_Number >= 1900 && Birth_Year_Number <= 2021 && Birth_Month_Number >= 1 && Birth_Month_Number <= 12 && Birth_Day_Number >= 1 && Birth_Day_Number <= MaxDayLength)
                            {
                                bool Box_Already_Exist = false;
                                bool IdCard_Already_Exist = false;
                                bool PhoneNumber_Already_Exist = false;
                                using AppDbContext dbContext = new AppDbContext();
                                foreach (var item in dbContext.Users.Where(e => e.BoxId == BoxId_Number))
                                {
                                    Box_Already_Exist = true;
                                }
                                foreach (var item in dbContext.Users.Where(e => e.IdCard == Add_User_IdCard.Text))
                                {
                                    IdCard_Already_Exist = true;
                                }
                                foreach (var item in dbContext.Users.Where(e => e.PhoneNumber == Add_User_PhoneNumber.Text))
                                {
                                    PhoneNumber_Already_Exist = true;
                                }
                                if (Box_Already_Exist) MessageBox.Show("盒子已被使用！");
                                else if (IdCard_Already_Exist) MessageBox.Show("该有效证件已登记！");
                                else if (PhoneNumber_Already_Exist) MessageBox.Show("该电话号码已被使用");
                                else
                                {
                                    //string Box_Id_String = Add_User_BoxId.Text;
                                    var data = new User
                                    {
                                        BoxId = BoxId_Number,
                                        Username = Add_User_Username.Text,
                                        IdCard = Add_User_IdCard.Text,
                                        BoardCard = Add_User_BoardCard.Text,
                                        Departure = Add_User_Departure.Text,
                                        Destination = Add_User_Destination.Text,
                                        PhoneType = Add_User_PhoneType.Text,
                                        PhoneNumber = Add_User_PhoneNumber.Text,
                                        BackupLink = Add_User_BackupLink.Text,
                                        Returned = false,
                                    };
                                    dbContext.Users.Add(data);
                                    dbContext.SaveChanges();
                                    Window window = Window.GetWindow(this);    //关闭父窗体
                                    window.Close();
                                }
                            }
                            else MessageBox.Show("有效证件号码格式错误，请重新输入！");
                        }
                        else MessageBox.Show("有效证件号码长度有误，请重新输入！");
                    }
                    else MessageBox.Show("手机号码格式错误，请重新输入！");
                }
                else MessageBox.Show("盒子编号填写错误，请重新输入！");
            }
        }

        private void Add_User_Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);    //关闭父窗体
            window.Close();
        }
    }
}
