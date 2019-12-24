using FunBooksAndVideos.Common.Models;
using FunBooksAndVideos.Common.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Tests
{
    public class TestFactory
    {
        public static Mock<ICustomerService> CreateMockCustomerService()
        {
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService.Setup(customerService => customerService.ActivateMembershipAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.CompletedTask);

            return mockCustomerService;
        }

        public static Mock<IShippingService> CreateMockShippingService(ShippingSlip shippingSlip)
        {
            var mockShippingService = new Mock<IShippingService>();
            mockShippingService.Setup(shippingService => shippingService.GenerateShippingSlipAsync(It.IsAny<int>(), It.IsAny<IEnumerable<IPurchaseItem>>())).Returns(Task.FromResult(shippingSlip));

            return mockShippingService;
        }

        public static IEnumerable<PhysicalProduct> GetPhysicalProducts()
        {
            return new List<PhysicalProduct>()
            { 
                new Video()
                {
                    Id = 1,
                    Name = "Comprehensive First Aid Training",
                    Value = 19.99
                },
                new Book()
                {
                    Id = 2,
                    Name = "The Girl on the train",
                    Value = 7.99
                }
            };
        }

        public static IEnumerable<MembershipProduct> GetMembershipProducts()
        {
            return new List<MembershipProduct>()
            {
                new MembershipProduct()
                {
                    Id = 3,
                    Name = "Book Club Membership",
                    Value = 29.99,
                    MembershipType = MembershipType.Book
                }
            };
        }

        public static ShippingSlip GetShippingSlip(IEnumerable<PhysicalProduct> products)
        {
            return new ShippingSlip()
            {
                Id = 123456789,
                Products = products
            };
        }
    }
}
