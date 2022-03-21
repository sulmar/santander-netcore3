using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Domain.Models
{

    public abstract class SearchCriteria : Base
    {

    }

    public class ProductSearchCriteria : SearchCriteria
    {
        public string Color { get; set; }
        public decimal? From { get; set; }
        public decimal? To { get; set; }        
    }
}
