using Billing.Models;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Billing.Test
{
    public class BillingUnitTest
    {

        [Fact]
        public void Task_is_ValidBilling()
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData() { CustomerAmount = 20, ProductPrice = 5.5 });
            Assert.True(result.issuccess);
        }

        [Fact]
        public void Task_is_ValidOutputMsg()
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData() { CustomerAmount = 20, ProductPrice = 5.5 });
            var expectedValue = "Your Change is:\n1 x £10\n2 x £2\n1 x 50p\n";
            Assert.Equal(expectedValue, result.output);
        }

        [Fact]
        public void Task_is_NotValidOutputMsg_CusAmountLess()
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData() { CustomerAmount = 4, ProductPrice = 5.5 });
            var expectedValue = "Please provide appropriate amount...";
            Assert.Equal(expectedValue, result.output);
        }

        [Fact]
        public void Task_is_NotValidOutputMsg_WhenNoChange()
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData() { CustomerAmount = 5.5, ProductPrice = 5.5 });
            var expectedValue = "No Change , Given the correct amount.";
            Assert.Equal(expectedValue, result.output);
        }

        [Fact]
        public void Task_is_CustomerAmountZero()
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData() { CustomerAmount = 0, ProductPrice = 5.5 });
            var expectedValue = "Customer amount is zero";
            Assert.Equal(expectedValue, result.output);
        }

        [Fact]
        public void Task_is_ProductPriceZero()
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData() { CustomerAmount = 20, ProductPrice = 0 });
            var expectedValue = "Product price is zero";
            Assert.Equal(expectedValue, result.output);
        }

        [Fact]
        public void Task_is_ProductPriceAndCusAmountZero()
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData() { CustomerAmount = 0, ProductPrice = 0 });
            var expectedValue = "Product price and customer amount zero";
            Assert.Equal(expectedValue, result.output);
        }

        [Theory]
        [InlineData(20, 5.5, true)]
        [InlineData(0.0, 5.5, false)]
        [InlineData(20, 0.0, false)]
        [InlineData(0.0, 0.0, false)]
        public void ValidBilling_Theory(double customeramount, double productprice, bool expectedresult)
        {
            var denominationdata = new DenominationsData() { Value = "50, 20, 10, 5, 2, 1" };
            var service = new Service.Service(denominationdata);
            var result = service.GetBalance(new InputData()
            {
                CustomerAmount = customeramount,
                ProductPrice = productprice
            });
            Assert.Equal(expectedresult, result.issuccess);
        }

    }
}
