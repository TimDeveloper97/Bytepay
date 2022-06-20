using ABytepay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ABytepay.Helpers
{
    public class CRUDHelper
    {
        static string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?.Replace("file:\\", "");
        static string pfile = path + @"\Drivers\" + "data.json";
        static string pdirec = path + @"\Drivers\";

        public static void Serialize(Account data)
        {
            try
            {
                InitFile();

                var json = JsonConvert.SerializeObject(data);
                File.WriteAllText(pfile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("No data login");
            }
            finally
            {
            }
        }

        public static Account Deserialize()
        {
            try
            {
                InitFile();

                var json = File.ReadAllText(path + @"\Drivers\" + "data.json");
                var data = JsonConvert.DeserializeObject<Account>(json);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No data login");
            }

            return null;
        }

        private static void InitFile()
        {
            if (!Directory.Exists(pdirec))
                Directory.CreateDirectory(pdirec);

            if (!File.Exists(pfile))
                File.Create(pfile);
        }
    }
}
