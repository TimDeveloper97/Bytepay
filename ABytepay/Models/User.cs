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

    public class Account
    {
        public string Email { get; set; }
        public string Key { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LicenseKey
    {
        public string Email { get; set; }
        public string Key { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsUse { get; set; }
    }
}
