using FunBooksAndVideos.Common.Models;
using FunBooksAndVideos.Common.Rules;
using FunBooksAndVideos.Common.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FunBooksAndVideos.Tests.UnitTests
{
    public class RuleUnitTest
    {
        [Fact]
        public async Task MembershipActivationRule_MembershipsAreActivated()
        {
            // Arrange
            Mock<ICustomerService> mockCustomerService = TestFactory.CreateMockCustomerService();
            IRule membershipActivationRule = TestFactory.CreateMembershipActivationRule(mockCustomerService.Object);
            RulesProcessor rulesProcessor = new RulesProcessor(new List<IRule>() { membershipActivationRule });
            PurchaseOrder purchaseOrder = TestFactory.GetPurchaseOrder();

            // Act
            await rulesProcessor.ProcessOrderAsync(purchaseOrder);

            // Assert
            foreach (PurchaseItem item in purchaseOrder.Items.Where(x => x.Product is MembershipProduct))
            {
                mockCustomerService.Verify(x => x.ActivateMembershipAsync(purchaseOrder.CustomerId, item.Product.Id));
            }
        }

        [Fact]
        public async Task MembershipActivationRule_PhysicalProductsArentActivated()
        {
            // Arrange
            Mock<ICustomerService> mockCustomerService = TestFactory.CreateMockCustomerService();
            IRule membershipActivationRule = TestFactory.CreateMembershipActivationRule(mockCustomerService.Object);
            RulesProcessor rulesProcessor = new RulesProcessor(new List<IRule>() { membershipActivationRule });
            PurchaseOrder purchaseOrder = TestFactory.GetPurchaseOrder(TestFactory.GetPurchaseItems(TestFactory.GetPhysicalProducts()));

            // Act
            await rulesProcessor.ProcessOrderAsync(purchaseOrder);

            // Assert
            mockCustomerService.Verify(x => x.ActivateMembershipAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task PhysicalProductShippingSlipRule_ShippingSlipIsGenerated()
        {
            // Arrange
            IEnumerable<PhysicalProduct> physicalProducts = TestFactory.GetPhysicalProducts();
            Mock<IShippingService> mockShippingService = TestFactory.CreateMockShippingService(physicalProducts);
            IRule physicalProductShippingSlipRule = TestFactory.CreatePhysicalProductShippingSlipRule(mockShippingService.Object);
            RulesProcessor rulesProcessor = new RulesProcessor(new List<IRule>() { physicalProductShippingSlipRule });
            PurchaseOrder purchaseOrder = TestFactory.GetPurchaseOrder();

            // Act
            await rulesProcessor.ProcessOrderAsync(purchaseOrder);

            // Assert
            var physicalPurchaseItems = purchaseOrder.Items.Where(x => x.Product is PhysicalProduct);
            mockShippingService.Verify(x => x.GenerateShippingSlipAsync(purchaseOrder.CustomerId, physicalPurchaseItems));
        }

        [Fact]
        public async Task PhysicalProductShippingSlipRule_MembershipDoesntGenerateShippingSlip()
        {
            // Arrange
            IEnumerable<PhysicalProduct> physicalProducts = TestFactory.GetPhysicalProducts();
            Mock<IShippingService> mockShippingService = TestFactory.CreateMockShippingService(physicalProducts);
            IRule physicalProductShippingSlipRule = TestFactory.CreatePhysicalProductShippingSlipRule(mockShippingService.Object);
            RulesProcessor rulesProcessor = new RulesProcessor(new List<IRule>() { physicalProductShippingSlipRule });
            PurchaseOrder purchaseOrder = TestFactory.GetPurchaseOrder(TestFactory.GetPurchaseItems(TestFactory.GetMembershipProducts()));

            // Act
            await rulesProcessor.ProcessOrderAsync(purchaseOrder);

            // Assert
            mockShippingService.Verify(x => x.GenerateShippingSlipAsync(It.IsAny<int>(), It.IsAny<IEnumerable<PurchaseItem>>()), Times.Never);
        }
    }
}
