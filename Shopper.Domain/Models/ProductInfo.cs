using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Domain
{
    public partial class Product : BaseEntity
    {
        public bool IsActive { get; set; }
    }
}
