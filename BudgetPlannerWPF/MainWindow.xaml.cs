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

    public delegate void Notify(decimal value);

    public partial class MainWindow : Window
    {

        //User data variables
        public static decimal gIncome, tDeduct, f, c, t, u, e;
        public static decimal rAmount;
        public static decimal pPrice, deposit, interest;
        public static int rMonths;
        //All Expenses
        public static decimal expenseTotal, limit;
        //Used to not spam user with warnings continuously
        public static bool limitReached = false;
        //Vehicle
        public static string model = "", make = "";
        public static decimal vPrice, vDeposit, vInterest, vInsurance;

        //User Objects
        public static HomeLoan? hl;
        public static Rent? r;
        public static Vehicle? v;

        //Error Messages
        public static string strMessage = "Please enter a value.";
        public static string decMessage = "Input must contain numbers and decimals only.";

        //Delegate
        public static Notify n = ExpensesCalculator;

        //BrushConverter
        public static BrushConverter bc = new();

        public static void ExpensesCalculator(decimal value)
        {
            expenseTotal += value;
            if (expenseTotal > limit)
            {
                MessageBox.Show("Your expenses are now greater than 75% of your income.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Error);
                limitReached = true;
            }
        }

        public void ResetSelections()
        {
            //Ensure only the selected page is highlighted when switching to a new page
            btnIncome.BorderBrush = (Brush)bc.ConvertFrom("blue");
            btnIncome.BorderThickness = new Thickness(1);
            btnExpenses.BorderBrush = (Brush)bc.ConvertFrom("blue");
            btnExpenses.BorderThickness = new Thickness(1);
            btnHome.BorderBrush = (Brush)bc.ConvertFrom("blue");
            btnHome.BorderThickness = new Thickness(1);
            btnVehicle.BorderBrush = (Brush)bc.ConvertFrom("blue");
            btnVehicle.BorderThickness = new Thickness(1);
            btnReport.BorderBrush = (Brush)bc.ConvertFrom("blue");
            btnReport.BorderThickness = new Thickness(1);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIncome_Click(object sender, RoutedEventArgs e)
        {
            DataFrame.Content = new IncomePage();
            btnHome.IsEnabled = false;
            btnVehicle.IsEnabled = false;
            btnReport.IsEnabled = false;
        }

        private void btnExpenses_Click(object sender, RoutedEventArgs e)
        {
            DataFrame.Content = new ExpensePage();
            btnVehicle.IsEnabled = false;
            btnReport.IsEnabled = false;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Will you be renting your house? \n (Select No for buying with a loan)", "Home finance type", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result.Equals(MessageBoxResult.Yes))
            {
                hl = null;
                DataFrame.Content = new RentPage();
                btnReport.IsEnabled = false;
            }
            else
            {
                r = null;
                DataFrame.Content = new LoanPage();
                btnReport.IsEnabled = false;
            }
        }

        private void btnVehicle_Click(object sender, RoutedEventArgs e)
        {
            DataFrame.Content = new VehiclePage();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            DataFrame.Content = new ReportPage();
        }
    }
}
