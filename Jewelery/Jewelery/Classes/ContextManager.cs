using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelery.Classes
{
    internal class ContextManager
    {
        private static JeweleryEntities context;

        public static JeweleryEntities GetContext()
        {
            if(context != null)
            {
                return context;
            }
            else
            {
                return context = new JeweleryEntities();
            }
                
        }
    }
}
