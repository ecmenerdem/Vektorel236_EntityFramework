using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Egitim
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }

        public List<OrderDetailResponseDTO> Details { get; set; }

        public decimal GrandTotal
        {
            get
            {
                return Details.Sum(x => x.UnitTotal);
            }
        }
    }
}
