using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Common.Services
{
    public interface ICustomerService
    {
        Task ActivateMembershipAsync(int customerId, int productId);
    }
}
