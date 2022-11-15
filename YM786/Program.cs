using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using YM786.Model;

namespace YM786
{
    class Program
    {   
        private static string _connectoinString = ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString;

        private static SqlConnection _sqlConnection = null;
        static void Main(string[] args)
        {
            //подключение к базам данных
            _sqlConnection = new SqlConnection(_connectoinString);
            _sqlConnection.Open();

            Console.WriteLine("StoreApp");
            string command = string.Empty;
            //Объект для запуска любых команд
            SqlCommand sqlCommand;
            while (true)
            {
                try
                {
                    InstallmentPlanManager installmentPlan = new InstallmentPlanManager();
                    Console.Write("Phone Number: ");
                    int phoneNumber = int.Parse(Console.ReadLine());
                    Console.Write("Amount: ");
                    int amount = int.Parse(Console.ReadLine());
                    Console.Write("Period: ");
                    int renge = installmentPlan.CheckRenge = int.Parse(Console.ReadLine());
                    Console.Write("Loan Type : ");
                    string loanType = Console.ReadLine();
                    //выйти и закрыть подключение базы данных
                    if (loanType.ToLower().Equals("exit"))
                    {
                        if (_sqlConnection.State.Equals(ConnectionState.Open))
                            _sqlConnection.Close();
                        break;
                    }
                    int principalAmount = installmentPlan.PaymentCalculation((ProductСategories)Enum.Parse(typeof(ProductСategories), loanType), amount, phoneNumber, renge);
                    Console.WriteLine(principalAmount);

                    command = $"INSERT INTO [Table] (PhoneNumber" +
                        $", PrincipalAmount, Period, LoanType) VALUES ('{phoneNumber}', '{principalAmount}', '{renge}', '{loanType}')";

                    sqlCommand = new SqlCommand(command, _sqlConnection);

                    Console.WriteLine(sqlCommand.ExecuteNonQuery());
                    Console.ReadKey();
                }
                catch(Exception ex)
                { Console.WriteLine(ex.Message); }
            }
        }
    }
}
