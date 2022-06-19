using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Models
{
    public class User
    {
        public string Email { get; set; }
        public string ComputerId { get; set; }
        public List<Product> Products { get; set; }
        public List<string> Keys { get; set; }
    }

    public class License
    {
        public string Key { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsUse { get; set; }
    }
}
