using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunBooksAndVideos.Common.Models
{
    public class PurchaseOrder : IPurchaseOrder
    {
        public int Id { get; set; }

        public double Total
        {
            get
            {
                return Items.Sum(x => x.Product.Value);
            }
        }

        public int CustomerId { get; set; }

        public IEnumerable<IPurchaseItem> Items { get; set; }
    }
}
