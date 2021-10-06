using Billing.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Billing.Models;
using System.Collections.Generic;

namespace Billing.Service
{
    public class Service : IService
    {
        private static string _appData;

        public Service(DenominationsData options)
        {
            _appData = options.Value;
        }
        public (string output, bool issuccess) GetBalance(InputData input)
        {
            string output = "";
            bool issuccess = false;
            List<int> denominations = new();
            if (!string.IsNullOrEmpty(_appData))
            {
              denominations = _appData.Split(',').Select(n => Convert.ToInt32(n)).ToList();
            }
            if (denominations.Count > 0)
            {                
                if (input.CustomerAmount != 0.00 && input.ProductPrice != 0.00)
                {
                    double balanceAmount = input.CustomerAmount >= input.ProductPrice ?
                    (input.CustomerAmount - input.ProductPrice) : 0.00;
                    if (balanceAmount > 0)
                    {

                        double valuePound;
                        double valuePence;
                        int lastVal = denominations.Last();
                        output += "Your Change is:\n";
                        foreach (int item in denominations)
                        {
                            if (balanceAmount >= item)
                            {
                                valuePound = (int)balanceAmount / item;
                                balanceAmount %= item;
                                output += $"{valuePound} x £{item}\n";
                            }
                            if (lastVal == item && balanceAmount != 0)
                            {
                                double amount = Convert.ToInt32(string.Format("{0:f2}", balanceAmount).Remove(0, 2));
                                foreach (int item2 in denominations)
                                {
                                    if (amount >= item2)
                                    {
                                        valuePence = (int)amount / item2;
                                        amount %= item2;
                                        output += $"{valuePence} x {item2}p\n";
                                    }
                                }
                            }
                        }
                        issuccess = true;
                    }
                    else if (input.CustomerAmount < input.ProductPrice)
                    {
                        output += "Please provide appropriate amount...";
                        issuccess = false;
                    }
                    else if (input.CustomerAmount == input.ProductPrice)
                    {
                        output += "No Change , Given the correct amount.";
                        issuccess = false;
                    }
                }
                else if (input.CustomerAmount == 0.00 && input.ProductPrice != 0.00)
                {
                    output += "Customer amount is zero";
                    issuccess = false;
                }
                else if (input.ProductPrice == 0.00 && input.CustomerAmount != 0.00)
                {
                    output += "Product price is zero";
                    issuccess = false;
                }
                else
                {
                    output += "Product price and customer amount zero";
                    issuccess = false;
                }
            }else
            {
                output += "Denominations Empty";
                issuccess = false;
            }
            

            return (output, issuccess);
        }
    }
}