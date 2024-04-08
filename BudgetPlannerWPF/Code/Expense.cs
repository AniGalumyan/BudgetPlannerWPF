using System;
using System.Collections.Generic;

namespace BudgetPlannerWPF.Code
{
    public abstract class Expense
    {
        
        #region Inherited Fields
        public decimal GrossIncome { get; set; }
        public decimal TaxDeduct { get; set; }
        //Expenses
        public List<KeyValuePair<decimal, string>> namedExpenses;
        public decimal AvailableMoney { get; set; }
        #endregion

        #region Inherited Methods
        protected Expense(decimal gIncome, decimal tDeduct, decimal f, decimal c, decimal t, decimal u, decimal e)
        {
            GrossIncome = gIncome;
            TaxDeduct = tDeduct;
            
            namedExpenses = new List<KeyValuePair<decimal, string>>() {
                new KeyValuePair<decimal, string> (f, "Fodds: "),
                new KeyValuePair<decimal, string> (c, "Clothes: "),
                new KeyValuePair<decimal, string> (t, "Travel: "),
                new KeyValuePair<decimal, string> (u, "University fees: "),
                new KeyValuePair<decimal, string> (e, "Entertainment: ")
            };
        }

        public static string FormatPrice(decimal price) => "R" + string.Format("{0:N2}", price);
        public decimal GetExpenses()
        {
            // total of expenses 
            decimal total = 0;
            foreach (var item in namedExpenses)
            {
                total += item.Key;
            }
            return total;
        }
        #endregion

        #region Abstract methods
        protected abstract decimal CalculateAvailable();
        #endregion
    }
}
