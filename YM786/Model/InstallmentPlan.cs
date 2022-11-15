using System;
using System.Linq;


namespace YM786.Model
{
    enum ProductСategories
    {
        Smartphone = 1,
        Television = 2,
        Computer = 3
    }
    class InstallmentPlan 
    {   
        int[] range = { 3, 6, 9, 12, 18, 24 };
        private int _range;
        public int CheckRenge 
        {
            get => _range;
            set
            {
                if (value >= 3 && value.Equals(range.FirstOrDefault(x => x.Equals(value))))
                    _range = value;
                else
                    throw new Exception("Range set incorrectly!! ");
            }
        }
        public int PrincipalAmount { get; set; }
        public string SMSmessage { get; set; }
        public int Percent { get; set; }

    }
    class InstallmentPlanManager : InstallmentPlan
    {
        public int PaymentCalculation(ProductСategories product, int amountOfPayment, int phoneNumber, int range)
        {
            
            
            switch (product)
            {
                case ProductСategories.Smartphone:
                    Percent = 3 * range / 12; 
                    PrincipalAmount = (amountOfPayment * Percent) / 100 + amountOfPayment;
                    break;
                case ProductСategories.Computer:
                    Percent = 3 * range / 12;
                    PrincipalAmount = (amountOfPayment * Percent) / 100 + amountOfPayment;
                    break;
                case ProductСategories.Television:
                    Percent = 3 * range / 18;
                    PrincipalAmount = (amountOfPayment * Percent) / 100 + amountOfPayment;
                    break;
                default:
                    throw new Exception("Wrong categoria type!");
            }
            SMSmessage = $"Phone number {phoneNumber},monthly payment: {Percent}, due date: {range} and commission{PrincipalAmount- amountOfPayment}";
            return PrincipalAmount;
        }
    }
}
