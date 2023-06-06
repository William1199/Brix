using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Carts
{
    public class CreateCartRequest
    {

        public int ProductID { get; set; }

        public int? TypeID { get; set; }    

        public int Quantity { get; set; }
    }
}
