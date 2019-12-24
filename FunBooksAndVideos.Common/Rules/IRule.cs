using FunBooksAndVideos.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Common.Rules
{
    public interface IRule
    {
        Task ApplyRuleAsync(IPurchaseOrder purchaseOrder);
    }
}
