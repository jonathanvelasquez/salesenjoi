using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesenjoi.Mobille.MVVM.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public bool isActive { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}
