﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        private bool pauser;
        FlightBoardViewModel fltBrdVM = new FlightBoardViewModel();
        ObservableDataSource<Point> planeLocations = null;
        public FlightBoard()
        {
            InitializeComponent();
            pauser = false;
            //fltBrdVM = new FlightBoardViewModel();//changed
            fltBrdVM.PropertyChanged += Vm_PropertyChanged;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (pauser)
            {
                if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
                {
                    Point p1 = new Point(fltBrdVM.Lat, fltBrdVM.Lon);// Fill here!
                    planeLocations.AppendAsync(Dispatcher, p1);
                    pauser = false;
                }
            }
            else
            {
                pauser = true;
            }
        }
    }
}

