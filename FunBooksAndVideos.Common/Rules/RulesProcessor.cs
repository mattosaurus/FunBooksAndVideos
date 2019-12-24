using FunBooksAndVideos.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Common.Rules
{
    public class RulesProcessor
    {
        private readonly IEnumerable<IRule> _rules;

        public RulesProcessor(IEnumerable<IRule> rules)
        {
            _rules = rules;
        }

        public async Task ProcessOrderAsync(IPurchaseOrder purchaseOrder)
        {
            foreach (IRule rule in _rules)
            {
                await rule.ApplyRuleAsync(purchaseOrder);
            }
        }
    }
}
