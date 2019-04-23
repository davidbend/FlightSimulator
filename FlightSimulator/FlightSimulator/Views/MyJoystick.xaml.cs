﻿using FlightSimulator.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MyJoystick.xaml
    /// </summary>
    public partial class MyJoystick : UserControl
    {
        public MyJoystick()
        {
            InitializeComponent();
            this.DataContext = new ManualBinding(ServerConnect.Instance);
        }
    }
}
