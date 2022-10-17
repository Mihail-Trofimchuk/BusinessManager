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

namespace BusinessManager.Views.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для AccountModalWindow.xaml
    /// </summary>
    public partial class AccountModalWindow : Window
    {
        public AccountModalWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
