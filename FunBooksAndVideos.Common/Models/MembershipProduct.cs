using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Common.Models
{
    public class MembershipProduct : Product
    {
        public MembershipType MembershipType { get; set; }
    }
}
