﻿using Client_Admin_.ViewModel.EmployeeRoleWindows;
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

namespace Client_Admin_.View
{
    /// <summary>
    /// Interaction logic for EmployeeRoleWindow.xaml
    /// </summary>
    public partial class EmployeeRoleWindow : Window
    {
        public EmployeeRoleWindow()
        {
            InitializeComponent();
            this.DataContext = new EmployeeRoleWindowVM();
        }
    }
}
