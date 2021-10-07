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
        private static DenominationsData _appData;

        public Service(DenominationsData options)
        {
            _appData = options;
        }
        public (string output, bool issuccess) GetBalance(InputData input)
        {
            string output = "";
            bool issuccess = false;
            List<int> denominationsPound = new();
            List<int> denominationsPence = new();
            char currencySymbol = (char)ushort.Parse(_appData.SymbolValue, System.Globalization.NumberStyles.HexNumber);
            if (_appData.PoundValue != null && _appData.PenceValue != null)
            {
                denominationsPound = _appData.PoundValue;
                denominationsPence = _appData.PenceValue;
            }
            if (denominationsPound.Count > 0 && denominationsPence.Count > 0)
            {
                if (input.CustomerAmount != 0 && input.ProductPrice != 0)
                {
                    double balanceAmount = input.CustomerAmount >= input.ProductPrice ?
                    (input.CustomerAmount - input.ProductPrice) : 0;
                    if (balanceAmount > 0)
                    {
                        double valuePound;
                        double valuePence;
                        int lastVal = denominationsPound.Last();
                        output += "Your Change is:\n";
                        foreach (int item in denominationsPound)
                        {
                            if (balanceAmount >= item)
                            {
                                valuePound = (int)balanceAmount / item;
                                balanceAmount %= item;
                                output += $"{valuePound} x {currencySymbol}{item}\n";
                            }
                            if (lastVal == item && balanceAmount != 0)
                            {
                                double amount = Convert.ToInt32(string.Format("{0:f2}", balanceAmount).Remove(0, 2));
                                foreach (int item2 in denominationsPence)
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
                        output += "Customer Amount is not sufficient, Please enter amount higher or same as the product price...";
                        issuccess = false;
                    }
                    else if (input.CustomerAmount == input.ProductPrice)
                    {
                        output += "Customer amount is same as the product price , No change to display...";
                        issuccess = false;
                    }
                }
                else if (input.CustomerAmount == 0 && input.ProductPrice != 0)
                {
                    output += "Customer entered amount is zero , Please enter a valid customer amount...";
                    issuccess = false;
                }
                else if (input.ProductPrice == 0 && input.CustomerAmount != 0)
                {
                    output += "Product price entered is zero , Please enter a valid product price...";
                    issuccess = false;
                }
                else
                {
                    output += "Product price and customer amount entered are zero , Please enter values greater than zero...";
                    issuccess = false;
                }
            }
            else
            {
                output += "Denominations configurations is empty";
                issuccess = false;
            }


            return (output, issuccess);
        }
    }
}