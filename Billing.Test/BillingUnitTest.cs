using Billing.Service.InputDTO;
using Billing.Service.Interfaces;
using Moq;
using Xunit;

namespace Billing.Test
{
    public class BillingUnitTest
    {
        [Fact]
        public void Task_is_ValidBilling()
        {            
            var service = new Service.Service();
            var result = service.GetBalance(new InputData() { CustomerAmount = 20, ProductPrice = 5.5 });
            Assert.True(result.issuccess);
        }

        [Theory]
        [InlineData(20, 5.5, true)]
        [InlineData(0.0, 5.5, false)]
        [InlineData(20, 0.0, true)]
        [InlineData(0.0, 0.0, false)]
        public void ValidBilling_Theory(double customeramount, double productprice, bool expectedresult)
        {
            var service = new Service.Service();
            var result = service.GetBalance(new InputData() { CustomerAmount = customeramount,
                ProductPrice = productprice });
            Assert.Equal(expectedresult, result.issuccess);
        }
    }
}
