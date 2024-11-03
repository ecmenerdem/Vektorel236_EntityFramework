using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Egitim
{
    public class OrderDetailResponseDTO
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal UnitTotal
        {

            get
            {
                return UnitPrice*Quantity;
            }

        }

    }
}
