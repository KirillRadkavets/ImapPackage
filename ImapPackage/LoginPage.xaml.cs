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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImapPackage
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private bool CheckInputValidation(string user_name, string password){
            if (user_name.Equals(""))
            {
                error.Text = "Write UserName";
                return false;
            }
            else if (password.Equals(""))
            {
                error.Text = "You must provide password.";
                return false;
            }
            return true;
        }


        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
           if( CheckInputValidation(username.Text,password.Password)){
               HomePage.mail = username.Text;
               HomePage.password = password.Password;
            MainWindow.LoggedIn = ImapService.Login(username.Text, password.Password);

            // Also navigate the user
            if(MainWindow.LoggedIn)
            {
                // Logged in
                MainWindow.MainFrame.Content = new HomePage();
            }
            else
            {
                // Problem
                error.Text = "There was a problem logging you in to Google Mail.";
            }
           }
         }
    }
}
