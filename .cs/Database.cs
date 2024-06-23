using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sushi_darom.cs
{
    internal class Database
    {
        static public List<Products> products = new List<Products>();
        static public List<Logpass> logpasses = new List<Logpass>();
        static public List<Category> categories = new List<Category>();
        static public List<Customers> customers = new List<Customers>();
        static public List<Cart> cart = new List<Cart>();
        static public List<OrderDetails> orderDetails = new List<OrderDetails>();
        static public List<Orders> orders = new List<Orders>();
        
    }
}
