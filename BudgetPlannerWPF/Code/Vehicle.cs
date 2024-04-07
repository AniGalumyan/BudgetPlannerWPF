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
            //Ask for interest as percentage, convert to decimal for calculations
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
            /* Calculation from:
             * https://www.siyavula.com/read/maths/grade-10/finance-and-growth/09-finance-and-growth-03
             * Accessed 10 May 2022 */
            vExpense = (Price + Deposit) ;

            return vExpense;
        }
        #endregion
    }
}
