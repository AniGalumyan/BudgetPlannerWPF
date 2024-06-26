﻿using BudgetPlannerWPF.Code;
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

namespace BudgetPlannerWPF
{
    
    public partial class RentPage : Page
    {
        public RentPage()
        {
            InitializeComponent();
            //Populate text fields with any previously supplied data
            if (MainWindow.rAmount != 0)
            {
                txbRent.Text = "" + MainWindow.rAmount;
            }
            //Change selection color of button
            var window = (MainWindow)Application.Current.MainWindow;
            window.ResetSelections();
            window.btnHome.BorderBrush = (Brush)MainWindow.bc.ConvertFrom("white");
            window.btnHome.BorderThickness = new Thickness(2);
        }

        private void btnRentSubmit_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            bool valid = true;
            // expenseTotal doesn't compound if you repeatedly click submit
            MainWindow.expenseTotal -= (MainWindow.rAmount);

            if (!decimal.TryParse(txbRent.Text, out MainWindow.rAmount))
            {
                valid = false;
                lblRentError.Content = MainWindow.decMessage;
                txbRent.BorderBrush = Brushes.Red;
            }
            else
            {
                lblRentError.Content = "";
                txbRent.BorderBrush = Brushes.Black;
            }

            if (valid)
            {
                MainWindow.r = new Rent(MainWindow.rAmount, MainWindow.gIncome, MainWindow.tDeduct, MainWindow.f, MainWindow.c, MainWindow.t, MainWindow.u, MainWindow.e);
                if (!MainWindow.limitReached) { MainWindow.n(MainWindow.rAmount); }
                window.btnHome.Background = (Brush)MainWindow.bc.ConvertFrom("#FF7FFF7F");
                //Ask user if they want to buy a vehicle
                MessageBoxResult result = MessageBox.Show("Will you be buying a vehicle?", "Buy Vehicle", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    window.btnVehicle.IsEnabled = true;
                    window.DataFrame.Content = new VehiclePage();
                }
                else
                {
                    window.btnReport.IsEnabled = true;
                    window.DataFrame.Content = new ReportPage();
                }
            }
            else
            {
               
                window.btnHome.Background = (Brush)MainWindow.bc.ConvertFrom("#FFFF7F7F");
                window.btnVehicle.IsEnabled = false;
                window.btnReport.IsEnabled = false;
            }
        }
    }
}
