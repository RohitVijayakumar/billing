using Billing.Service.InputDTO;
using Billing.Service.Interfaces;
using System.Linq;

namespace Billing.Service
{
    public class Service : IService
    {
        public (string output, bool issuccess) GetBalance(InputData input)
        {
            string output = "";
            bool issuccess;
            double balanceAmount = input.CustomerAmount >= input.ProductPrice ?
                (input.CustomerAmount - input.ProductPrice) : 0.00;
            if (balanceAmount > 0)
            {
                int[] denominations = { 50, 40, 20, 10, 2, 1 };
                double value;
                int lastVal = denominations.Last();
                output += "Your Change is:\n";
                foreach (int item in denominations)
                {
                    if (balanceAmount > item)
                    {
                        value = (int)balanceAmount / item;
                        balanceAmount %= item;
                        output += $"{value} x £{item}\n";
                    }

                    if (lastVal == item && balanceAmount != 0)
                    {
                        output += $"1 x {string.Format("{0:f2}", balanceAmount).Remove(0, 2)}p\n";
                    }
                }
                issuccess = true;
            }
            else
            {
                output += "Please provide a valid CustomerAmount...";
                issuccess = false;
            }
            return (output, issuccess);
        }
    }
}