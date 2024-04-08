namespace BudgetPlannerWPF.Code
{
    public class Rent : Expense
    {
        #region Fields
        public decimal RentalAmount { get; set; }
        #endregion

        #region Methods
        public Rent(decimal rAmount, decimal gIncome, decimal tDeduct, decimal f, decimal c, decimal t, decimal u, decimal e) : base(gIncome, tDeduct, f, c, t, u, e)
        {
            RentalAmount = rAmount;
            AvailableMoney = CalculateAvailable();
        }
        protected override decimal CalculateAvailable()
        {
            return GrossIncome - TaxDeduct - GetExpenses() - RentalAmount;
        }
        #endregion
    }
}
