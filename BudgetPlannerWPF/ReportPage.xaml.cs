using BudgetPlannerWPF.Code;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        static List<string> reportList = new List<string>();
        static double target = 0;
        static decimal available = 0;
        static int years = 0;

        public ReportPage()
        {
            InitializeComponent();
            //Change selection color of button
            var window = (MainWindow)Application.Current.MainWindow;
            window.ResetSelections();
            window.btnReport.BorderBrush = (Brush)MainWindow.bc.ConvertFrom("#FF84A8FF");
            window.btnReport.BorderThickness = new Thickness(2);

            reportList.Clear();
            lstExpenses.Items.Clear();

            FinalReport();
            //Add sorted items to list to display
            foreach (var item in reportList)
            {
                lstExpenses.Items.Add(item);
            }
        }

        public void FinalReport()
        {
            List<KeyValuePair<decimal, string>> allExpenses = new List<KeyValuePair<decimal, string>>();
            decimal vehicleExpense = 0;
            decimal availRent = 0;
            //Display expenses in descending order
            if (MainWindow.v != null)
            {
                vehicleExpense = MainWindow.v.CalculateVehicleExpense();
                allExpenses.Add(new KeyValuePair<decimal, string>(vehicleExpense, "Vehicle Repayments: "));
            }

            if (MainWindow.hl == null && MainWindow.r != null)
            {
                allExpenses.AddRange(MainWindow.r.namedExpenses);
                allExpenses.Add(new KeyValuePair<decimal, string>(MainWindow.r.RentalAmount, "Rent: "));
            }
            else if(MainWindow.hl != null)
            {
                allExpenses.AddRange(MainWindow.hl.namedExpenses);
                allExpenses.Add(new KeyValuePair<decimal, string>(MainWindow.hl.MonthlyRepayment, "Home Loan Repayments: "));
            }
            /* Sorting code from:
             * https://stackoverflow.com/questions/14544953/c-sharp-sorting-a-listkeyvaluepairint-string
             * User answered:
             * https://stackoverflow.com/users/69083/guffa
             * Accessed 3 June 2022*/
            allExpenses.Sort((x, y) => y.Key.CompareTo(x.Key));
            foreach (var expense in allExpenses)
            {
                reportList.Add(expense.Value + Expense.FormatPrice(expense.Key));
            }

            /* Conditional operator
             * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
             * Accessed 3 June 2022*/
            if (MainWindow.r != null)
            {
                availRent = MainWindow.r.AvailableMoney;
            }
            available = MainWindow.hl == null ? (availRent - vehicleExpense) : (MainWindow.hl.AvailableMoney - vehicleExpense);
        }

    }
}
