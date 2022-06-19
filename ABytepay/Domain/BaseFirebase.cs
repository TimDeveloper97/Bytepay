using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Domain
{
    public class BaseFirebase
    {
        private readonly string _realTimeData = "https://testdatabase-b76ec.firebaseio.com/";
        public readonly FirebaseClient _firebaseDatabase;

        public BaseFirebase()
        {
            _firebaseDatabase = new FirebaseClient(_realTimeData);
        }
    }
}
