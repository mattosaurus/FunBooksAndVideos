﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Common.Models
{
    public class PurchaseItem : IPurchaseItem
    {
        public int Id { get; set; }

        public Product Product { get; set; }
    }
}
