using Billing.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace Billing.Test
{
    public class BillingUnitTest
    {
        private readonly DenominationsData denominationdata = new() { PenceValue = new List<int>(new int[] { 50, 20, 10, 5, 2, 1 }), PoundValue = new List<int>(new int[] { 50, 20, 10, 5, 2, 1 }), SymbolValue = "00A3" };

        [Fact]
        public void Task_is_ValidBilling()
        {
            var (_, issuccess) = getResult(20, 5.5);
            Assert.True(issuccess);
        }

        [Fact]
        public void Task_Is_Valid_OutputMsg()
        {
            var (output, _) = getResult(20, 5.5); 
            var expectedValue = "Your Change is:\n1 x £10\n2 x £2\n1 x 50p\n";
            Assert.Equal(expectedValue, output);
        }

        [Fact]
        public void Task_is_NotValidOutputMsg_CusAmountLess()
        {
            var (output, _) = getResult(4, 5.5);
            var expectedValue = "Customer Amount is not sufficient, Please enter amount higher or same as the product price...";
            Assert.Equal(expectedValue, output);
        }

        [Fact]
        public void Task_is_NotValidOutputMsg_WhenNoChange()
        {
            var (output, _) = getResult(5.5, 5.5);
            var expectedValue = "Customer amount is same as the product price , No change to display...";
            Assert.Equal(expectedValue, output);
        }

        [Fact]
        public void Task_is_CustomerAmountZero()
        {
            var (output, _) = getResult(0, 5.5); 
            var expectedValue = "Customer entered amount is zero , Please enter a valid customer amount...";
            Assert.Equal(expectedValue, output);
        }

        [Fact]
        public void Task_is_ProductPriceZero()
        {
            var (output, _) = getResult(20, 0); 
            var expectedValue = "Product price entered is zero , Please enter a valid product price...";
            Assert.Equal(expectedValue, output);
        }

        [Fact]
        public void Task_is_ProductPriceAndCusAmountZero()
        {
            var (output, _) = getResult(0, 0);
            var expectedValue = "Product price and customer amount entered are zero , Please enter values greater than zero...";
            Assert.Equal(expectedValue, output);
        }

        [Theory]
        [InlineData(20, 5.5, true)]
        [InlineData(0.0, 5.5, false)]
        [InlineData(20, 0.0, false)]
        [InlineData(0.0, 0.0, false)]
        public void ValidBilling_Theory(double customeramount, double productprice, bool expectedresult)
        {
            var (_, issuccess) = getResult(customeramount, productprice);            
            Assert.Equal(expectedresult, issuccess);
        }

        public (string output,bool issuccess) getResult(double CustomerAmount, double ProductPrice)
        {
            var service = new Service.Service(denominationdata);
            return service.GetBalance(new InputData() { CustomerAmount = CustomerAmount, ProductPrice = ProductPrice });
        }

    }
}
