using FunBooksAndVideos.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Common.Services
{
    public interface IShippingService
    {
        Task<ShippingSlip> GenerateShippingSlipAsync(int customerId, IEnumerable<PurchaseItem> purchaseItems);
    }
}
