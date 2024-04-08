using BudgetPlannerWPF.Code;
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
   
    public partial class VehiclePage : Page
    {
        public VehiclePage()
        {
            InitializeComponent();
            //Populate text fields with any previously supplied data
            if (!MainWindow.model.Equals("") && !MainWindow.make.Equals("") && MainWindow.vPrice != 0 && MainWindow.vDeposit != 0 && MainWindow.vInterest != 0 && MainWindow.vInsurance != 0)
            {
                txbMake.Text = MainWindow.make;
                txbVehiclePrice.Text = "" + MainWindow.vPrice;
                txbVehicleDeposit.Text = "" + MainWindow.vDeposit;
            }
            //Change selection color of button
            var window = (MainWindow)Application.Current.MainWindow;
            window.ResetSelections();
            window.btnVehicle.BorderBrush = (Brush)MainWindow.bc.ConvertFrom("#FF84A8FF");
            window.btnVehicle.BorderThickness = new Thickness(2);
        }

        private void btnVehicleSubmit_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            bool valid = true;
            decimal vehicleExpense = 0;
            //Makes sure expenseTotal doesn't compound if you repeatedly click submit
            MainWindow.expenseTotal -= (vehicleExpense);

            if (string.IsNullOrWhiteSpace(txbMake.Text))
            {
                valid = false;
                lblMakeModelError.Content = MainWindow.strMessage;
                txbMake.BorderBrush = Brushes.Red;
            }
            else
            {
                MainWindow.make = txbMake.Text;
                lblMakeModelError.Content = "";
                txbMake.BorderBrush = Brushes.Black;
            }

           

            if (!decimal.TryParse(txbVehiclePrice.Text, out MainWindow.vPrice))
            {
                valid = false;
                lblVehiclePriceError.Content = MainWindow.decMessage;
                txbVehiclePrice.BorderBrush = Brushes.Red;
            }
            else
            {
                lblVehiclePriceError.Content = "";
                txbVehiclePrice.BorderBrush = Brushes.Black;
            }

            if (!decimal.TryParse(txbVehicleDeposit.Text, out MainWindow.vDeposit))
            {
                valid = false;
                lblVehicleDepositError.Content = MainWindow.decMessage;
                txbVehicleDeposit.BorderBrush = Brushes.Red;
            }
            else
            {
                lblVehicleDepositError.Content = "";
                txbVehicleDeposit.BorderBrush = Brushes.Black;
            }

            
            if (valid)
            {
                MainWindow.v = new Vehicle(MainWindow.make,MainWindow.vPrice, MainWindow.vDeposit);
                vehicleExpense = MainWindow.v.CalculateVehicleExpense();
                if (!MainWindow.limitReached) { MainWindow.n(vehicleExpense); }
                window.btnVehicle.Background = (Brush)MainWindow.bc.ConvertFrom("#FF7FFF7F");
                window.btnReport.IsEnabled = true;
                window.DataFrame.Content = new ReportPage();
            }
            else
            {
                
                window.btnVehicle.Background = (Brush)MainWindow.bc.ConvertFrom("white");
                window.btnReport.IsEnabled = false;
            }
        }
    }
}
