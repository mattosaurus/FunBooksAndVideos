using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Common.Models
{
    public interface IPurchaseItem
    {
        int Id { get; set; }

        Product Product { get; set; }
    }
}
