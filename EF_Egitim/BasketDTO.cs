using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Egitim
{
    public class BasketDTO
    {

        private readonly NORTHWINDEntities _context;

        public BasketDTO(NORTHWINDEntities context)
        {
            _context = context;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CustomerID { get; set; }
        public int Quantity { get; set; }

        public decimal? UnitPrice
        {


            get
            {
                return _context.Set<Products>().SingleOrDefault(q => q.ProductID == ProductID).UnitPrice;
            }


        }

        public decimal? UnitTotal
        {
            get
            {
                if (DateTime.Now.Date.Day == 3)
                {
                    double total = Convert.ToDouble(Quantity * _context.Set<Products>().SingleOrDefault(q => q.ProductID == ProductID).UnitPrice);

                    double discountTotal = total - (total * 0.3);

                    return Convert.ToDecimal(discountTotal);
                }

                return Quantity * _context.Set<Products>().SingleOrDefault(q => q.ProductID == ProductID).UnitPrice;
            }
        }

    }
}
