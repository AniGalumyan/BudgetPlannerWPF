using System;
using System.Linq;

namespace BudgetPlannerWPF.Code
{
    public class HomeLoan : Expense
    {
        #region Fields
        decimal interest;
        public decimal PurchasePrice { get; set; }
        public decimal Deposit { get; set; }
        public decimal Interest
        {
            get => interest;
            //Prompt for interest rate as a percentage and convert it to decimal format for computations.

            set => interest = value / 100;
        }
        public int RepayMonths { get; set; }
        public decimal MonthlyRepayment { private set; get; }
        #endregion

        #region Methods
        public HomeLoan(decimal pPrice, decimal deposit, decimal interest, int rMonths, decimal gIncome, decimal tDeduct, decimal f, decimal c, decimal t, decimal u, decimal e) : base(gIncome, tDeduct, f, c, t, u, e)
        {
            PurchasePrice = pPrice;
            Deposit = deposit;
            Interest = interest;
            RepayMonths = rMonths;
            AvailableMoney = CalculateAvailable();
        }
        private decimal CalculateMonthlyRepayment()
        {
            decimal repayment = (PurchasePrice - Deposit) * (1 + Interest * (RepayMonths / 12));
            repayment /= RepayMonths;
            if (repayment > GrossIncome / 3)
            {
                Console.Write($"---\nWARNING: Approval of this home loan is unlikely. The monthly repayment is {FormatPrice(repayment)}, which is more than a third of your gross monthly income.\n---");
            }
            MonthlyRepayment = repayment;
            return repayment;
        }
        protected override decimal CalculateAvailable()
        {
            return GrossIncome - TaxDeduct - GetExpenses() - CalculateMonthlyRepayment();
        }
        #endregion
    }
}
