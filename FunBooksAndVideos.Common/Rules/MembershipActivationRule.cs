using FunBooksAndVideos.Common.Models;
using FunBooksAndVideos.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Common.Rules
{
    public class MembershipActivationRule : IRule
    {
        private readonly ICustomerService _customerService;

        public MembershipActivationRule(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task ApplyRuleAsync(IPurchaseOrder purchaseOrder)
        {
            var memberships = purchaseOrder.Items.Where(x => x.Product is MembershipProduct).Select(x => x.Product);

            foreach (MembershipProduct membership in memberships)
            {
                await _customerService.ActivateMembershipAsync(purchaseOrder.CustomerId, membership.Id);
            }
        }
    }
}
