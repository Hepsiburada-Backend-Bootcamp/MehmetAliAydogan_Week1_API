using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiBurada_Week1.QueryFilters
{
    public class ProductFilter
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string PriceLT { get; set; }
        public string PriceMT { get; set; }
        public string DiscountPercLT { get; set; }
        public string SortBy { get; set; }

    }
}
