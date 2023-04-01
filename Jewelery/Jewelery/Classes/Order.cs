using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelery.Classes
{
    internal class Order
    {
        private static List<VW_ProductList> orders = new List<VW_ProductList>();

        public static List<VW_ProductList> OrderList { get {return orders; } set { orders = value; } }
    }
}
