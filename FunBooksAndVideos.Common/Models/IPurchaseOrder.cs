using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Common.Models
{
    public interface IPurchaseOrder
    {
        int Id { get; set; }

        double Total { get; }

        int CustomerId { get; set; }

        IEnumerable<IPurchaseItem> Items { get; set; }
    }
}
