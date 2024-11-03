using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Egitim
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }

        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Stock { get; set; }
        public int TotalSalesQuantityCount { get; set; }

        public decimal TotalEarningFromProduct { get; set; }

    }
}
