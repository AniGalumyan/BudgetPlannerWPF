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
    
    public partial class ExpensePage : Page
    {
        public ExpensePage()
        {
            InitializeComponent();
            // Populate text boxes with data previously entered, if available

            if (MainWindow.f != 0 && MainWindow.c != 0 && MainWindow.t != 0 && MainWindow.u != 0 && MainWindow.e != 0)
            {
                txbF.Text = "" + MainWindow.f;
                txbC.Text = "" + MainWindow.c;
                txbT.Text = "" + MainWindow.t;
                txbU.Text = "" + MainWindow.u;
                txbE.Text = "" + MainWindow.e;
            }
            //Modify the button's selection color.

            var window = (MainWindow)Application.Current.MainWindow;
            window.ResetSelections();
            window.btnExpenses.BorderBrush = (Brush)MainWindow.bc.ConvertFrom("#FF84A8FF");
            window.btnExpenses.BorderThickness = new Thickness(2);
        }

        private void btnExpenseSubmit_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            bool valid = true;
            //expenseTotal doesn't compound if you repeatedly click submit
            MainWindow.expenseTotal -= (MainWindow.f + MainWindow.c + MainWindow.t + MainWindow.u + MainWindow.e);

            if (!decimal.TryParse(txbF.Text, out MainWindow.f))
            {
                valid = false;
                lblFsError.Content = MainWindow.decMessage;
                txbF.BorderBrush = Brushes.Red;
            }
            else
            {
                lblFsError.Content = "";
                txbF.BorderBrush = Brushes.Black;
            }
            // check if expenses are over the limit
            if (!MainWindow.limitReached) { MainWindow.n(MainWindow.f); }

            if (!decimal.TryParse(txbC.Text, out MainWindow.c))
            {
                valid = false;
                lblCsError.Content = MainWindow.decMessage;
                txbC.BorderBrush = Brushes.Red;
            }
            else
            {
                lblCsError.Content = "";
                txbC.BorderBrush = Brushes.Black;
            }
            if (!MainWindow.limitReached) { MainWindow.n(MainWindow.c); }

            if (!decimal.TryParse(txbT.Text, out MainWindow.t))
            {
                valid = false;
                lblTError.Content = MainWindow.decMessage;
                txbT.BorderBrush = Brushes.Red;
            }
            else
            {
                lblTError.Content = "";
                txbT.BorderBrush = Brushes.Black;
            }
            if (!MainWindow.limitReached) { MainWindow.n(MainWindow.t); }

            if (!decimal.TryParse(txbU.Text, out MainWindow.u))
            {
                valid = false;
                lblUError.Content = MainWindow.decMessage;
                txbU.BorderBrush = Brushes.Red;
            }
            else
            {
                lblUError.Content = "";
                txbU.BorderBrush = Brushes.Black;
            }
            if (!MainWindow.limitReached) { MainWindow.n(MainWindow.u); }

            if (!decimal.TryParse(txbE.Text, out MainWindow.e))
            {
                valid = false;
                lblEError.Content = MainWindow.decMessage;
                txbE.BorderBrush = Brushes.Red;
            }
            else
            {
                lblEError.Content = "";
                txbE.BorderBrush = Brushes.Black;
            }
            if (!MainWindow.limitReached) { MainWindow.n(MainWindow.e); }

            if (valid)
            {
                window.btnExpenses.Background = (Brush)MainWindow.bc.ConvertFrom("#FF7FFF7F");
                window.btnHome.IsEnabled = true;
                MessageBoxResult result = MessageBox.Show("Will you be renting your house? \n (Select No for buying with a loan)", "Home finance type", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    MainWindow.hl = null;
                    window.DataFrame.Content = new RentPage();
                }
                else
                {
                    MainWindow.r = null;
                    window.DataFrame.Content = new LoanPage();
                }
            }
            else
            {
               
                window.btnExpenses.Background = (Brush)MainWindow.bc.ConvertFrom("#FFFF7F7F");
                window.btnHome.IsEnabled = false;
                window.btnVehicle.IsEnabled = false;
                window.btnReport.IsEnabled = false;
            }
        }
    }
}
