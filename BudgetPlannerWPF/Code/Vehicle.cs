namespace BudgetPlannerWPF.Code
{
    public class Vehicle
    {
        #region Fields
        private const int repayYears = 5;
        private decimal interest;
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public decimal Interest
        {
            get => interest;
            //Prompt for interest rate as a percentage and convert it to decimal format for computations.

            set => interest = value / 100;
        }
        public decimal Insurance { get; set; }
        #endregion

        #region Methods
        public Vehicle(string make,  decimal price, decimal deposit)
        {
            Make = make;
            Price = price;
            Deposit = deposit;
            
        }

        public decimal CalculateVehicleExpense()
        {
            decimal vExpense;
            
            vExpense = (Price + Deposit) ;

            return vExpense;
        }
        #endregion
    }
}
