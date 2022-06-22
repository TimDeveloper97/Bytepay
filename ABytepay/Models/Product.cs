using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Models
{
    public class Product
    {
        public string Name1 { get; set; }
        public string Amount1 { get; set; }
        public string Name2 { get; set; }
        public string Amount2 { get; set; }

        public Product(string n1, string a1, string n2, string a2)
        {
            this.Name1 = n1;
            this.Amount1 = a1;
            this.Name2 = n2;
            this.Amount2 = a2;
        }
    }
}
