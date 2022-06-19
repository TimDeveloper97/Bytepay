using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseKey
{
    class Program
    {
        private static readonly string _realTimeData = "https://testdatabase-b76ec.firebaseio.com/";
        public static FirebaseClient _firebaseDatabase;

        static async Task Main(string[] args)
        {
            _firebaseDatabase = new FirebaseClient(_realTimeData);

            await AddKey();
        }

        static async Task AddKey()
        {
            try
            {
                var key = new ABytepay.Models.License
                {
                    Key = Guid.NewGuid().ToString().ToUpper(),
                    IsUse = false,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(7),
                };
                await _firebaseDatabase.Child("Keys").PostAsync(key);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
