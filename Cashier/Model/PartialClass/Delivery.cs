using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Model
{
    public partial class Delivery
    {
        public string StrDate
        {
            get 
            {
                return DTDelivery.Date.ToShortDateString();
            }
        }
        public TimeSpan StrTime
        {
            get
            {
                return DTDelivery.TimeOfDay;
            }
        }

    }
}
