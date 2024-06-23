using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sushi_darom.cs
{
    internal class Orders
    {
        public string order_id { get; set; }
        public string customer_name { get; set; }
        public string order_date { get; set; }
        public string order_status { get; set; }
        public string total_sum { get; set; }
        public string comment_ord { get; set; }
    }
}
