using DeviceId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Helpers
{
    public class ComputerHelper
    {
        public static string GetDeviceId()
        {
            string deviceId = new DeviceIdBuilder()
                .AddMachineName().AddMacAddress().AddUserName().AddOsVersion().AddUserName().ToString();
            return deviceId;
        }
    }
}
