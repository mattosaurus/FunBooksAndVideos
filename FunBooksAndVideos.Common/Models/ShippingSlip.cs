using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Common.Models
{
    public class ShippingSlip
    {
        public int Id { get; set; }

        public IEnumerable<PhysicalProduct> Products { get; set; }
    }
}
