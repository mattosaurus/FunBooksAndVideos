using FunBooksAndVideos.Common.Models;
using FunBooksAndVideos.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Common.Rules
{
    public class PhysicalProductShippingSlipRule : IRule
    {
        private readonly IShippingService _shippingService;

        public PhysicalProductShippingSlipRule(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        public async Task ApplyRuleAsync(IPurchaseOrder purchaseOrder)
        {
            var purchaseItems = purchaseOrder.Items.Where(x => x.Product is PhysicalProduct);

            await _shippingService.GenerateShippingSlipAsync(purchaseOrder.CustomerId, purchaseItems);
        }
    }
}
