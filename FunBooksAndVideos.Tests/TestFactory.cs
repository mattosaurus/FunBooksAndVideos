using FunBooksAndVideos.Common.Models;
using FunBooksAndVideos.Common.Rules;
using FunBooksAndVideos.Common.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static Mock<IShippingService> CreateMockShippingService(IEnumerable<PhysicalProduct> products)
        {
            var mockShippingService = new Mock<IShippingService>();
            ShippingSlip shippingSlip = GetShippingSlip(products);
            mockShippingService.Setup(shippingService => shippingService.GenerateShippingSlipAsync(It.IsAny<int>(), It.IsAny<IEnumerable<IPurchaseItem>>())).Returns(Task.FromResult(shippingSlip));

            return mockShippingService;
        }

        public static IRule CreateMembershipActivationRule()
        {
            return new MembershipActivationRule(CreateMockCustomerService().Object);
        }

        public static IRule CreateMembershipActivationRule(ICustomerService customerService)
        {
            return new MembershipActivationRule(customerService);
        }

        public static IRule CreatePhysicalProductShippingSlipRule(IEnumerable<PhysicalProduct> products)
        {
            return new PhysicalProductShippingSlipRule(CreateMockShippingService(GetShippingSlip(products)).Object);
        }

        public static IRule CreatePhysicalProductShippingSlipRule(IShippingService shippingService)
        {
            return new PhysicalProductShippingSlipRule(shippingService);
        }

        public static RulesProcessor CreateRulesProcessor()
        {
            List<IRule> rules = new List<IRule>();
            rules.Add(CreateMembershipActivationRule());
            rules.Add(CreatePhysicalProductShippingSlipRule(GetPhysicalProducts()));

            return new RulesProcessor(rules);
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

        public static IEnumerable<PurchaseItem> GetPurchaseItems()
        {
            List<PurchaseItem> purchaseItems = new List<PurchaseItem>();

            purchaseItems.AddRange(GetPhysicalProducts().Select(x => new PurchaseItem() { Id = x.Id, Product = x }));
            purchaseItems.AddRange(GetMembershipProducts().Select(x => new PurchaseItem() { Id = x.Id, Product = x }));

            return purchaseItems;
        }

        public static IEnumerable<PurchaseItem> GetPurchaseItems(IEnumerable<Product> products)
        {
            List<PurchaseItem> purchaseItems = new List<PurchaseItem>();

            purchaseItems.AddRange(products.Select(x => new PurchaseItem() { Id = x.Id, Product = x }));

            return purchaseItems;
        }

        public static PurchaseOrder GetPurchaseOrder()
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                Id = 987654321,
                CustomerId = 987,
                Items = GetPurchaseItems()
            };

            return purchaseOrder;
        }

        public static PurchaseOrder GetPurchaseOrder(IEnumerable<PurchaseItem> purchaseItems)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                Id = 987654321,
                CustomerId = 987,
                Items = purchaseItems
            };

            return purchaseOrder;
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
